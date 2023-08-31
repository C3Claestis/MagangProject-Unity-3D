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
    }
    
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SelectFastestUnit();
        }

        if (Input.GetMouseButtonDown(0)){
            TryMoveSelectedUnitToGridPosition();
        }

        if(Input.GetMouseButtonUp(1)){
            selectedUnit.GetSpinAction().Spin();
        }
    }

    /// <summary> Handles the selection of the fastest unit that hasn't moved yet.
    /// </summary>
    private void SelectFastestUnit()
    {
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);

        Unit fastestUnit = null;
        int fastestSpeed = int.MinValue;

        foreach (GameObject unitObject in unitObjects)
        {
            Unit unitComponent = unitObject.GetComponent<Unit>();

            if (unitComponent == null) continue;
            
            int unitSpeed = unitComponent.GetAgility();
            bool hasMoved = unitComponent.GetMoveStatus();

            if (!hasMoved && unitSpeed > fastestSpeed)
            {
                fastestUnit = unitComponent;
                fastestSpeed = unitSpeed;
            }
        }

        if (selectedUnit != null)
        {
            ResetSelectedUnit();
        }

        if (fastestUnit != null)
        {
            CameraController.Instance.SetCameraFocusToPosition(fastestUnit.transform.position);
            SelectUnit(fastestUnit);
        }
        else
        {
            selectedUnit = null;
            Debug.Log("All units have already moved");
        }
    }

    /// <summary>Resets the selected unit's status and shading after it has been moved.
    /// </summary>
    private void ResetSelectedUnit()
    {
        selectedUnit.SetSelectedStatus(false);
        selectedUnit.ChangeUnitShade();
        selectedUnit.SetMoveStatus(true);
    }

    /// <summary>Tries to move the selected unit to the mouse cursor's grid position.
    /// </summary>
    private void TryMoveSelectedUnitToGridPosition(){
        if (selectedUnit == null) return;

        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        
        if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)){
            selectedUnit.GetMoveAction().MoveTo(mouseGridPosition);
        }
    }

    /// <summary>Selects a unit and updates its status and shading.
    /// </summary>
    /// <param name="unit">The unit to be selected.</param>
    private void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        selectedUnit.SetMoveStatus(true);
        selectedUnit.SetSelectedStatus(true);
        selectedUnit.ChangeUnitShade();
    }

    public Unit GetSelectedUnit() => selectedUnit;
}
