using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_LookAtTarget : MonoBehaviour
    {
        public Transform target;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (target != null)
                transform.LookAt(target);
        }
    }
}
