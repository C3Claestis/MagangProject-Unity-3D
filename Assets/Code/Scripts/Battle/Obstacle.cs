namespace Nivandria.Battle
{
    using System;
    using UnityEngine;

    public class Obstacle : MonoBehaviour, IDamageable
    {
        public event EventHandler OnHitted;
        public event EventHandler OnDead;


        [SerializeField] private string obstacleName = "Obstacle";
        [SerializeField] private int health = 10;
        [SerializeField] private bool isBroken = false;

        void Start()
        {
            gameObject.name = obstacleName;
        }

        public void Damage(int damage)
        {
            health -= damage;

            if(health > 0) Debug.Log("Hitted");
            else Broke();
        }

        private void Broke()
        {
            health = 0;
            isBroken = true;
            // ! Add Animation
            Debug.Log("Broken");
        }

        public bool IsBroken() => isBroken;

    }

}