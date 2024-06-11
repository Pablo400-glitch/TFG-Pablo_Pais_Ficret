using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField]
    private int currentPoints;

    [SerializeField]
    private CubeSpawner numberOfCubes;

    private int maxPoints;

    private void Awake()
    {
        maxPoints = numberOfCubes.numCubes;
    }

    public void AddPoints()
    {
        currentPoints++;
        Debug.Log(currentPoints);
        EventManager.PointsUIUpdate(currentPoints, maxPoints);
    }
}
