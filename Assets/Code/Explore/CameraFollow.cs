namespace Nivandria.Explore
{
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        private Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindAnyObjectByType<PlayerMov>().GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }
    }

}