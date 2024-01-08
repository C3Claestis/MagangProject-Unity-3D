namespace Nivandria.Battle.UnitSystem
{
    using System;
    using UnityEngine;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.Action;
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using System.Collections;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.AI;
    using Nivandria.Battle.Grid;
    using Cinemachine;
    using UnityEngine.SceneManagement;

    public class UnitTurnSystem : MonoBehaviour
    {
        enum GameOver
        {
            WIN,
            LOSE,
            DRAW
        }

        public static UnitTurnSystem Instance { get; private set; }

        public event EventHandler OnUnitListChanged;

        [SerializeField] TextMeshProUGUI turnCountUI;
        private List<Unit> waitingUnitList;
        private Unit selectedUnit;
        private string unitTag = "Units";
        private int turnRounds = 1;

        private GameOver gameoverState;
        private bool gameover;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitTurnSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void StartBattle()
        {
            //Init Turn Visual
            waitingUnitList = new List<Unit>();
            waitingUnitList = SortFromFastestUnit();
            SetTurnCount(turnRounds);

            //Update Turn Visual
            UnitTurnSystemUI.Instance.UpdateTurnSystemVisual();

            //SettingUp Camera
            var cameraController = CameraController.Instance;
            CinemachineVirtualCamera verticalCamera = cameraController.GetVerticalCamera();
            cameraController.InitializedVirtualCamera(verticalCamera);

            UnitTurnSystemUI.Instance.ShowBattleStartCard(true);
            PlayerInputController.Instance.SetActionMap("BattleUI");
            StartCoroutine(ScreenWait(UnitTurnSystemUI.Instance.ShowBattleStartCard));
        }


        private IEnumerator ScreenWait(Action<bool> screen)
        {
            Pointer.Instance.HidePointerHand(true);
            Pointer.Instance.HideCircle(true);

            yield return new WaitForSeconds(2);

            Pointer.Instance.HidePointerHand(false);
            Pointer.Instance.HideCircle(false);

            screen(false);
            HandleUnitSelection();
            UnitActionSystem.Instance.ShowActionUI();

        }

        /// <summary> Handles the selection of the fastest unit that hasn't moved yet.</summary>
		public void HandleUnitSelection()
        {
            if (UnitActionSystem.Instance.GetBusyStatus()) return;

            Unit nextUnit = GetAndRemoveFirstUnit();

            if (selectedUnit != null)
            {
                ResetSelectedUnit();
            }

            if (nextUnit != null)
            {
                CameraController.Instance.SetCameraFocusToPosition(nextUnit.transform.position);
                SelectUnit(nextUnit);
                if (nextUnit.IsEnemy())
                {
                    nextUnit.GetComponent<UnitAI>().HandleEnemyTurn();
                }
                return;
            }

            NextTurn();
        }

        private List<Unit> SortFromFastestUnit()
        {
            GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);
            List<Unit> unitList = new List<Unit>();

            foreach (GameObject unitObject in unitObjects)
            {
                Unit unitComponent = unitObject.GetComponent<Unit>();

                if (unitComponent == null) continue;
                if (!unitComponent.IsAlive()) continue;

                unitList.Add(unitComponent);
            }

            unitList = unitList.OrderByDescending(unit => unit.GetCurrentAgility()).ToList();

            return unitList;
        }

        public bool CheckGameOverCondition()
        {
            if (gameover) return true;

            List<Unit> unitList = SortFromFastestUnit();

            if (unitList.Count == 0)
            {
                gameoverState = GameOver.DRAW;
                WaitAndAction(2);
                gameover = true;
                return true;
            }

            bool enemyIsAlive = false;
            bool SacraIsAlive = false;

            foreach (var unit in unitList)
            {
                if (unit.GetCharacterName() == "Sacra") SacraIsAlive = true;
                if (unit.IsEnemy()) enemyIsAlive = true;

                if (enemyIsAlive && SacraIsAlive) return false;
            }

            if (enemyIsAlive && !SacraIsAlive) gameoverState = GameOver.LOSE;
            else if (!enemyIsAlive && SacraIsAlive) gameoverState = GameOver.WIN;

            gameover = true;

            StartCoroutine(WaitAndAction(3));
            return true;
        }

        IEnumerator WaitAndAction(int waitForSeconds)
        {
            UnitActionSystemUI.Instance.ShowActionButtonBlocker(false);
            UnitActionSystem.Instance.HideActionUI();

            yield return new WaitForSeconds(waitForSeconds);

            StartGameOver(gameoverState);
        }

        private void StartGameOver(GameOver state)
        {
            switch (state)
            {
                case GameOver.WIN:
                    UnitTurnSystemUI.Instance.ShowWinCard(true);
                    Debug.Log("You win");
                    break;
                case GameOver.LOSE:
                    UnitTurnSystemUI.Instance.ShowGameOverCard(true);
                    Debug.Log("You lose");
                    break;
                case GameOver.DRAW:
                    Debug.Log("Draw");
                    break;
            }
        }

        /// <summary>Resets the selected unit's status and shading after it's turn has ended.</summary>
        private void ResetSelectedUnit()
        {
            selectedUnit.SetSelectedStatus(false);
            selectedUnit.UpdateUnitShade();
            selectedUnit.SetTurnStatus(true);
        }

        /// <summary>Selects a unit and updates its status and shading.</summary>
        /// <param name="unit">The unit to be selected.</param>
        private void SelectUnit(Unit unit)
        {
            selectedUnit = unit;
            selectedUnit.SetTurnStatus(true);
            selectedUnit.SetSelectedStatus(true);
            selectedUnit.UpdateUnitShade();
            selectedUnit.ResetActionStatus();
            Pathfinding.Instance.SetupPath(selectedUnit.GetUnitType());
            UnitActionSystem.Instance.SetSelectedAction(unit.GetAction<MoveAction>());
            OnUnitListChanged?.Invoke(this, EventArgs.Empty);
        }

        private Unit GetAndRemoveFirstUnit()
        {
            if (waitingUnitList.Count == 0) return null;

            Unit firstUnit = waitingUnitList[0];
            waitingUnitList.RemoveAt(0);

            return firstUnit;
        }

        public void RemoveUnitFromList(Unit unit)
        {
            if (waitingUnitList.Count == 0) return;

            waitingUnitList.Remove(unit);
            OnUnitListChanged?.Invoke(this, EventArgs.Empty);
            return;
        }

        private void NextTurn()
        {
            waitingUnitList = SortFromFastestUnit();
            selectedUnit = null;
            turnRounds++;
            SetTurnCount(turnRounds);

            UnitActionSystem.Instance.HideActionUI();
            UnitTurnSystemUI.Instance.ShowRoundCard(true);
            UnitActionSystemUI.Instance.ShowActionButtonBlocker(false);
            StartCoroutine(ScreenWait(UnitTurnSystemUI.Instance.ShowRoundCard));
        }

        public Unit GetSelectedUnit() => selectedUnit;
        public List<Unit> GetWaitingUnitList() => waitingUnitList;
        public int GetTurnNumber() => turnRounds;

        private void SetTurnCount(int number) => turnCountUI.text = $"TURN {number}";
    }

}