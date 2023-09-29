namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controls the rotation of multiple glass objects and checks if they are aligned correctly.
    /// </summary>
    public class RotateGlass : MonoBehaviour
    {
        int count = 0; // Current selected glass
        [SerializeField] private bool glass1 = false; // Indicates if glass 1 is aligned correctly
        [SerializeField] private bool glass2 = false; // Indicates if glass 2 is aligned correctly
        [SerializeField] private bool glass3 = false; // Indicates if glass 3 is aligned correctly
        [SerializeField] float rotationSpeed = 30.0f; // Rotation speed
        [SerializeField] Transform[] glass = new Transform[3]; // References to glass objects
        [SerializeField] float[] value_true = new float[3]; // Target rotation values for each glass
        [SerializeField] Text text; // UI text to display information

        // Update is called once per frame
        void Update()
        {
            KondisiMenang();

            // Rotate the currently selected glass
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotasiPutarKurang(count);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotasiPutarTambah(count);
            }

            // Switch to the next/previous glass
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                count++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                count--;
            }

            // Ensure the count stays within valid bounds
            if (count > 2)
            {
                count = 0;
            }
            if (count < 0)
            {
                count = 2;
            }

            Test();

            Debug.Log("Rotation 0: " + glass[0].rotation.eulerAngles.z);
            Debug.Log("Rotation 1: " + glass[1].rotation.eulerAngles.z);
            Debug.Log("Rotation 2: " + glass[2].rotation.eulerAngles.z);
        }

        // Methods for rotating the currently selected glass
        void RotasiPutarKurang(int nilai)
        {
            // Calculate the new rotation
            float newRotation = glass[nilai].rotation.eulerAngles.z - rotationSpeed * Time.deltaTime;
            // Update the rotation of the GameObject along the Z axis
            glass[nilai].transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
        void RotasiPutarTambah(int nilai)
        {
            // Calculate the new rotation
            float newRotation = glass[nilai].rotation.eulerAngles.z + rotationSpeed * Time.deltaTime;
            // Update the rotation of the GameObject along the Z axis
            glass[nilai].transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }

        // Check if all glasses are aligned correctly
        void KondisiMenang()
        {
            // Check glass 0 alignment
            if (glass[0].rotation.eulerAngles.z < value_true[0] + 1 && glass[0].rotation.eulerAngles.z > value_true[0] - 1)
            {
                glass1 = true;
            }
            else
            {
                glass1 = false;
            }

            // Check glass 1 alignment
            if (glass[1].rotation.eulerAngles.z < value_true[1] + 1 && glass[1].rotation.eulerAngles.z > value_true[1] - 1)
            {
                glass2 = true;
            }
            else
            {
                glass2 = false;
            }

            // Check glass 2 alignment
            if (glass[2].rotation.eulerAngles.z < value_true[2] + 1 && glass[2].rotation.eulerAngles.z > value_true[2] - 1)
            {
                glass3 = true;
            }
            else
            {
                glass3 = false;
            }

            // Trigger win condition if all glasses are aligned correctly
            if (glass1 && glass2 && glass3)
            {
                Debug.Log("BENAR");
            }
        }

        // Display information about the currently selected glass
        void Test()
        {
            switch (count)
            {
                case 0:
                    text.text = "Besar sedang diputar";
                    break;
                case 1:
                    text.text = "Sedang sedang diputar";
                    break;
                case 2:
                    text.text = "Kecil sedang diputar";
                    break;
            }
        }
    }
}