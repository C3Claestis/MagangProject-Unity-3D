using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridObjectDebug;
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 4;
    [SerializeField] private float cellSize = 2;

    private GridSystem gridSystem;

    private void Start(){
        gridSystem = new GridSystem(width, height, cellSize);
        gridSystem.CreateDebugObjects(gridObjectDebug);
    }

    private void Update(){
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    } 

}