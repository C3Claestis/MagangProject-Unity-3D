using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    public GameObject panel;
    public Button toggleButton;

    private bool isPanelActive = false;

    private void Start()
    {
        toggleButton.onClick.AddListener(TogglePanelState);
    }

    private void TogglePanelState()
    {
        isPanelActive = !isPanelActive;
        panel.SetActive(isPanelActive);
    }
}
