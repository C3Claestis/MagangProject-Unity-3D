using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelQuest;

    public void PanelQuest()
    {
        panelQuest.SetActive(true);
        panelMenu.SetActive(true);
    }

    public void PanelMenu()
    {
        panelMenu.SetActive(true);
        panelQuest.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
