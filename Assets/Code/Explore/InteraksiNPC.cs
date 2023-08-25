using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteraksiNPC : MonoBehaviour
{
    public Text _name;
    bool isDetek = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (isDetek)
            {
                Debug.Log(other.name);
                other.GetComponent<NPC>().isInterect = true;
                _name.text = other.name;
            }            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {            
            Debug.Log(other.name);
            other.GetComponent<NPC>().isInterect = false;
            _name.text = "";
            isDetek = true;
        }
    }
}
