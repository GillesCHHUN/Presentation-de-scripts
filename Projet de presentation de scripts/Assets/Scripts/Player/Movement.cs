using System;
using UnityEngine;


namespace Player.Core
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private PlayerReferenceSO playerRef;

        private CharacterController _controller;
        private InputReader _input;
        private Animator _animator;

        private readonly int MoveXHash = Animator.StringToHash("MoveX");
        private readonly int MoveYHash = Animator.StringToHash("MoveY");

        private float verticalVelocity;
        private Vector3 playerControls;
        private PlayerHealth playerHealth;


        [SerializeField] private LayerMask aimLayerMask;
        [SerializeField] private float rotationSpeed = 20f;


        private void Start()
        {
            playerRef.ColliderSO = this.GetComponent<CharacterController>();

            _input = GetComponent<InputReader>();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if (playerHealth.IsDead) return;

            PlayerMovement();
            PlayerStickRotation();

            // PlayerMouseRotation();
        }

        private void PlayerMovement()
        {
            playerControls = new Vector3(_input.MovementValue.x, 0, _input.MovementValue.y).normalized;
            PlayerGravity();
            _controller.Move(playerControls * moveSpeed * Time.deltaTime);
            PlayerAnimation(playerControls);
        }

        private void PlayerGravity()
        {
            if (!_controller.isGrounded)
            {
                verticalVelocity -= 9.81f * Time.deltaTime;
                playerControls.y = verticalVelocity;
            }
            else
            {
                verticalVelocity = -.5f;
            }
        }

        private void PlayerAnimation(Vector3 playerControls)
        {
            float xVelocity = Vector3.Dot(playerControls.normalized, transform.right);
            float zVelocity = Vector3.Dot(playerControls.normalized, transform.forward);
            _animator.SetFloat(MoveXHash, xVelocity, 0.1f, Time.deltaTime);
            _animator.SetFloat(MoveYHash, zVelocity, 0.1f, Time.deltaTime);
        }

        private void PlayerMouseRotation()
        {
            RaycastHit hitInfo;

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity,
                aimLayerMask);

            if (hit)
            {
                Vector3 lookDirection = hitInfo.point - transform.position;
                lookDirection.y = 0;
                lookDirection.Normalize();

                transform.forward = lookDirection;
            }
        }

        private void PlayerStickRotation()
        {
            if (_input.RotationValue.sqrMagnitude > 0.1f)
            {
                Vector3 targetDirection = new Vector3(_input.RotationValue.x, 0, _input.RotationValue.y);

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}