namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Makes the object constantly face the camera while ignoring its own rotation.
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        private Transform cameraTransform; // Reference to the main camera's transform

        private void Awake()
        {
            cameraTransform = Camera.main.transform; // Get the main camera's transform
        }

        /// <summary>
        /// Makes the object look at a position while ignoring its own rotation.
        /// </summary>
        /// <param name="targetPos">The position to look at.</param>
        private void LookAtBackwards(Vector3 targetPos)
        {
            Vector3 offset = transform.position - targetPos; // Calculate the offset
            transform.LookAt(transform.position + offset); // Make the object look at the position
        }

        private void LateUpdate()
        {
            LookAtBackwards(cameraTransform.position); // Make the object constantly face the camera
        }
    }
}