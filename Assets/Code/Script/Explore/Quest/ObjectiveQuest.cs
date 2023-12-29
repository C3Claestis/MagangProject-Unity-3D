namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Quest;
    using UnityEngine;
    using UnityEngine.UI;

    public class ObjectiveQuest : MonoBehaviour
    {
        [SerializeField] GameObject[] _objects;
        [SerializeField] Text teks; 
        Text potatoteks;
        Animator transisi;
        private int potatoandwater;

        [Header("Quest 1 Or Not?")]
        [SerializeField] QuestOneOrNot Quest;
        #region Get And Set
        private static ObjectiveQuest instance;
        public int GetPotatoAndWater() => potatoandwater;
        public void SetPotatoAndWater(int potatoandwater) => this.potatoandwater = potatoandwater;
        public static ObjectiveQuest GetInstance()
        {
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
        #endregion
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
            if (transisi == null) { transisi = GameObject.Find("Transisi").GetComponent<Animator>(); }
            else { return; }
        }
        void Update()
        {
            if (potatoandwater >= 6)
            {
                if (Quest == QuestOneOrNot.Quest_1)
                {
                    transisi.SetTrigger("Dialog");
                    HandleQuest.GetInstance().Mision1 = true;                    
                    Destroy(potatoteks);
                    Destroy(gameObject);
                }               
            }
            potatoteks.text = "(" + potatoandwater.ToString() + "/6)";
        }
    }
}