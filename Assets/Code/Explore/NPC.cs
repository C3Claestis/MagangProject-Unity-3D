using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [HideInInspector]
    public bool isInterect = false;

    [SerializeField] GameObject bubbleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bubbleText.SetActive(isInterect);        
    }    
}
