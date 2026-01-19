using UnityEngine;

namespace Player.Core
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        
        [SerializeField] private Collider playerCollider;

        public void DeactivateCollider()
        {
            playerCollider.isTrigger = true;
        }
        
    }
}