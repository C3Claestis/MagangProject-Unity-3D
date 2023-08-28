using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteraksiNPC : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Text text;
    [SerializeField] Transform player;
    RaycastHit raycast;
    float rotationSpeed = 5.0f;
    public bool isTalk;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if(Physics.Raycast(ray, out raycast, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycast.distance, Color.red);
            string objectName = raycast.collider.gameObject.name;
            Debug.Log("Object Name: " + objectName);            
            NPC npc = raycast.collider.GetComponent<NPC>();
            if (npc != null)
            {
                if (!npc.isInterect)
                {                    
                    npc.isInterect = true;
                    text.text = raycast.collider.gameObject.name;
                    Debug.Log("isInteract: " + npc.isInterect);                    
                }
            }
            if (isTalk) { RotateTowardsPlayer(raycast.collider.gameObject); }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.green);
            text.text = "";
            NPC[] npcs = FindObjectsOfType<NPC>();
            foreach (NPC npc in npcs)
            {
                npc.isInterect = false;
            }           
        }
    }
    private void RotateTowardsPlayer(GameObject obj)
    {
        if (player != null)
        {
            Vector3 lookDirection = player.position - obj.transform.position;
            lookDirection.y = 0; // Optional if you don't want vertical rotation
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }   
}
