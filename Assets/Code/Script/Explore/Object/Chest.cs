namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Chest : MonoBehaviour
    {
        MeshFilter mesh;
        [SerializeField] Mesh Open, Close;
        private bool IsOpen = false;
        private bool IsPlayer = false;
        #region Getter Setter
        public void SetOpenChest(bool open) => this.IsOpen = open;
        public bool GetOpen() => IsOpen;
        public void SetIsPlayer(bool player) => this.IsPlayer = player;
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
            // Mengubah mesh chest sesuai kondisi IsOpen
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