namespace Nivandria.Battle.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GridSystemVisualSingle : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;


        //Getter Setter
        public void ShowGrid() => meshRenderer.enabled = true;
        public void HideGrid() => meshRenderer.enabled = false;
    }
}