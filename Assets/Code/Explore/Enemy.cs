namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;
    public class Enemy : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;
        [SerializeField] Image barCountDown;
        [SerializeField] GameObject dangerIcon;
        [SerializeField] KoloniKroco koloni;                
        [SerializeField] private float maxRange; // Atur jangkauan maksimum     
        [SerializeField] private float moveSpeedRandom; // Kecepatan pergerakan
        private Vector3 targetPosition;
        private Vector3 initialPosition;
        private Vector3 currentVelocity; // Variabel untuk menghitung kecepatan pergerakan
        bool IsRandomly = true;
        private bool isMoving = false;
        private Coroutine idleCoroutine;
        private float rotasiRandom = 5;

        private float rotateSpeed = 180.0f; // Kecepatan rotasi (misalnya, 180 derajat/detik)
        private float rotationThreshold = 5.0f; // Ambang batas rotasi (misalnya, 5 derajat)
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
            Follow(koloni.GetFollow());
        }
        void BarCircle()
        {
            barCountDown.fillAmount = Mathf.InverseLerp(0, koloni.GetDurasi(), koloni.GetTimeWait());            
        }
        public void Follow(Transform follow)
        {
            if (koloni.GetIsFollow() == true)
            {
                IsRandomly = false;
                dangerIcon.SetActive(true);
                agent.enabled = true;
                koloni.SetJarak(GetJarakDuaKaliKoloni());

                // Menghitung arah menuju target
                Vector3 targetDirection = follow.position - transform.position;
                targetDirection.y = 0; // Mengabaikan perubahan tinggi

                // Menghitung rotasi untuk menghadap target
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // Rotasi secara perlahan untuk mencapai rotasi target
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                // Jika sudah menghadap target, aktifkan kembali NavMeshAgent dan mulai mengejar
                if (Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold)
                {
                    anim.SetBool("IsFollow", true);
                    agent.enabled = true;
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
                    agent.enabled = false;                                
                }                
                else
                {
                    koloni.SetJarak(GetJarakKoloni());
                    agent.SetDestination(koloni.transform.position);
                }
            }

            if (IsRandomly)
            {
                MoveRandomly();
            }
        }
        private void MoveRandomly()
        {            
            // Menghitung perbedaan antara posisi sekarang dan target position
            Vector3 direction = targetPosition - transform.position;

            // Menghitung rotasi yang sesuai
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Menerapkan rotasi secara perlahan menggunakan Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotasiRandom * Time.deltaTime);

            // Menggunakan Vector3.SmoothDamp untuk pergerakan yang lebih halus
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, moveSpeedRandom);
            transform.position = newPosition;

            // Jika sudah mendekati target, atur target baru
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {                
                isMoving = false;
                
                // Mulai jeda waktu selama dua detik
                if (idleCoroutine == null)
                {
                    idleCoroutine = StartCoroutine(StartIdleAfterDelay(2f)); // Jeda 2 detik sebelum animasi idle dimulai
                }
            }         
        }

        private void SetRandomTargetPosition()
        {
            // Pilih posisi acak dalam area jangkauan
            float randomX = Random.Range(initialPosition.x - maxRange *2, initialPosition.x + maxRange * 2);
            float randomZ = Random.Range(initialPosition.z - maxRange *2, initialPosition.z + maxRange * 2);
            targetPosition = new Vector3(randomX, initialPosition.y, randomZ);
        }
        // Metode untuk memulai animasi idle setelah jeda waktu
        private IEnumerator StartIdleAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isMoving = true;
            SetRandomTargetPosition();
            idleCoroutine = null; // Atur ulang coroutine setelah selesai
        }
    }    
}