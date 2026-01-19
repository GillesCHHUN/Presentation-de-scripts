using System;
using System.Collections;
using UnityEngine;


namespace Player.Core
{
    public class PlayerDamageFX : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private void Start()
        {
            StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            for (int i = 0; i < 5; i++)
            {
                _meshRenderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
                _meshRenderer.enabled = true;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

}

