using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Diana"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}