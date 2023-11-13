namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Dreamteck.Splines;    

    public class NPC : MonoBehaviour
    {
        [Header("Deteksi Interaksi")]
        [SerializeField] GameObject bubbleText;
        [SerializeField] bool isPatrol;

        [Header("Dialogue Value")]
        [SerializeField] TextAsset inkJSON;
        [SerializeField] int value_dialogue;
        float rotationSpeed = 5f;
        SplineFollower splineFollower;
        Quaternion initialRotation;
        Animator anim;
        private bool isTalk = false;
        private bool isInterect = false;

        [Header("InkExternal")]
        private InkExternal inkExternal;
        [SerializeField] Vector3[] Axis_Posisi;
        [SerializeField] Quaternion[] Axis_Rotasi;

        #region Getter Setter
        public void SetTalk(bool talk) => this.isTalk = talk;
        public void SetInterect(bool interect) => this.isInterect = interect;
        public bool GetInterect() => isInterect;
        #endregion

        //Kamera Posisi = 3, 0.5f, -2.5f;
        //Kamera rotasi = 15, -45, 0;
        // Start is called before the first frame update
        void Start()
        {
            splineFollower = GetComponent<SplineFollower>();
            anim = GetComponent<Animator>();
            initialRotation = transform.rotation;
            inkExternal = FindAnyObjectByType<InkExternal>();
        }

        // Update is called once per frame
        void Update()
        {
            //Jika NPC sedang interaksi
            if (isTalk)
            {
                bubbleText.SetActive(false);
                DialogueManager.GetInstance().EnterDialogMode(inkJSON, value_dialogue);
                
                switch (inkExternal.GetAvatar())
                {
                    case 1:
                        Debug.Log("BERHASIL AMBIL " + 1);
                        SearchImage(true, false);
                        break;
                    case 2:
                        Debug.Log("BERHASIL AMBIL " + 2);
                        SearchImage(false, true);
                        break;
                    case 3:
                        Debug.Log("BERHASIL AMBIL " + 3);
                        SearchImage(true, true);
                        break;                                        
                }
                if (isPatrol)
                {
                    anim.SetBool("isTalk", true);
                    splineFollower.follow = false;
                }
            }
            //Jika NPC tidak sedang interaksi
            else
            {
                bubbleText.SetActive(isInterect);
                if (isPatrol)
                {
                    anim.SetBool("isTalk", false);
                    splineFollower.follow = true;
                }
            }

            //Jika sedang tidak ada player yang Raycast
            if (!isInterect)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
            }
        }

        void SearchImage(bool Image_1, bool Image_2)
        {
            GameObject image1 = GameObject.Find("Avatar-1");
            GameObject image2 = GameObject.Find("Avatar-2");

            // Cari parent GameObject yang berisi child bernama "Avatar-1"
            Transform childAvatar1 = image1.transform.Find("Avatar");
            Transform childAvatar2 = image2.transform.Find("Avatar");

            if (childAvatar1 != null)
            {
                childAvatar1.gameObject.SetActive(Image_1);
            }
            if (childAvatar2 != null)
            {
                childAvatar2.gameObject.SetActive(Image_2);
            }
        }
    }
}