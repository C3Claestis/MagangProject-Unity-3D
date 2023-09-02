using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] SplinePositioner splinePositioner;
    [SerializeField] double moveSpeed = 5f;
    private double percent = 0.0;
    [SerializeField] private bool reset = false;
    [SerializeField] private bool reverse = false;
    private bool isMoving = true;

    private void Update()
    {
        if (reset){
            reset = false;
            isMoving = true;
            percent = 0.0;
        }

        if (!isMoving) return;
        if (reverse){
            percent -= moveSpeed * Time.deltaTime;
            if (percent <= 0.0) isMoving = false;
            
        } 
        else {
            if (percent >= 1.0) isMoving = false;
            percent += moveSpeed * Time.deltaTime;
        }
        percent = Mathf.Clamp01((float)percent);
        splinePositioner.SetPercent(percent);
    }
}
