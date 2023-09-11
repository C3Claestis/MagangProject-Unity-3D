namespace Nivandria.Battle
{
    using UnityEngine;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using System;

    public class Unit : MonoBehaviour
    {
        #region  Character Status Variables
        [SerializeField] private string characterName = "Base Unit";
        [SerializeField] private int baseHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] private int basePhysicalAttack = 12;
        [SerializeField] private int currentPhysicalAttack;
        [SerializeField] private int baseAgility = 7;
        [SerializeField] private int currentAgility;
        [SerializeField] private float baseMagicalAttack = 11;
        [SerializeField] private float currentMagicalAttack;
        [SerializeField] private float basePhysicalDefense = 7;
        [SerializeField] private float currentPhysicalDefense;
        [SerializeField] private float baseMagicalDefense = 6;
        [SerializeField] private float currentMagicalDefense;
        [SerializeField] private bool hasMove = false;
        [SerializeField] private bool isSelected = false;
        #endregion

        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private MoveType moveType = MoveType.Normal;
        private GridPosition gridPosition;
        private BaseAction[] baseActionArray;

        private void Awake()
        {
            baseActionArray = GetComponents<BaseAction>(); //Store all the component attached to this unit that extend BaseAction;
        }

        private void Start()
        {
            currentHealth = baseHealth;
            currentPhysicalAttack = basePhysicalAttack;
            currentMagicalAttack = baseMagicalAttack;
            currentPhysicalDefense = basePhysicalDefense;
            currentMagicalDefense = baseMagicalDefense;
            currentAgility = baseAgility;

            gameObject.name = characterName;
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            GetAction<MoveAction>().SetMoveType(moveType);

            ChangeUnitShade();
        }

        /// <summary>Change unit material shading based on the selection status.</summary>
        public void ChangeUnitShade()
        {
            Material newMaterial = skinnedMeshRenderer.material;
            newMaterial.color = isSelected ? new Color(0.9f, 0.9f, 0.9f, 1f) : new Color(0.4f, 0.4f, 0.4f, 1f);
            skinnedMeshRenderer.material = newMaterial;
        }

        /// <summary>Updates the grid position of the unit based on its current world position.</summary>
        public void UpdateUnitGridPosition()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }

        #region Getter Setter
        public T GetAction<T>() where T : BaseAction
        {
            foreach (BaseAction baseAction in baseActionArray)
            {
                if (baseAction is T)
                {
                    return (T)baseAction;
                }
            }

            return null;
        }
        public GridPosition GetGridPosition() => gridPosition;
        public BaseAction[] GetBaseActionArray() => baseActionArray;

        public int GetAgility() => currentAgility;
        public bool GetMoveStatus() => hasMove;
        public string GetCharacterName() => characterName;

        public void SetMoveStatus(bool hasMove) => this.hasMove = hasMove;
        public void SetSelectedStatus(bool isSelected) => this.isSelected = isSelected;
        #endregion
    }
}