namespace Nivandria.Explore.Puzzle
{   
    using UnityEngine;

    public class LockPosisi : MonoBehaviour
    {
        [SerializeField] GameObject player, clone;
        private bool isLocked = false;
        private bool isActive = false;
     
        public void InputKondisi()
        {
            if (isActive)
            {
                if (isLocked)
                {
                    clone.SetActive(false);
                    player.SetActive(true);
                    isLocked = false;
                }
                else
                {
                    player.transform.position = transform.position;
                    clone.SetActive(true);
                    player.SetActive(false);
                    isLocked = true;
                }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("IN");
                isActive = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("EXIT");
                isActive = false;
                ;
            }
        }
    }
}