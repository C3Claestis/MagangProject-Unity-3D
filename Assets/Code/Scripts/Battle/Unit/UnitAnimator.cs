namespace Nivandria.Battle.UnitSystem
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using UnityEngine;

    public class UnitAnimator : MonoBehaviour
    {
        private Animator unitAnimator;

        private List<IDamageable> targetList;

        private void Awake()
        {
            unitAnimator = GetComponent<Animator>();

            if (TryGetComponent<MoveAction>(out MoveAction moveAction))
            {
                moveAction.OnStartMoving += MoveAction_OnStartMoving;
                moveAction.OnStopMoving += MoveAction_OnStopMoving;
                moveAction.OnJump += moveAction_OnJump;
            }

            if (TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                iDamageable.OnHitted += IDamageable_OnHitted;
                iDamageable.OnDead += IDamageable_OnDead;
            }
        }

        private void IDamageable_OnDead(object sender, EventArgs e)
        {
            unitAnimator.SetBool("Dead", true);
        }

        private void IDamageable_OnHitted(object sender, EventArgs e)
        {
            unitAnimator.SetTrigger("Hit");
        }

        private void MoveAction_OnStartMoving(object sender, EventArgs e)
        {
            switch (gameObject.name)
            {
                case "Sacra":
                    unitAnimator.SetBool("isRun", true);
                    return;

                case "Boar":
                    unitAnimator.SetBool("IsFollow", true);
                    return;
            }

            unitAnimator.SetBool("IsWalking", true);
        }

        private void MoveAction_OnStopMoving(object sender, EventArgs e)
        {
            switch (gameObject.name)
            {
                case "Sacra":
                    unitAnimator.SetBool("isRun", false);
                    return;

                case "Boar":
                    unitAnimator.SetBool("IsFollow", false);
                    return;
            }

            unitAnimator.SetBool("IsWalking", false);
        }

        private void moveAction_OnJump(object sender, EventArgs e)
        {
            unitAnimator.SetTrigger("Jump");
        }
    }

}