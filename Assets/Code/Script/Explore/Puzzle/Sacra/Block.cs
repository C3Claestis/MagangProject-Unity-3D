namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Block : MonoBehaviour
    {        
        [SerializeField] string char1, char2, char3, char4; 
        [SerializeField] TeksBlock teks;
        [SerializeField] LayerMask layerMask;        
        [SerializeField] Transform blok;        
        [SerializeField] GameObject material;
        RaycastHit raycast;
        int count = 0;
        float range = 2;
        private bool isRotating = false;
        private bool isReset = false;
        private bool isDeteksi = false;
        private float targetRotation = 0f;
        private float rotationSpeed = 90f;

        public void SetCount(int count) => this.count = count;
        public void SetIsReset(bool reset) => this.isReset = reset;
        // Update is called once per frame
        void Update()
        {
            Scan();
            if (isReset)
            {
                blok.rotation = Quaternion.Euler(0, 0, 0);
                teks.SetHuruf("");
                teks.SetHuruf(null);
                isReset = false;
                count = 0;
            }
            if (isRotating)
            {
                Mutar();
            }
            
            if(targetRotation == 360)
            {
                targetRotation = 0;
            }
            switch (count)
            {                
                case 1:
                    teks.SetHuruf(char1);
                    break;
                case 2:
                    teks.SetHuruf(char2);
                    break;
                case 3:
                    teks.SetHuruf(char3);                   
                    break;
                case 4:
                    teks.SetHuruf(char4);
                    break;
            }
        }
        public void InputScan()
        {
            if (isDeteksi)
            {
                if (!isRotating)
                {
                    // Mengatur target rotasi 90 derajat lebih tinggi dari rotasi saat ini
                    targetRotation = blok.eulerAngles.y + 90f;
                    isRotating = true;
                }
            }            
        }
        /// <summary>
        /// Untuk Scan Jika Ada Player Maka Bisa Melakukan Interaksi
        /// </summary>
        void Scan()
        {
            //Raycast untuk scan NPC di depan
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * -1));

            if (Physics.Raycast(ray, out raycast, range, layerMask, QueryTriggerInteraction.Ignore))
            {
                //Untuk membuat line 
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * raycast.distance, Color.red);
                material.SetActive(true);
                isDeteksi = true;

            }
            else
            {
                isDeteksi = false;
                material.SetActive(false);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * range, Color.green);
            }

        }
        /// <summary>
        /// Untuk Memutar Objek Blok
        /// </summary>
        void Mutar()
        {
            if (isRotating)
            {
                // Melakukan rotasi secara smooth
                float step = rotationSpeed * Time.deltaTime;
                blok.rotation = Quaternion.RotateTowards(blok.rotation, Quaternion.Euler(0f, targetRotation, 0f), step);
                
                // Memeriksa apakah rotasi selesai
                if (Mathf.Abs(blok.eulerAngles.y - targetRotation) < 0.01f)
                {                    
                    isRotating = false;
                    if (count == 4)
                    {
                        count = 1;
                    }
                    else { count++; }
                }
            }
        }
    }

}