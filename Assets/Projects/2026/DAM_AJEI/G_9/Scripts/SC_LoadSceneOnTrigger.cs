using UnityEngine;
using UnityEngine.SceneManagement;
namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_LoadSceneOnTrigger : MonoBehaviour
    {
        [Header("Configuración")]
        public string sceneToLoad;
        public LayerMask playerLayer;

        private void OnTriggerEnter(Collider other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                LoadScene();
            }
        }

        void LoadScene()
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("No has asignado ninguna escena en sceneToLoad");
            }
        }
    }
}