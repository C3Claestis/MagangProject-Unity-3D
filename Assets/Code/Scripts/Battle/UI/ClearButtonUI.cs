namespace Nivandria.Battle.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Explore;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ClearButtonUI : MonoBehaviour
    {
        public void BackToTrainingButton()
        {
            SceneManager.LoadScene(3);
        }
    }

}