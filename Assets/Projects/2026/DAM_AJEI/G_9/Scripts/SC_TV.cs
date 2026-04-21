using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_TV : MonoBehaviour
    {
        private bool _opened = false;
        private Vector3 _start_size;

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _start_size = transform.localScale;
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
        }
        public void OpenTv()
        {
            if (_opened) return;
            _opened = true;

            transform.localScale = Vector3.zero;
            _meshRenderer.enabled = false;

            StartCoroutine(OpenTvRoutine());
        }
        private IEnumerator OpenTvRoutine()
        {
            float duration = 1;
            float timer = 0;

            while (timer < duration)
            {
                float t = timer / duration;
                t = Mathf.SmoothStep(0, 1, t);

                transform.localScale = Vector3.Lerp(Vector3.zero, _start_size, t);

                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = _start_size;
        }
    }
}
