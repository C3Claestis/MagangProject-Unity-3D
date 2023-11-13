namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents a creature that follows the player within a specified range and duration.
    /// </summary>
    public class KoloniKroco : MonoBehaviour
    {
        [SerializeField] float _jarak, _jarak_kali_dua, durasi;
        Transform follow;
        private bool IsFollow = false;
        private bool IsDeteksi = false;
        private float range, wait;        

        #region Getter Setter
        /// <summary>
        /// Sets the detection range for following the player.
        /// </summary>
        /// <param name="jarak">The detection range.</param>
        public void SetJarak(float jarak) => this.range = jarak;

        /// <summary>
        /// Gets the detection range for following the player.
        /// </summary>
        /// <returns>The detection range.</returns>
        public float GetJarak() => _jarak;

        /// <summary>
        /// Sets a value that is twice the detection range.
        /// </summary>
        /// <param name="duakali">Twice the detection range.</param>
        public void SetJarakKaliDua(float duakali) => this._jarak_kali_dua = duakali;

        /// <summary>
        /// Gets a value that is twice the detection range.
        /// </summary>
        /// <returns>Twice the detection range.</returns>
        public float GetJarakKaliDua() => _jarak_kali_dua;

        /// <summary>
        /// Sets whether the creature is following the player.
        /// </summary>
        /// <param name="isfollow">True if following; false otherwise.</param>
        public void SetIsFollow(bool isfollow) => this.IsFollow = isfollow;

        /// <summary>
        /// Gets whether the creature is following the player.
        /// </summary>
        /// <returns>True if following; false otherwise.</returns>
        public bool GetIsFollow() => IsFollow;

        /// <summary>
        /// Sets the transform to follow, typically the player's transform.
        /// </summary>
        /// <param name="follow">The transform to follow.</param>
        public void SetFollow(Transform follow) => this.follow = follow;

        /// <summary>
        /// Gets the transform being followed.
        /// </summary>
        /// <returns>The transform being followed.</returns>
        public Transform GetFollow() => follow;

        /// <summary>
        /// Sets whether the creature has detected the player.
        /// </summary>
        /// <param name="isdetek">True if detected; false otherwise.</param>
        public void SetIsDeteksi(bool isdetek) => this.IsDeteksi = isdetek;

        /// <summary>
        /// Gets whether the creature has detected the player.
        /// </summary>
        /// <returns>True if detected; false otherwise.</returns>
        public bool GetIsDeteksi() => IsDeteksi;

        /// <summary>
        /// Gets the time waited since detecting the player.
        /// </summary>
        /// <returns>The time waited since detecting the player.</returns>
        public float GetTimeWait() => wait;

        /// <summary>
        /// Gets the duration for which the creature waits after detecting the player.
        /// </summary>
        /// <returns>The duration for waiting.</returns>
        public float GetDurasi() => durasi;
        #endregion

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
            // Detect the distance to the player
            float dis = Vector3.Distance(transform.position, follow.position);
            
            if (dis <= range)
            {
                SetIsDeteksi(true);
                wait += Time.deltaTime;
                if (wait > durasi)
                {
                    SetIsFollow(true);
                    SetIsDeteksi(false);
                    InputSystem.GetInstance().SetIsTarget(true);
                }
            }
            else
            {
                wait = 0;
                SetIsFollow(false);
                SetIsDeteksi(false);
                InputSystem.GetInstance().SetIsTarget(false);
            }            
        }     

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
            Gizmos.color = Color.red;
        }       
    }
}