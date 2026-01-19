
using UnityEngine;


public class BulletDamage : MonoBehaviour
{
   private IDamageable damageable;
   [SerializeField] private GameObject hitParticlesPrefab;

   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out damageable))
        {
            damageable.TakeDamage(10);
            SpawnHitEffect(other);
        }
        
        Destroy(gameObject);

    }
   
    private void SpawnHitEffect(Collision collision)
    {
        if (hitParticlesPrefab != null)
        {
            // On récupère le point exact de l'impact
            ContactPoint contact = collision.contacts[0];
            
            GameObject effect = Instantiate(hitParticlesPrefab, contact.point, Quaternion.LookRotation(contact.normal));
            
            Destroy(effect, 2f); 
        }
    }
}
