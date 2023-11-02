namespace Nivandria.Battle.UnitSystem
{
    using System;
    using Nivandria.Battle.Action;
    using UnityEngine;

    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;

        private void Awake()
        {
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
            unitAnimator.SetBool("IsWalking", true);
        }

        private void MoveAction_OnStopMoving(object sender, EventArgs e)
        {
            unitAnimator.SetBool("IsWalking", false);
        }

        private void moveAction_OnJump(object sender, EventArgs e)
        {
            unitAnimator.SetTrigger("Jump");
        }
    }

}