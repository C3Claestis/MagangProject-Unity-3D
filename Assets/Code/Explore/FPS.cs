namespace Nivandria.Explore
{
    using UnityEngine;    
    using UnityEngine.UI;

    public class FPS : MonoBehaviour
    {
        public Text fpsText;
        float deltaTime;

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS : " + Mathf.Ceil(fps).ToString();
        }
    }
}