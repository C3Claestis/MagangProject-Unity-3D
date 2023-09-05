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
    public Text interaksi;
    private bool isTalk, isNPC;    
    private void Update()
    {
        Interaksi();   
    }

    private void Interaksi()
    {
        //Raycast untuk scan NPC di depan
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out raycast, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            //Untuk membuat line 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycast.distance, Color.red);
            //Mengambil komponen script NPCnya
            NPC npc = raycast.collider.GetComponent<NPC>();
            //Jika NPCnya ada didepannya
            if (npc != null)
            {
                //Jika NPC tidak sedang berinteraksi dengan player
                if (npc.GetInterect() == false)
                {
                    npc.SetInterect(true);                    
                    text.text = raycast.collider.gameObject.name;                    
                    isNPC = true;                    
                }
            }
            //Kondisi untuk rotasi ketika di tekan button interaksi di keyboard
            if (isTalk) { RotateTowardsPlayer(raycast.collider.gameObject); npc.SetTalk(true); }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.green);
            text.text = "";            
            //Mengambil semua komponen NPC yang ada untuk mengembalikan nilai awalnya
            NPC[] npcs = FindObjectsOfType<NPC>();
            foreach (NPC npc in npcs)
            {
                npc.SetInterect(false);
                npc.SetTalk(false);
                isNPC = false;
            }
        }
    }
    //Untuk mengatur rotasi agar NPC melihat ke arah player
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
    public void SetIsTalk(bool istalk) => this.isTalk = istalk;
    public void SetIsNPC(bool isnpc) => this.isNPC = isnpc;
    public bool GetIsTalk() => isTalk;
    public bool GetIsNPC() => isNPC;
}
