using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{

    public class SC_ObjectReceiver : MonoBehaviour
    {
        public bool objectInserted = false;
        public GameObject storedObject;
        public Transform teleportTarget;
        public TankCannon tankCannon;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!objectInserted)
            {
                storedObject = other.gameObject;

                storedObject.SetActive(false);
                // Añade una bala a la recámara
                tankCannon.AddBullet();

                _audioSource.Play();

                Debug.Log("Objeto insertado correctamente.");
            }
        }

        // Ahora devolvemos un bool indicando si la activación fue exitosa
        public bool Activate()
        {
            if (objectInserted && storedObject != null)
            {
                storedObject.transform.position = teleportTarget.position;
                storedObject.SetActive(true);

                objectInserted = false;
                storedObject = null;

                Debug.Log("Sistema activado correctamente. Objeto movido.");
                return true;
            }
            else
            {
                Debug.Log("No puedes activar el sistema. No hay objeto.");
                return false;
            }
        }
    }
}