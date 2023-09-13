namespace Nivandria.UI.Keys
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine.InputSystem;
    using Nivandria.UI.Archive;

    public class KeysLogManager : MonoBehaviour
    {
        public static KeysLogManager Instance{get; private set;}
        [SerializeField] Transform contentLeft;
        [SerializeField] Transform contentRight;
        [SerializeField] Image iconKey;
        [SerializeField] TextMeshProUGUI nameKey;
        [SerializeField] TextMeshProUGUI descriptionKey;



        [SerializeField] List<Keys> keysList = new List<Keys>();
        [SerializeField] GameObject keysLog;

        private List<KeysLog> keyLogList;
        private KeysLog selectedKeyLog;

        public KeysLog GetSelectedKeyLog() => selectedKeyLog;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        void Start()
        {
            GameObject newKey;
            keyLogList = new List<KeysLog>();

            for (int i = 0; i < keysList.Count; i++)
            {
                if (i/2 == 1)
                {
                    newKey = Instantiate(keysLog, contentRight);
                    newKey.GetComponent<KeysLog>().SetKeyDetail(keysList[i]);
                }
                else
                {
                    newKey = Instantiate(keysLog, contentLeft);
                    newKey.GetComponent<KeysLog>().SetKeyDetail(keysList[i]);
                }

                keyLogList.Add(newKey.GetComponent<KeysLog>());

                //Button keyButton = newKey.GetComponent<Button>();
                
            }

            SetSelectedKeyLog(keyLogList[0]);
            
        }

        
        void Update()
        {

        }

        public void UpdateVisualKeyLog()
        {
            foreach (KeysLog key in keyLogList)
            {
                key.UpdateVisual();
            }
        }

        public void SetSelectedKeyLog(KeysLog keyLog)
        {
            selectedKeyLog = keyLog;
            nameKey.text = selectedKeyLog.GetKeys().GetNameKey();
            descriptionKey.text = selectedKeyLog.GetKeys().GetDescriptionKey();
            UpdateVisualKeyLog();
        }



    }

}