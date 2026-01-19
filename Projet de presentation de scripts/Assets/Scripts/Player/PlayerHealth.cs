using System;
using UnityEngine;

namespace Player.Core
{
    public class PlayerHealth : MonoBehaviour, IDamageablePlayer
    {
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private PlayerReferenceSO _playerReferenceSO;

        public static Action OnPlayerDeath;
        private Animator _animator;
        public bool IsDead { get; private set; }
        private readonly int IsDeadHash = Animator.StringToHash("IsDead");


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
           
        }

        private void Start()
        {
            IsDead  = false;
            _currentHealth = _maxHealth;
            _playerReferenceSO.IsPlayerDeadSO = false;
        }
        
        
   
        public void DamagePlayer(int damage)
        {
            _currentHealth -= damage;
        }
        
        private void Update()
        {
            if (IsDead) return;
            Die();
        }
        

        private void Die()
        {
            if (_currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
                _animator.SetTrigger(IsDeadHash);
                IsDead = true;
                _playerReferenceSO.IsPlayerDeadSO = true;
            }
        }
        
        
    }
}

