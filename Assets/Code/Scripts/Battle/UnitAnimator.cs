namespace Nivandria.Battle.Action
{
    using System;
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