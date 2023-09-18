namespace Nivandria.Explore.Puzzle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LazerBeam : MonoBehaviour
    {
        [SerializeField] Material material;
        Lazer lazerBeam;

        // Update is called once per frame
        void Update()
        {
            Destroy(GameObject.Find("LaserBeam"));
            lazerBeam = new Lazer(gameObject.transform.position, gameObject.transform.forward, material);
        }
    }
}