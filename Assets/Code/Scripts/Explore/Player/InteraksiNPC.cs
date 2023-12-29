namespace Nivandria.Explore
{    
    using UnityEngine;

    /// <summary>
    /// Handles interactions with non-player characters (NPCs).
    /// </summary>
    public class InteraksiNPC : MonoBehaviour
    {
        #region Variabel
        [SerializeField] LayerMask layerMask; // Layer mask for raycasting interactions        
        [SerializeField] Transform player; // Reference to the player's transform
        RaycastHit raycast; // Stores raycast hit information
        float rotationSpeed = 5.0f; // Rotation speed for NPCs
        private bool isTalk, isNPC;
        #endregion

        #region Getter Setter
        public void SetIsTalk(bool istalk) => this.isTalk = istalk; // Setter for conversation flag
        public void SetIsNPC(bool isnpc) => this.isNPC = isnpc; // Setter for NPC interaction flag        
        public bool GetIsTalk() => isTalk; // Getter for conversation flag
        public bool GetIsNPC() => isNPC; // Getter for NPC interaction flag 
        #endregion

        private void Update()
        {
            Interaksi();           
        }

        private void Interaksi()
        {
            // Raycast to detect NPCs in front of the player
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

            if (Physics.Raycast(ray, out raycast, 1f, layerMask, QueryTriggerInteraction.Ignore))
            {
                // Draw a debug ray
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycast.distance, Color.red);
                // Get the NPC component if one exists
                NPC npc = raycast.collider.GetComponent<NPC>();
                // If an NPC is detected
                if (npc != null)
                {
                    // If the NPC is not currently interacting with the player
                    if (npc.GetInterect() == false)
                    {
                        npc.SetInterect(true);
                        isNPC = true;
                    }
                }
                // Rotate towards the player when the interaction button is pressed
                if (isTalk)
                {
                    RotateTowardsPlayer(raycast.collider.gameObject);
                    npc.SetTalk(true);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.green);
                // Reset interaction flags for all NPCs in the scene
                NPC[] npcs = FindObjectsOfType<NPC>();
                foreach (NPC npc in npcs)
                {
                    npc.SetInterect(false);
                    npc.SetTalk(false);
                    isNPC = false;
                }
            }
        }
        
        // Rotate the NPC to face the player
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
}