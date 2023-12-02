namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;

    /// <summary>
    /// Controls the behavior of an enemy character in the game.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        // Components
        NavMeshAgent agent;
        Animator anim;

        // UI Elements
        [SerializeField] Image barCountDown;
        [SerializeField] GameObject dangerIcon;

        // Reference to KoloniKroco script
        [SerializeField] KoloniKroco koloni;

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

        // Rotation Parameters
        private float rotateSpeed = 180.0f; // Rotation speed (e.g., 180 degrees per second)
        private float rotationThreshold = 5.0f; // Rotation threshold (e.g., 5 degrees)

        // Public methods for getting distance from KoloniKroco
        private float GetJarakKoloni() => koloni.GetJarak();
        private float GetJarakDuaKaliKoloni() => koloni.GetJarakKaliDua();

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            initialPosition = transform.position;
            SetRandomTargetPosition();
        }

        // Update is called once per frame
        void Update()
        {
            barCountDown.gameObject.SetActive(koloni.GetIsDeteksi());

            if (koloni.GetIsDeteksi() == true)
            {
                BarCircle();
            }

            if (isMoving)
            {
                anim.SetBool("IsWalk", true);
            }
            else
            {
                anim.SetBool("IsWalk", false);
            }

            if (IsRandomly)
            {
                MoveRandomly();
            }
            Follow(koloni.GetFollow());
        }

        // Update the countdown bar's fill amount
        void BarCircle()
        {
            barCountDown.fillAmount = Mathf.InverseLerp(0, koloni.GetDurasi(), koloni.GetTimeWait());
        }

        // Follow the target transform
        public void Follow(Transform follow)
        {
            if (koloni.GetIsFollow() == true)
            {
                IsRandomly = false;
                dangerIcon.SetActive(true);
                agent.speed = 3;
                koloni.SetJarak(GetJarakDuaKaliKoloni());

                Vector3 targetDirection = follow.position - transform.position;
                targetDirection.y = 1;

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold)
                {
                    anim.SetBool("IsFollow", true);
                    agent.speed = 3;
                    koloni.SetJarak(GetJarakDuaKaliKoloni());
                    agent.SetDestination(follow.position);
                }
            }
            else
            {
                dangerIcon.SetActive(false);
                if (agent.remainingDistance <= 1f)
                {
                    anim.SetBool("IsFollow", false);
                    IsRandomly = true;
                    agent.speed = 0;
                }
                else
                {
                    koloni.SetJarak(GetJarakKoloni());
                    agent.SetDestination(koloni.transform.position);
                }
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
    }
}