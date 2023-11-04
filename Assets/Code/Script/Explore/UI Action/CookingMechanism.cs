namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class CookingMechanism : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] float Max_value, Min_value;
        [SerializeField] Image timer;
        float speed = 0.1f;
        bool isKurang = true;
        // Update is called once per frame
        void Update()
        {
            if (isKurang)
            {
                timer.fillAmount -= speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (timer.fillAmount > Min_value && timer.fillAmount < Max_value)
                {
                    isKurang = false;
                    Debug.Log("GETQQ");
                }
            }
        }
    }
}