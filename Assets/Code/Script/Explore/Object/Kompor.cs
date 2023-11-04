namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class Kompor : MonoBehaviour
    {
        [SerializeField] GameObject icon;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("ENTER");
                icon.SetActive(true);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("EXIT");
                icon.SetActive(false);
            }
        }
    }
}