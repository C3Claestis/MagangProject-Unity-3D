namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RotateGaard : MonoBehaviour
    {        
        [SerializeField] float rotationAngle;
        int count;
        private void Start()
        {
            count = 0;
        }
        // Update is called once per frame
        void Update()
        {          
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(count < 2)
                {
                    count++;
                    RotateObject(+rotationAngle);
                }                    
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(count > -2)
                {
                    count--;
                    RotateObject(-rotationAngle);
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