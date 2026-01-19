using System;
using Player.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyEventAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject enemyToDestroy;
        
        [SerializeField] private GameObject attackRHCollider;
        [SerializeField] private GameObject attackLHCollider;
         
        public bool IsAttacking { get; set; }
   
        public void ResetAttack()
        {
            IsAttacking = false;
        }

        public void DestroyEnemy()
        {
            Destroy(enemyToDestroy, 1);
        }
    
        public void ActivateRHCollider()
        {
            attackRHCollider.gameObject.SetActive(true);
        }

        public void DeactivateRHCollider()
        {
            attackRHCollider.gameObject.SetActive(false);
        }
        public void ActivateLHCollider()
        {
            attackLHCollider.gameObject.SetActive(true);
        }

        public void DeactivateLHCollider()
        {
            attackLHCollider.gameObject.SetActive(false);
        }

    }
}