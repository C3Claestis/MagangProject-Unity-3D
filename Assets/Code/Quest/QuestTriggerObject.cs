namespace Nivandria.Explore
{
    using UnityEngine;

    public class QuestTriggerObject : MonoBehaviour
    {
        #region Variabel
        private static QuestTriggerObject instance;
        [SerializeField] LayerMask layerMask; // Layer mask for raycasting interactions        
        [SerializeField] Transform player; // Reference to the player's transform
        RaycastHit raycast; // Stores raycast hit information
        float rotationSpeed = 5.0f; // Rotation speed for NPCs
        private bool isQuest, isDetect;
        #endregion

        #region Getter Setter
        public void SetIsQuest(bool isQuest) => this.isQuest = isQuest; // Setter for conversation flag
        public void SetIsDetect(bool isDetect) => this.isDetect = isDetect; // Setter for NPC interaction flag        
        public bool GetIsQuest() => isQuest; // Getter for conversation flag
        public bool GetIsDetect() => isDetect; // Getter for NPC interaction flag 
        #endregion

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
        public static QuestTriggerObject GetInstance()
        {
            return instance;
        }
        void Update()
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
                NPCQuest npc = raycast.collider.GetComponent<NPCQuest>();
                // If an NPC is detected
                if (npc != null)
                {
                    // If the NPC is not currently interacting with the player
                    if (npc.GetInterect() == false)
                    {
                        npc.SetInterect(true);
                        isDetect = true;
                    }
                }
                // Rotate towards the player when the interaction button is pressed
                if (isQuest)
                {
                    RotateTowardsPlayer(raycast.collider.gameObject);
                    npc.SetTalk(true);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.green);
                // Reset interaction flags for all NPCs in the scene
                NPCQuest[] npcs = FindObjectsOfType<NPCQuest>();
                foreach (NPCQuest npc in npcs)
                {
                    npc.SetInterect(false);
                    npc.SetTalk(false);
                    isDetect = false;
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