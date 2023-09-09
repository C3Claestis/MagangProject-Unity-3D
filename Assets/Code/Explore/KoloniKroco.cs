namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class KoloniKroco : MonoBehaviour
    {
        [SerializeField] float _jarak, _jarak_kali_dua, durasi;
        Transform follow;        
        private bool IsFollow = false;
        private bool IsDeteksi = false;
        private float range, wait;
        // Start is called before the first frame update
        void Start()
        {            
            range = _jarak;
            SetFollow(FindAnyObjectByType<PlayerMov>().GetComponent<Transform>()); 
        }

        // Update is called once per frame
        void Update()
        {
            JarakScene();
        }
        void JarakScene()
        {
            //Deteksi jarak player
            float dis = Vector3.Distance(transform.position, follow.position);
            
            if (dis <= range)
            {
                SetIsDeteksi(true);
                wait += Time.deltaTime;
                if (wait > durasi)
                {
                    SetIsFollow(true);
                    SetIsDeteksi(false);
                }
            }
            else
            {
                wait = 0;
                SetIsFollow(false);
                SetIsDeteksi(false);
            }            
        }     
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
            Gizmos.color = Color.red;
        }
        public void SetJarak(float jarak) => this.range = jarak;
        public float GetJarak() => _jarak;
        public void SetJarakKaliDua(float duakali) => this._jarak_kali_dua = duakali;
        public float GetJarakKaliDua() => _jarak_kali_dua;
        public void SetIsFollow(bool isfollow) => this.IsFollow = isfollow;
        public bool GetIsFollow() => IsFollow;
        public void SetFollow(Transform follow) => this.follow = follow;
        public Transform GetFollow() => follow;
        public void SetIsDeteksi(bool isdetek) => this.IsDeteksi = isdetek;
        public bool GetIsDeteksi() => IsDeteksi;
        public float GetTimeWait() => wait;
        public float GetDurasi() => durasi;
    }

}