using UnityEngine;

namespace Autohand.Demo
{
    public class SC_ReloadButton : MonoBehaviour
    {
        [Header("Key System")]
        public PlacePoint placePoint;

        [Header("Key Spawn")]
        public GameObject keyPrefab;
        public Transform keySpawnPoint;

        private bool keyInserted = false;
        private bool buttonUsed = false;

        void Start()
        {
            placePoint.OnPlaceEvent += OnKeyInserted;
        }

        void OnKeyInserted(PlacePoint point, Grabbable grab)
        {
            Debug.Log("EVENTO PLACEPOINT FUNCIONA");
            keyInserted = true;
            buttonUsed = false;
        }

        void SpawnKey()
        {
            if (keyPrefab != null && keySpawnPoint != null)
            {
                Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
                Debug.Log("Nueva llave spawneada");
            }
        }

        public void PressButton()
        {
            if (!keyInserted)
            {
                Debug.Log("No puedes usar el botón, no hay llave");
                return;
            }

            if (buttonUsed)
            {
                Debug.Log("No puedes usar el botón, ya fue usado");
                return;
            }

            Debug.Log("Botón accionado");

            buttonUsed = true;
        }
    }
}