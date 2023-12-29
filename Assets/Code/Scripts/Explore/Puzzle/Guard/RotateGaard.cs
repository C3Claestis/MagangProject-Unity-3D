namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Allows rotating an object in a puzzle environment using input actions.
    /// </summary>
    public class RotateGaard : MonoBehaviour
    {        
        [SerializeField] float rotationAngle;  // The angle by which the object should be rotated.
        int count;                             // Keeps track of the rotation count.

        private void Start()
        {
            count = 0;  // Initialize the rotation count.
        }     

        /// <summary>
        /// Handles the "RotasiKanan" action, which rotates the object to the right.
        /// </summary>
        /// <param name="context">The input action context.</param>
        public void RotasiKanan(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (count > -2)
                {
                    count--;
                    RotateObject(-rotationAngle);  // Rotate the object to the right.
                }
            }            
        }

        /// <summary>
        /// Handles the "RotasiKiri" action, which rotates the object to the left.
        /// </summary>
        /// <param name="context">The input action context.</param>
        public void RotasiKiri(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (count < 2)
                {
                    count++;
                    RotateObject(rotationAngle);  // Rotate the object to the left.
                }
            }            
        }

        /// <summary>
        /// Rotates the object by the specified angle around the Y-axis.
        /// </summary>
        /// <param name="angle">The angle by which to rotate the object.</param>
        void RotateObject(float angle)
        {
            // Rotate the object around the Y-axis by the given angle.
            transform.Rotate(Vector3.up, angle);
        }
    }
}