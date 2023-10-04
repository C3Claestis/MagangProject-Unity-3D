namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using CodeMonkey.Utils;

    public class WindowPointer : MonoBehaviour
    {
        private Vector3 targetPos;
        private RectTransform rectTransform;
        [SerializeField] Transform player;
        [SerializeField] Camera uiCamera;

        void Awake()
        {
            GameObject targetObject = GameObject.Find("Kroco");
            targetPos = targetObject.transform.position;
            rectTransform = transform.Find("EnemyPointer").GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 toPos = targetPos;
            Vector3 fromPos = player.position;//Camera.main.transform.position;
            fromPos.z = 0f;
            Vector3 dir = (toPos - fromPos).normalized;
            float angle = UtilsClass.GetAngleFromVector(dir);
            rectTransform.localEulerAngles = new Vector3(0, 0, angle);

            Vector3 targetPosScreen = Camera.main.WorldToScreenPoint(targetPos);
            bool isOffScreen = targetPosScreen.x <= 0 || targetPosScreen.x >= Screen.width || targetPosScreen.y <= 0 || targetPosScreen.y >= Screen.height;

            if (isOffScreen)
            {
                Vector3 cappedTarget = targetPosScreen;
                if(cappedTarget.x <= 0) cappedTarget.x = 0f;
                if(cappedTarget.x >= Screen.width) cappedTarget.x = Screen.width;
                if(cappedTarget.y <= 0) cappedTarget.y = 0f;
                if(cappedTarget.y >= Screen.height) cappedTarget.y = Screen.height;

                Vector3 pointerWorldPos = uiCamera.ScreenToWorldPoint(cappedTarget);
                rectTransform.position = pointerWorldPos;
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
            }
        }
    }
}