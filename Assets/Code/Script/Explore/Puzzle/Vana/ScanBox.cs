namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ScanBox : MonoBehaviour
    {
        [SerializeField] GameObject benar, salah;
        // Update is called once per frame
        void Update()
        {
            if (transform.position.x % 1 == 0)
            {
                benar.SetActive(true);
                salah.SetActive(false);
            }
            else
            {
                benar.SetActive(false);
                salah.SetActive(true);
            }
        }
    }

}