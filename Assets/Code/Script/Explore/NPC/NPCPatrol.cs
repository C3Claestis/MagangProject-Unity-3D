namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NPCPatrol : MonoBehaviour
    {
        // Components        
        Animator anim;
        // Movement Parameters
        [SerializeField] private float maxRange; // Maximum range of movement
        [SerializeField] private float moveSpeedRandom; // Movement speed

        // Target Positions
        private Vector3 targetPosition;
        private Vector3 initialPosition;
        private Vector3 currentVelocity;

        // Movement Flags
        bool IsRandomly = true;
        private bool isMoving = false;
        private Coroutine idleCoroutine;
        private float rotasiRandom = 5;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            initialPosition = transform.position;
            SetRandomTargetPosition();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsRandomly)
            {
                MoveRandomly();
            }
            if (isMoving)
            {
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }
        }
        // Move randomly within a specified range
        private void MoveRandomly()
        {
            Vector3 direction = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotasiRandom * Time.deltaTime);

            // Check if the NPC is close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
            {
                Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, moveSpeedRandom);
                transform.position = newPosition;
                isMoving = true;
            }
            else
            {
                isMoving = false;

                if (idleCoroutine == null)
                {
                    idleCoroutine = StartCoroutine(StartIdleAfterDelay(2f));
                }
            }
        }

        // Set a random target position within the maximum range
        private void SetRandomTargetPosition()
        {
            float randomX = Random.Range(initialPosition.x - maxRange * 2, initialPosition.x + maxRange * 2);
            float randomZ = Random.Range(initialPosition.z - maxRange * 2, initialPosition.z + maxRange * 2);
            targetPosition = new Vector3(randomX, initialPosition.y, randomZ);
        }
        // Start idle animation after a delay
        private IEnumerator StartIdleAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isMoving = true;
            SetRandomTargetPosition();
            idleCoroutine = null;
        }
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, maxRange);
            Gizmos.color = Color.red;
        }
    }
}