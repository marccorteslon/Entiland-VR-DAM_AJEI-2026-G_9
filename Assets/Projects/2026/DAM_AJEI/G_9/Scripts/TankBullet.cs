using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
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
}