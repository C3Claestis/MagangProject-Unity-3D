namespace Nivandria.Battle
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine.InputSystem;
    using UnityEngine;
    using System.Linq;
    using Unit = UnitSystem.Unit;
    using UnityEngine.UI;
    using TMPro;

    public class PlacingUnitSystem : MonoBehaviour
    {
        public static PlacingUnitSystem Instance { get; private set; }

        [SerializeField] private InitializeBattlefield initBattle;
        [SerializeField] private Transform placingUnitPrefab;
        [SerializeField] private Transform unitContainer;

        [SerializeField] private List<Transform> unitList;
        [SerializeField] private List<UnitSetup> enemyUnitList;

        private List<GridPosition> validPlacingGrid = new List<GridPosition>();
        private List<UnitSetup> unitPlaced = new List<UnitSetup>();

        private List<Transform> unitToPlaceList;
        [SerializeField] private Transform unitToPlace;
        bool startPlacing = false;
        bool finish = false;

        [Header("UI")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image charaImage;
        [SerializeField] private TextMeshProUGUI nameText;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            Pointer.Instance.HidePointerHand(true);
            Pointer.Instance.HideCircle(true);
            ShowCanvas(false);
            UnitTurnSystemUI.Instance.ShowTurnSystemUI(false);
            unitToPlaceList = new List<Transform>(unitList);
            initBattle.SetEnemyUnitList(enemyUnitList);
            initBattle.SetupUnit();
            UpdateSelectedUnit();

            int height = LevelGrid.Instance.GetGridHeight();
            for (int x = 0; x < 2; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    validPlacingGrid.Add(new GridPosition(x, z));
                }
            }

            StartCoroutine(OpeningScreen());
        }

        public void PlacingUnit()
        {
            if (!startPlacing) return;
            if (unitToPlace == null) return;
            GridPosition gridPosition = Pointer.Instance.GetCurrentGrid();

            if (!validPlacingGrid.Contains(gridPosition)) return;

            SpawnUnit(unitToPlace, gridPosition);

            GridSystemVisual.Instance.HideGrid(gridPosition);
        }

        public void ConfirmPlace(GridPosition gridPosition)
        {
            validPlacingGrid.Remove(gridPosition);
            unitToPlaceList.Remove(unitToPlace);
            UpdateSelectedUnit();
        }

        private void DeleteUnit(GridPosition gridPosition)
        {
            Unit unit = LevelGrid.Instance.GetUnitListAtGridPosition(gridPosition)?.FirstOrDefault();
            if (unit == null) return;

            LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, unit);
            UnitSetup unitSetup = GetUnitPlaced(gridPosition);
            unitPlaced.Remove(unitSetup);
            Destroy(unit.gameObject);

            unitToPlaceList.Add(unitSetup.GetPrefab());
            validPlacingGrid.Add(new GridPosition(gridPosition.x, gridPosition.z));
            UpdateGrids();

            if (unitToPlaceList.Count == 1) UpdateSelectedUnit();
        }

        public void DeleteUnit()
        {
            GridPosition gridPosition = Pointer.Instance.GetCurrentGrid();
            DeleteUnit(gridPosition);
        }

        public void ChangeSelectedUnit(float changeValue)
        {
            int maxList = unitToPlaceList.Count;

            if (unitToPlace == null || maxList <= 1) return;

            int indexOfSelectedUnit = unitToPlaceList.IndexOf(unitToPlace);

            if (indexOfSelectedUnit == 0 && changeValue < 0) unitToPlace = unitToPlaceList[unitToPlaceList.Count - 1];
            else if (indexOfSelectedUnit == unitToPlaceList.Count - 1 && changeValue > 0) unitToPlace = unitToPlaceList[0];
            else if (changeValue < 0) unitToPlace = unitToPlaceList[indexOfSelectedUnit - 1];
            else if (changeValue > 0) unitToPlace = unitToPlaceList[indexOfSelectedUnit + 1];

            UpdateUI();
        }

        public void CancelRotate()
        {
            validPlacingGrid.RemoveAt(validPlacingGrid.Count - 1);
            unitToPlaceList.RemoveAt(unitToPlaceList.Count - 1);
        }

        public void Cancel_Action()
        {
            if (unitPlaced.Count == 0) return;

            GridPosition lastUnitPosition = unitPlaced[unitPlaced.Count - 1].GetPosition();
            DeleteUnit(lastUnitPosition);
            Pointer.Instance.SetPointerOnGrid(lastUnitPosition);
        }

        private UnitSetup GetUnitPlaced(GridPosition gridPosition)
        {
            foreach (var unitSetup in unitPlaced)
            {
                if (unitSetup.GetPosition() == gridPosition) return unitSetup;
            }
            return null;
        }

        private void SpawnUnit(Transform unitPrefab, GridPosition gridPosition)
        {
            Vector3 position = LevelGrid.Instance.GetWorldPosition(gridPosition);
            Transform newUnitTransform = Instantiate(unitPrefab, position, Quaternion.identity, unitContainer);
            Unit newUnit = newUnitTransform.GetComponent<Unit>();
            newUnitTransform.rotation = Quaternion.Euler(0, 90, 0);
            unitPlaced.Add(new UnitSetup(unitPrefab, gridPosition));
            GetComponent<RotatePlaceUnit>().StartRotating(newUnit);
        }

        public void UpdateSelectedUnit()
        {
            if (unitToPlaceList.Count == 0)
            {
                unitToPlace = null;
                charaImage.color = new Color(1, 1, 1, 0);
                nameText.text = "";
                return;
            }

            unitToPlace = unitToPlaceList[0];
            UpdateUI();
        }

        public void UpdateGrids()
        {
            GridSystemVisual.Instance.ShowGridPositionList(validPlacingGrid, GridVisualType.White);
        }

        void UpdateUI()
        {
            Unit unit = unitToPlace.GetComponent<Unit>();
            charaImage.sprite = unit.GetUnitIcon();
            charaImage.color = new Color(1, 1, 1, 1);
            nameText.text = unit.GetCharacterName();
        }

        private IEnumerator OpeningScreen()
        {
            UnitTurnSystemUI.Instance.ShowOpeningCard(true);
            Vector3 position = new Vector3(0, 0, LevelGrid.Instance.GetGridHeight() / 2);

            yield return new WaitForSeconds(2);

            UpdateGrids();
            Pointer.Instance.HideCircle(false);
            PlayerInputController.Instance.SetActionMap("PlacingUnits");
            UnitTurnSystemUI.Instance.ShowOpeningCard(false);
            CameraController.Instance.SetCameraFocusToPosition(position);
            Pointer.Instance.SetPointerOnGrid(LevelGrid.Instance.GetGridPosition(position));
            startPlacing = true;
            ShowCanvas(true);
        }

        public void PlacingUnits_Finish(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (unitPlaced.Count == 0)
            {
                Debug.LogError("Unit need to be placed at least one!");
                return;
            }
            UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction, "Finish Setup Units ?");
        }

        private void NoButtonAction()
        {
            PlayerInputController.Instance.SetActionMap("PlacingUnits");
        }

        private void YesButtonAction()
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            UnitTurnSystemUI.Instance.ShowTurnSystemUI(true);
            UnitTurnSystem.Instance.StartBattle();
            finish = true;

            for (int i = 0; i < unitContainer.childCount; i++)
            {
                unitContainer.GetChild(i).GetComponent<Unit>().UpdateUnitShade();
            }

            ShowCanvas(false);
        }


        private void ShowCanvas(bool show)
        {
            canvasGroup.alpha = show ? 1 : 0;
            canvasGroup.blocksRaycasts = show;
            canvasGroup.interactable = show;
        }


        public bool GetStatus() => finish;
    }

}