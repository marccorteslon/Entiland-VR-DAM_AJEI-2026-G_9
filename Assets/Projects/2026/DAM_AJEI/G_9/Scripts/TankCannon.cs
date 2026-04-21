using TMPro;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class TankCannon : MonoBehaviour
    {
        [Header("Disparo")]
        public Transform firePoint;
        public GameObject bulletPrefab;
        public float bulletSpeed = 25f;
        public float bulletLifetime = 5f;
        public int bullets = 0;

        [Header("Referencias")]
        public SC_Tank tank;
        public TMP_Text txt_bullets;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Shoot()
        {
            // No dispares sin balas
            if (bullets <= 0) return;

            if (firePoint == null)
            {
                Debug.LogWarning("No hay firePoint asignado en TankCannon.");
                return;
            }

            if (bulletPrefab == null)
            {
                Debug.LogWarning("No hay bulletPrefab asignado en TankCannon.");
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed;
            }

            Destroy(bullet, bulletLifetime);

            tank.Shoot();

            _audioSource.Play();

            bullets--;
            if (bullets < 0) bullets = 0;
        }

        public void AddBullet()
        {
            bullets++;
            txt_bullets.text = bullets.ToString();
        }
        public void RemoveBullet()
        {
            bullets--;
            if (bullets < 0) { bullets = 0; }
            txt_bullets.text = bullets.ToString();
        }
    }
}