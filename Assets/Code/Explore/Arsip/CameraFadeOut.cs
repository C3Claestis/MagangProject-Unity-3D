using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeOut : MonoBehaviour
{
    private ObjectFedOut objectFed;
    GameObject players;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(players != null)
        {
            Vector3 dir = players.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;

                if(hit.collider.gameObject == players)
                {
                    if(objectFed != null)
                    {
                        objectFed.isTransparent = false;
                    }
                }
                else
                {
                    objectFed = hit.collider.gameObject.GetComponent<ObjectFedOut>();
                    if(objectFed != null)
                    {
                        objectFed.isTransparent = true;
                    }
                }
            }
        }
    }
}
