namespace Nivandria.Explore.Puzzle
{
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;
    public class RotateGlass : MonoBehaviour
    {
        int count = 0;
        [SerializeField] private bool glass1 = false;
        [SerializeField] private bool glass2 = false;
        [SerializeField] private bool glass3 = false;
        [SerializeField] float rotationSpeed = 30.0f; // Kecepatan rotasi
        [SerializeField] Transform[] glass = new Transform[3];
        [SerializeField] float[] value_true = new float[3];

        [SerializeField] Text text;

        // Update is called once per frame
        void Update()
        {
            if (glass[0].rotation.eulerAngles.z < value_true[0] + 1 && glass[0].rotation.eulerAngles.z > value_true[0] - 1)
            {
                glass1 = true;
            }
            else
            {
                glass1 = false;
            }

            if (glass[1].rotation.eulerAngles.z < value_true[1] + 1 && glass[1].rotation.eulerAngles.z > value_true[1] - 1)
            {
                glass2 = true;
            }
            else
            {
                glass2 = false;
            }
            
            if (glass[2].rotation.eulerAngles.z < value_true[2] + 1 && glass[2].rotation.eulerAngles.z > value_true[2] - 1)
            {
                glass3 = true;
            }
            else
            {
                glass3 = false;
            }

            //Untuk memutar glass
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotasiPutarKurang(count);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotasiPutarTambah(count);
            }

            //Untuk berpindah glass yang di putar
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                count++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                count--;
            }

            //Untuk Mengatur agar ketika lebih dari jangkauan maka kembali ke awal
            if (count > 2)
            {
                count = 0;
            }
            if (count < 0)
            {
                count = 2;
            }

            if (glass1 && glass2 && glass3)
            {
                Debug.Log("BENAR");
            }
            Test();

            Debug.Log("Rotation 0: " + glass[0].rotation.eulerAngles.z);
            Debug.Log("Rotation 1: " + glass[1].rotation.eulerAngles.z);
            Debug.Log("Rotation 2: " + glass[2].rotation.eulerAngles.z);
        }
        void RotasiPutarKurang(int nilai)
        {
            // Menghitung rotasi baru
            float newRotation = glass[nilai].rotation.eulerAngles.z - rotationSpeed * Time.deltaTime;
            // Mengubah rotasi GameObject pada sumbu Z
            glass[nilai].transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
        void RotasiPutarTambah(int nilai)
        {
            // Menghitung rotasi baru
            float newRotation = glass[nilai].rotation.eulerAngles.z + rotationSpeed * Time.deltaTime;
            // Mengubah rotasi GameObject pada sumbu Z
            glass[nilai].transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }

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