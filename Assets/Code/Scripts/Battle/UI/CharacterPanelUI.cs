namespace Nivandria.Battle.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class CharacterPanelUI : MonoBehaviour
    {
        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider xpBar;

        private void Start()
        {
            UnitTurnSystem.Instance.OnUnitListChanged += UnitTurnSystem_OnUnitListChanged;
        }

        private void UpdateCharacterPanel()
        {
            Unit unit = UnitTurnSystem.Instance.GetSelectedUnit();

            characterName.text = unit.GetCharacterName();
            characterImage.sprite = unit.GetUnitIcon();
            hpBar.value = (float)unit.GetCurrentHealth() / unit.GetBaseHealth();
        }

        private void UnitTurnSystem_OnUnitListChanged(object sender, EventArgs e)
        {
            UpdateCharacterPanel();
        }
    }

}