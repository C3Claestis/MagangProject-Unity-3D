using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    private string unitTag = "Units";
    
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            HandleUnitSelection();
        }

        if (Input.GetMouseButtonDown(0)){
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private void HandleUnitSelection(){
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);
        
        Unit fastestUnit = null;
        int fastestSpeed = int.MinValue;

        foreach (GameObject unitObject in unitObjects)
        {
            Unit unitComponent = unitObject.GetComponent<Unit>();

            if (unitComponent != null)
            {
                int unitSpeed = unitComponent.GetCT();

                if (unitSpeed >= 100 && unitSpeed > fastestSpeed)
                {
                    fastestUnit = unitComponent;
                    fastestSpeed = unitSpeed;
                }
            }
        }

        if (fastestUnit != null)
        {
            selectedUnit = fastestUnit;
            Debug.Log("Fastest unit with speed over 100: " + fastestUnit.gameObject.name + ", Speed: " + fastestSpeed);
        }
        else
        {
            Debug.Log("No units with speed over 100 and the 'Unit' component found.");
        }
    }
}
