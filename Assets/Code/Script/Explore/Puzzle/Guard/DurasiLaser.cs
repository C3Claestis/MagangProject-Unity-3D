namespace Nivandria.Explore.Puzzle
{    
    using UnityEngine;

    public class DurasiLaser : MonoBehaviour
    {
        [SerializeField] Laser laser;
        private float Durasi;        
        private bool isActive = false;
        public void SetDurasi(float tambahDurasi)
        {
            // Tambahkan durasi sesuai dengan parameter yang diberikan
            Durasi += tambahDurasi;
            // Pastikan Durasi tidak kurang dari 0
            Durasi = Mathf.Max(0, Durasi);
        }

        void Update()
        {
            if(Durasi > 0)
            {
                Durasi -= Time.deltaTime;
                isActive = true;
            }
            else
            {
                isActive = false;
            }

            if (isActive)
            {
                laser.enabled = true;
            }
            else
            {
                laser.enabled = false;
            }
        }
    }
}