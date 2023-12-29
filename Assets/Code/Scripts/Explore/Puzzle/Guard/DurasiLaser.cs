namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;

    /// <summary>
    /// Represents the duration of a laser interaction and controls laser visibility.
    /// </summary>
    public class DurasiLaser : MonoBehaviour
    {
        [SerializeField] Laser laser; // Reference to the associated laser object.
        private float Durasi; // The remaining duration of the laser interaction.
        private bool isActive = false; // Indicates if the laser interaction is active.

        /// <summary>
        /// Sets the duration of the laser interaction, adding the specified duration.
        /// </summary>
        /// <param name="tambahDurasi">The duration to add to the existing duration.</param>
        public void SetDurasi(float tambahDurasi)
        {
            // Add the specified duration while ensuring Durasi is not less than 0.
            Durasi += tambahDurasi;
            Durasi = Mathf.Max(0, Durasi);
        }

        void Update()
        {
            if (Durasi > 0)
            {
                // Decrease the remaining duration over time.
                Durasi -= Time.deltaTime;
                isActive = true;
            }
            else
            {
                isActive = false;
            }

            // Enable or disable the laser based on its activity status.
            laser.enabled = isActive;
        }
    }
}