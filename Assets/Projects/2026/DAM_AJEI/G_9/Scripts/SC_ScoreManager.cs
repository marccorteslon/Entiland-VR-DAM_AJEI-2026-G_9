using System.Collections;
using TMPro;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_ScoreManager : MonoBehaviour
    {
        public static SC_ScoreManager instance { get; private set; }
        private int dianas = 0;
        public TMP_Text txt_diana;

        public GameObject scene;
        private GameObject current_scene;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);

            current_scene = Instantiate(scene);

            txt_diana = GameObject.Find("Diana amount text").GetComponent<TMP_Text>();
        }

        public void EndGame()
        {
            SC_Gun.instance.LerpVignette();
            Destroy(current_scene);
            current_scene = Instantiate(scene);
        }
        private IEnumerator EndGameRoutine()
        {
            yield return new WaitForSeconds(2);

        }

        public void AddDiana()
        {
            dianas++;
            txt_diana.text = dianas.ToString();

            if (dianas >= 10)
            {
                EndGame();
            }
        }
    }
}