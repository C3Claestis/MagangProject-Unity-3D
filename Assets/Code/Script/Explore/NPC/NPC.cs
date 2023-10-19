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
                
        #region Getter Setter
        public void SetTalk(bool talk) => this.isTalk = talk;
        public void SetInterect(bool interect) => this.isInterect = interect;
        public bool GetInterect() => isInterect;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            splineFollower = GetComponent<SplineFollower>();
            anim = GetComponent<Animator>();
            initialRotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            //Jika NPC sedang interaksi
            if (isTalk != false)
            {
                bubbleText.SetActive(false);
                DialogueManager.GetInstance().EnterDialogMode(inkJSON, value_dialogue);
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
    }
}