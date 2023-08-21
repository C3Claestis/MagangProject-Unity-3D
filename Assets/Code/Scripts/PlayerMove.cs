using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Beziercurve path1to2, path2to3, path3to2, path2to1;
    public float speed = 5f;    

    private float t = 0f;
    public byte path;
    private void Update()
    {
        switch (path)
        {            
            case 1:
                PathSatuToDua();              
                break;
            case 2:
                PathDuaToTiga();
                break;
            case 3:
                PathTigaToDua();
                break;
            case 4:
                PathDuaToSatu();
                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            t = 0;
            
            if(path == 0)
            {
                path = 1;
            }else if(path == 1)
            {
                path = 2;
            }else if (path == 4)
            {
                path = 1;
            }else if(path == 3)
            {
                path = 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            t = 0;

            if (path == 2)
            {
                path = 3;
            }else if (path == 1)
            {
                path = 4;
            }else if (path == 3)
            {
                path = 4;
            }
        }
    }        

    void PathSatuToDua()
    {
        t += speed * Time.deltaTime / path1to2.GetPathLength();
        t = Mathf.Clamp01(t);

        Vector3 targetPosition = path1to2.GetPointOnBezier(t);
        transform.position = targetPosition;
    }
    void PathDuaToTiga()
    {
        t += speed * Time.deltaTime / path2to3.GetPathLength();
        t = Mathf.Clamp01(t);

        Vector3 targetPosition = path2to3.GetPointOnBezier(t);
        transform.position = targetPosition;
    }
    void PathTigaToDua()
    {
        t += speed * Time.deltaTime / path3to2.GetPathLength();
        t = Mathf.Clamp01(t);

        Vector3 targetPosition = path3to2.GetPointOnBezier(t);
        transform.position = targetPosition;
    }
    void PathDuaToSatu()
    {
        t += speed * Time.deltaTime / path2to1.GetPathLength();
        t = Mathf.Clamp01(t);

        Vector3 targetPosition = path2to1.GetPointOnBezier(t);
        transform.position = targetPosition;
    }
}
