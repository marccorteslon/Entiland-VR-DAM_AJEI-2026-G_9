using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_Gun : MonoBehaviour
    {
        public static SC_Gun instance { get; private set; }
        public LayerMask layer_player;
        public float shoot_force = 10;

        [Header("Post Process")]
        public Volume postProcessVolume;
        private Vignette vignette;

        private AudioSource _audioSource;
        private Rigidbody _rb;

        private void Awake()
        {
            instance = this;

        }
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
                LerpVignette();
            }

            if (Physics.SphereCast(transform.position, 0.2f, transform.forward, out RaycastHit hit, layer_player))
            {
                SC_ScoreManager.instance.EndGame();
            }
        }
        public void LerpVignette()
        {
            StartCoroutine(LerpVignetteRoutine());
        }
        private IEnumerator LerpVignetteRoutine()
        {
            float duration = 1;
            float timer = 0;

            while (timer < duration)
            {
                float t = timer / duration;
                t = Mathf.SmoothStep(0, 1, t);

                vignette.intensity.value = t;

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}