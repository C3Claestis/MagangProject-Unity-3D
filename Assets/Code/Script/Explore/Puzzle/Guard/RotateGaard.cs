namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class RotateGaard : MonoBehaviour
    {        
        [SerializeField] float rotationAngle;
        int count;
        private void Start()
        {
            count = 0;
        }     
        public void RotasiKanan(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (count > -2)
                {
                    count--;
                    RotateObject(-rotationAngle);
                }
            }            
        }
        public void RotasiKiri(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (count < 2)
                {
                    count++;
                    RotateObject(+rotationAngle);
                }
            }            
        }
        void RotateObject(float angle)
        {
            // Ubah rotasi pada sumbu Y objek sesuai dengan sudut yang diberikan
            transform.Rotate(Vector3.up, angle);
        }
    }
}