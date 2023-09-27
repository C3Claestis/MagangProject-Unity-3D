namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class TeksBlock : MonoBehaviour
    {
        [SerializeField] string nilai_benar;
        private Text teks;
        private string huruf;
        private bool isCorrect = false;
        private bool isIsi = false;
        public void SetHuruf(string huruf) => this.huruf = huruf;
        public bool GetIsCorrect() => isCorrect;
        public bool GetIsIsi() => isIsi;
        // Start is called before the first frame update
        void Start()
        {            
            teks = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            teks.text = huruf;

            if (nilai_benar == huruf)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }

            if(string.IsNullOrEmpty(huruf))
            {
                isIsi = false;
            }
            else
            {
                isIsi = true;
            }
        }
    }
}