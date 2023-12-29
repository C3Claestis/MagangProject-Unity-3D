namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;

    /// <summary>
    /// Allows locking the player's position to a specific location and switching between a player object and a clone object.
    /// </summary>
    public class LockPosisi : MonoBehaviour
    {
        [SerializeField] GameObject player;  // The player GameObject.
        [SerializeField] GameObject clone;   // The clone GameObject.
        private bool isLocked = false;       // Indicates whether the player's position is locked.
        private bool isActive = false;       // Indicates whether the trigger zone is active.

        /// <summary>
        /// Handles the input condition to lock/unlock the player's position and switch between player and clone objects.
        /// </summary>
        public void InputKondisi()
        {
            if (isActive)
            {
                if (isLocked)
                {
                    clone.SetActive(false);       // Deactivate the clone object.
                    player.SetActive(true);        // Activate the player object.
                    isLocked = false;              // Unlock the player's position.
                }
                else
                {
                    player.transform.position = transform.position;  // Move the player to this position.
                    clone.SetActive(true);        // Activate the clone object.
                    player.SetActive(false);       // Deactivate the player object.
                    isLocked = true;               // Lock the player's position.
                }
            }
        }

        /// <summary>
        /// Called when another Collider enters the trigger zone.
        /// </summary>
        /// <param name="other">The Collider that entered the trigger zone.</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isActive = true;  // The trigger zone is active when the player is inside.
            }
        }

        /// <summary>
        /// Called when another Collider exits the trigger zone.
        /// </summary>
        /// <param name="other">The Collider that exited the trigger zone.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isActive = false;  // The trigger zone is no longer active when the player exits.
            }
        }
    }
}