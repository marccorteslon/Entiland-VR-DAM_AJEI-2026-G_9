using UnityEngine;


namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SC_CameraRenderer : MonoBehaviour
    {
        [Header("References")]
        public Camera sourceCamera;     // Cámara que renderiza
        public int textureWidth = 1024;
        public int textureHeight = 1024;

        private RenderTexture renderTexture;
        private MeshRenderer meshRenderer;

        void Start()
        {
            if (sourceCamera == null)
            {
                Debug.LogError("No source camera assigned!");
                return;
            }

            meshRenderer = GetComponent<MeshRenderer>();

            
            renderTexture = new RenderTexture(textureWidth, textureHeight, 16);
            renderTexture.Create();

            
            sourceCamera.targetTexture = renderTexture;

            
            meshRenderer.material.mainTexture = renderTexture;
        }

        void OnDestroy()
        {
            if (renderTexture != null)
            {
                renderTexture.Release();
            }
        }
    }
}