using UnityEngine;

public class TankCannon : MonoBehaviour
{
    [Header("Disparo")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 25f;
    public float bulletLifetime = 5f;

    public void Shoot()
    {
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
    }
}