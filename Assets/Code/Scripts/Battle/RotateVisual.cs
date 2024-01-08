namespace Nivandria.Battle
{
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using UnityEngine;

    public class RotateVisual : MonoBehaviour
    {
        public void ResetPosition()
        {
            GridPosition pointerGrid = Pointer.Instance.GetCurrentGrid();
            Vector3 unitPosition = LevelGrid.Instance.GetWorldPosition(pointerGrid);
            transform.position = new Vector3(unitPosition.x, 0.04f, unitPosition.z);
        }

        public void SetActive(bool status) => transform.gameObject.SetActive(status);

        public Transform GetTransform() => transform;
    }

}