using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
 //   [HideInInspector]
    public bool isInterect = false;
    float rotationSpeed = 5f;
    [SerializeField] GameObject bubbleText;
    Quaternion initialRotation;
    InteraksiNPC interaksiNPC;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        interaksiNPC = FindAnyObjectByType<InteraksiNPC>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(interaksiNPC.isTalk != true)
        {
            bubbleText.SetActive(isInterect);
        }
        else { bubbleText.SetActive(false); }
        

        if (!isInterect)
        {            
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);            
        }
    }    
}
