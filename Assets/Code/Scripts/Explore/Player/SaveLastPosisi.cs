namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SaveLastPosisi : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField] SceneIndex Scenes = SceneIndex.Home;
        [Header("Players")]
        Transform Players;
        private static SaveLastPosisi instance;
        private bool isSave = false;
        public bool isTrigger;
        public void SetSave(bool isSave) => this.isSave = isSave;
        public bool GetSave() => isSave;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
        public static SaveLastPosisi GetInstance()
        {
            return instance;
        }
        // Start is called before the first frame update
        void Start()
        {
            Players = GetComponent<Transform>();
            isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.Alpha0)){
                PlayerPrefs.DeleteAll();
            }
            if(Input.GetKey(KeyCode.Alpha9)){
                isSave = true;
            }
            if (isTrigger)
            {
                switch (Scenes)
                {
                    case SceneIndex.Home:
                        ValuePosition(PlayerPrefs.GetFloat("X1"), PlayerPrefs.GetFloat("Y1"), PlayerPrefs.GetFloat("Z1"));
                        break;
                    case SceneIndex.HomeYard:
                        ValuePosition(PlayerPrefs.GetFloat("X2"), PlayerPrefs.GetFloat("Y2"), PlayerPrefs.GetFloat("Z2"));                        
                        break;
                    case SceneIndex.TrainingGround:
                        ValuePosition(PlayerPrefs.GetFloat("X3"), PlayerPrefs.GetFloat("Y3"), PlayerPrefs.GetFloat("Z3"));                        
                        break;
                }
            }
            if (isSave)
            {
                switch (Scenes)
                {
                    case SceneIndex.Home:
                        PositionUpdate(1);                        
                        break;
                    case SceneIndex.HomeYard:
                        PositionUpdate(2);
                        break;
                    case SceneIndex.TrainingGround:
                        PositionUpdate(3);
                        break;
                }
            }
        }

        void ValuePosition(float x, float y, float z)
        {
            Vector3 newPos = new Vector3(x, y, z);

            if (Players.position != newPos)
            {
                Players.position = newPos;
                isTrigger = false;
            }
        }

        void PositionUpdate(int value)
        {
            PlayerPrefs.SetFloat("X" + value, Players.position.x);
            PlayerPrefs.SetFloat("Y" + value, Players.position.y);
            PlayerPrefs.SetFloat("Z" + value, Players.position.z);
            PlayerPrefs.Save();
            Debug.Log("BERHASIL SAVE = " + PlayerPrefs.GetFloat("X1") + " " + PlayerPrefs.GetFloat("Y1") + " " + PlayerPrefs.GetFloat("Z1"));
            isSave = false;
        }
    }
    public enum SceneIndex
    {
        Home,
        HomeYard,
        TrainingGround
    }
}
