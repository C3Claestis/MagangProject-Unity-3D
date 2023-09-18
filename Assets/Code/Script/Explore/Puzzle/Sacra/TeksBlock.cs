namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class TeksBlock : MonoBehaviour
    {
        private Text teks;
        private char huruf;
        public void SetHuruf(char huruf) => this.huruf = huruf;
        // Start is called before the first frame update
        void Start()
        {
            teks = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            teks.text = huruf.ToString();
        }
    }

}