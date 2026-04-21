using UnityEngine;
using UnityEngine.SceneManagement;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_Gun : MonoBehaviour
    {
        public LayerMask layer_player;
        public float shoot_force = 10;
        private AudioSource _audioSource;
        private Rigidbody _rb;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Shoot()
        {
            _audioSource.Play();

            _rb.AddForce(-transform.forward * shoot_force, ForceMode.Impulse);

            if (Physics.SphereCast(transform.position, 0.01f, transform.forward, out RaycastHit hit, layer_player))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
