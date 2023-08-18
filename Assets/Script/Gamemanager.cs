using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{  
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
