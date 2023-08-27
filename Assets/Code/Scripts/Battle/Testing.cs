using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            unit.GetMoveAction().GetValidActionGridPosition();
        }

    }

}