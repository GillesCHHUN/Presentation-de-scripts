using System;
using System.Collections;
using UnityEngine;


namespace Player.Core
{
    public class Shoot : MonoBehaviour
    { 
        [SerializeField] private WeaponDataSO weaponDataSO;
        private InputReader inputReader;
        [SerializeField] private ObjectPool bulletToPool;
        private PlayerHealth playerHealth;
        [SerializeField] private Transform firePoint;
        //[SerializeField] private float bulletSpeed;

        //[SerializeField] private float bulletPerSec = 5; // buller per second
        private float lastTimeShoot;


        private void Awake()
        {
            inputReader = GetComponentInParent<InputReader>();
            playerHealth = GetComponentInParent<PlayerHealth>();
        }

        private void Update()
        {
            if (playerHealth.IsDead) return;
            
            PlayerShoot();
        }

        private void PlayerShoot()
        {
            if (ReadyToShoot() && inputReader.IsShooting)
            {
                CreateBullet();
            }
        }

        private void CreateBullet()
        {
            GameObject bullet = bulletToPool.GetObject();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * weaponDataSO.bulletSpeed;
            StartCoroutine(DeactivateBullet(bullet));
        }

        private bool ReadyToShoot()
        {
            if (Time.time > lastTimeShoot + 1 / weaponDataSO.bulletPerSec)
            {
                lastTimeShoot = Time.time;
                return true;
            }

            return false;
        }

        private IEnumerator DeactivateBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2f);
            bulletToPool.ReturnObject(bullet);
        }
    }
}