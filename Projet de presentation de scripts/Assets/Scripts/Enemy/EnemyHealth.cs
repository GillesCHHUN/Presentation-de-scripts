using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour,IDamageable
    {
        public int currentHealth;
        [SerializeField] private int maxHealth = 20;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}


