
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
            
            // On crée l'effet à la position du contact, orienté selon la normale (la face touchée)
            GameObject effect = Instantiate(hitParticlesPrefab, contact.point, Quaternion.LookRotation(contact.normal));
            
            // On détruit l'effet automatiquement après 2 secondes pour ne pas polluer la hiérarchie
            Destroy(effect, 2f); 
        }
    }
}
