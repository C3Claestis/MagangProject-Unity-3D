namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ProBuilder.Shapes;

    public class SaveLastPosisi : MonoBehaviour
    {
        [Header("Index Scene")]
        [SerializeField] int Scene;
        [Header("Players")]
        [SerializeField] Transform Players;

        private static SaveLastPosisi instance;
        private bool isSave = false;
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
        public static SaveLastPosisi GetInstance(){
            return instance;
        }
        // Start is called before the first frame update
        void Start()
        {
            switch (Scene)
            {
                case 1:
                    ValuePosition(PlayerPrefs.GetFloat("X1"), PlayerPrefs.GetFloat("Y1"), PlayerPrefs.GetFloat("Z1"));
                    break;
                case 2:
                    ValuePosition(PlayerPrefs.GetFloat("X2"), PlayerPrefs.GetFloat("Y2"), PlayerPrefs.GetFloat("Z2"));
                    break;
                case 3:
                    ValuePosition(PlayerPrefs.GetFloat("X3"), PlayerPrefs.GetFloat("Y3"), PlayerPrefs.GetFloat("Z3"));
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isSave)
            {
                switch (Scene)
                {
                    case 1:
                        PositionUpdate(1);
                        break;
                    case 2:
                        PositionUpdate(2);
                        break;
                    case 3:
                        PositionUpdate(3);
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerPrefs.DeleteAll();
            }
        }

        void ValuePosition(float x, float y, float z)
        {
            Vector3 newPos = new Vector3(x, y, z);

            if (newPos != Vector3.zero)
            {
                Players.position = newPos;
                Debug.Log("Loading position from last save location!");
            }
        }

        void PositionUpdate(int value)
        {
            PlayerPrefs.SetFloat("X" + value, Players.position.x);
            PlayerPrefs.SetFloat("Y" + value, Players.position.y);
            PlayerPrefs.SetFloat("Z" + value, Players.position.z);
            PlayerPrefs.Save();
            isSave = false;
        }
    }
}
