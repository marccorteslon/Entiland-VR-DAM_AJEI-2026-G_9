using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class TankBullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                SC_ScoreManager.instance.AddDiana();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}