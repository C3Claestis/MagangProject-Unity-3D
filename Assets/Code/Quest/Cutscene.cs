using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cutscene : MonoBehaviour
{
    [SerializeField] float Timer;    
    [SerializeField] int ValueQuest;
    [SerializeField] byte BackSceneValue;    
    void Update()
    {
        StartCoroutine(Wait());        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene(BackSceneValue);
    }
}
