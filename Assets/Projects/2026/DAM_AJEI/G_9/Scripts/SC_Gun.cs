using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_Gun : MonoBehaviour
    {
        public LayerMask layer_player;
        public float shoot_force = 10;

        [Header("Post Process")]
        public Volume postProcessVolume;
        private Vignette vignette;

        private AudioSource _audioSource;
        private Rigidbody _rb;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody>();

            // Obtener el vignette del volume
            if (postProcessVolume != null && postProcessVolume.profile.TryGet(out vignette))
            {
                vignette.intensity.value = 0f; 
            }
        }

        public void Shoot()
        {
            _audioSource.Play();

            _rb.AddForce(-transform.forward * shoot_force, ForceMode.Impulse);

            // Activar vignette al disparar
            if (vignette != null)
            {
                vignette.intensity.value = 1f; 
            }

            if (Physics.SphereCast(transform.position, 0.25f, transform.forward, out RaycastHit hit, layer_player))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}