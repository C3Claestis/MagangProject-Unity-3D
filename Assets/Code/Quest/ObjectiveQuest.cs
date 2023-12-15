namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    public class ObjectiveQuest : MonoBehaviour
    {
        [SerializeField] GameObject[] _objects;
        [SerializeField] Text teks, potatoteks;
        private static ObjectiveQuest instance;                
        private int potatoandwater;
        public int GetPotatoAndWater() => potatoandwater;
        public void SetPotatoAndWater(int potatoandwater) => this.potatoandwater = potatoandwater;
        public static ObjectiveQuest GetInstance(){
            return instance;
        }
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            //Kentang
            Instantiate(_objects[0]);
            Instantiate(_objects[1]);
            Instantiate(_objects[2]);
            Instantiate(_objects[3]);
            Instantiate(_objects[4]);
            //Bucket
            Instantiate(_objects[5]);
            Instantiate(teks, GameObject.Find("Title Quest").transform);            
            potatoteks = GameObject.Find("CountPotato(Clone)").GetComponent<Text>();
        }
        void Update()
        {                        
            potatoteks.text = "("+potatoandwater.ToString()+"/6)";
        }
    }
}