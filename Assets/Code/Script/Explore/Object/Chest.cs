namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary>
    /// Represents a chest in the game that can be opened or closed.
    /// </summary>
    public class Chest : MonoBehaviour
    {
        private MeshFilter mesh;
        [SerializeField] Mesh Open, Close;
        private bool IsOpen = false;
        private bool IsPlayer = false;

        #region Getter Setter
        /// <summary>
        /// Sets the state of the chest (open or closed).
        /// </summary>
        /// <param name="open">True if the chest is open; false if closed.</param>
        public void SetOpenChest(bool open) => this.IsOpen = open;

        /// <summary>
        /// Gets the state of the chest (open or closed).
        /// </summary>
        /// <returns>True if the chest is open; false if closed.</returns>
        public bool GetOpen() => IsOpen;

        /// <summary>
        /// Sets whether the player is in proximity to the chest.
        /// </summary>
        /// <param name="player">True if the player is near the chest; false otherwise.</param>
        public void SetIsPlayer(bool player) => this.IsPlayer = player;

        /// <summary>
        /// Checks if the player is in proximity to the chest.
        /// </summary>
        /// <returns>True if the player is near the chest; false otherwise.</returns>
        public bool GetIsPlayer() => IsPlayer;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            mesh = GetComponent<MeshFilter>();
        }

        // Update is called once per frame
        void Update()
        {
            // Change the chest's mesh based on its open or closed state
            if (IsOpen)
            {
                mesh.mesh = Open;
            }
            else
            {
                mesh.mesh = Close;
            }            
        }           

        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                IsPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsPlayer = false;
            }
        }
    }    
}