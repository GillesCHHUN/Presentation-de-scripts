using System;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{

    private IDamageablePlayer enemy;
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out enemy))
        {
            enemy.DamagePlayer(damage);
        }
    }
}
