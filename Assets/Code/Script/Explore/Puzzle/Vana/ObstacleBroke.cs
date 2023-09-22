namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class ObstacleBroke : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        private bool isDeteksi = false;
        private bool isBroke = false;
        public bool GetIsBroke() => isBroke;
        public void InputKondisi(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (isDeteksi) { panel.SetActive(true); }                
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                isDeteksi = true;
                isBroke = true;                           
            }
        }
        private void OnTriggerExit(Collider other)
        {            
            if (other.gameObject.tag == "Player")
            {
                isDeteksi = false;
                isBroke = false;
            }
        }
    }

}