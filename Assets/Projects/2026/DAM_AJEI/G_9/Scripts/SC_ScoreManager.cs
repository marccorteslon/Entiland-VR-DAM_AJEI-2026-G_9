using TMPro;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_ScoreManager : MonoBehaviour
    {
        public static SC_ScoreManager instance { get; private set; }
        private int dianas = 0;
        public TMP_Text txt_diana;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        public void AddDiana()
        {
            dianas++;
            txt_diana.text = dianas.ToString();
        }
    }
}