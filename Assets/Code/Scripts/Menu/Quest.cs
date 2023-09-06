namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Quest : MonoBehaviour
    {

        [SerializeField] private string title;
        [SerializeField] private string type;
        [SerializeField] private string giver;
        [SerializeField] private string location;
        [SerializeField] private string description;
        [SerializeField] private string[,] objective; //Sementara
        [SerializeField] private string[,] reward; //Sementara


        //Buat setter getter nya kaya dibawah
        public void SetTitle(string title) => this.title = title;
        public string GetTitle() => title;
    }

}