using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class NPC : MonoBehaviour
{
    [SerializeField] GameObject bubbleText;
    [SerializeField] bool isPatrol;    
    float rotationSpeed = 5f;    
    SplineFollower splineFollower;
    Quaternion initialRotation;    
    Animator anim;
    private bool isTalk = false;
    private bool isInterect = false;
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
        bubbleText.SetActive(isInterect);
        //Jika NPC sedang interaksi
        if (isTalk != false)
        {                                 
            if (isPatrol)
            {
                anim.SetBool("isTalk", true);
                splineFollower.follow = false;
            }
        }
        //Jika NPC tidak sedang interaksi
        else 
        {             
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

    public void SetTalk(bool talk) => this.isTalk = talk;
    public void SetInterect(bool interect) => this.isInterect = interect;

    public bool GetInterect() => isInterect;
}
