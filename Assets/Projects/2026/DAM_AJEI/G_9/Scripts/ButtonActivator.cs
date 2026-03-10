using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{

    public class SC_ButtonActivator : MonoBehaviour
    {
        public SC_ObjectReceiver receiver;

        [Header("Audio")]
        public AudioClip activationSound;
        private AudioSource audioSource;

        void Awake()
        {
            // Creamos un AudioSource si no hay uno
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void OnButtonPressed()
        {
            if (receiver != null)
            {
                // Activamos y comprobamos si fue exitoso
                bool success = receiver.Activate();
                if (success && activationSound != null)
                {
                    audioSource.PlayOneShot(activationSound);
                }
            }
        }
    }
}