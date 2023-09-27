namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents a block with characters that can be rotated and scanned for interactions.
    /// </summary>
    public class Block : MonoBehaviour
    {
        [SerializeField] string char1, char2, char3, char4; // Characters assigned to the block.
        [SerializeField] TeksBlock teks; // Reference to a text display component.
        [SerializeField] LayerMask layerMask; // Layer mask for raycasting.
        [SerializeField] Transform blok; // Transform of the block.
        [SerializeField] GameObject material; // Material indicating interaction detection.

        private RaycastHit raycast; // Stores information about raycast hits.
        private int count = 0; // Current character count.
        private float range = 2; // Interaction range.
        private bool isRotating = false; // Indicates if the block is currently rotating.
        private bool isReset = false; // Indicates if the block should be reset.
        private bool isDeteksi = false; // Indicates if an interaction is detected.
        private float targetRotation = 0f; // Target rotation angle for the block.
        private float rotationSpeed = 90f; // Rotation speed in degrees per second.

        /// <summary>
        /// Sets the character count for the block.
        /// </summary>
        /// <param name="count">The character count to set.</param>
        public void SetCount(int count) => this.count = count;

        /// <summary>
        /// Sets whether the block should be reset.
        /// </summary>
        /// <param name="reset">True if the block should be reset; otherwise, false.</param>
        public void SetIsReset(bool reset) => this.isReset = reset;

        void Update()
        {
            Scan();

            if (isReset)
            {
                // Reset block's rotation, clear text, and reset count.
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

            if (targetRotation == 360)
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

        /// <summary>
        /// Handles the block's interaction when detected.
        /// </summary>
        public void InputScan()
        {
            if (isDeteksi)
            {
                if (!isRotating)
                {
                    // Set the target rotation 90 degrees higher than the current rotation.
                    targetRotation = blok.eulerAngles.y + 90f;
                    isRotating = true;
                }
            }
        }

        /// <summary>
        /// Scans for nearby interactions.
        /// </summary>
        void Scan()
        {
            // Raycast to scan for interactions in front of the block.
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * -1));

            if (Physics.Raycast(ray, out raycast, range, layerMask, QueryTriggerInteraction.Ignore))
            {
                // Draw a debug ray for visualization.
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
        /// Rotates the block.
        /// </summary>
        void Mutar()
        {
            if (isRotating)
            {
                // Perform a smooth rotation.
                float step = rotationSpeed * Time.deltaTime;
                blok.rotation = Quaternion.RotateTowards(blok.rotation, Quaternion.Euler(0f, targetRotation, 0f), step);

                // Check if the rotation is completed.
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