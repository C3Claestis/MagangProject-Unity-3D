using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    
    private Unit selectedUnit;
    private string unitTag = "Units";

    private void Awake(){
        if(Instance != null){
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start() {
        HandleUnitSelection();
    }
    
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            HandleUnitSelection();
        }

        if (Input.GetMouseButtonDown(0)){
            if (selectedUnit == null) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private void HandleUnitSelection()
    {
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);

        Unit fastestUnit = null;
        int fastestSpeed = int.MinValue;

        foreach (GameObject unitObject in unitObjects)
        {
            Unit unitComponent = unitObject.GetComponent<Unit>();

            if (unitComponent != null)
            {
                int unitSpeed = unitComponent.GetAgility();
                bool hasMoved = unitComponent.GetMoveStatus();

                if (!hasMoved && unitSpeed > fastestSpeed)
                {
                    fastestUnit = unitComponent;
                    fastestSpeed = unitSpeed;
                }
            }
        }

        if (selectedUnit != null)
        {
            ResetSelectedUnit();
        }

        if (fastestUnit != null)
        {
            SelectUnit(fastestUnit, fastestSpeed);
        }
        else
        {
            selectedUnit = null;
            Debug.Log("All units have already moved");
        }
    }

    private void ResetSelectedUnit()
    {
        selectedUnit.SetSelectedStatus(false);
        selectedUnit.ChangeMaterial();
        selectedUnit.SetMoveStatus(true);
    }

    private void SelectUnit(Unit unit, int speed)
    {
        selectedUnit = unit;
        selectedUnit.SetMoveStatus(true);
        selectedUnit.SetSelectedStatus(true);
        selectedUnit.ChangeMaterial();
        Debug.Log("Fastest Unit Selected! Name: " + unit.GetCharacterName() + ", Speed: " + speed);
    }

}
