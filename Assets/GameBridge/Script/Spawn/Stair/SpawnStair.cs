using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStair : MonoBehaviour
{
    public GameObject stairStepPrefab;
    public int numberOfSteps = 10;
    public float stepHeight = 0.2f;
    public float stepDepth = 1.0f;

    void Start()
    {
        GenerateStairs();
    }

    void GenerateStairs()
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            GameObject step = Instantiate(stairStepPrefab);
            step.transform.position = new Vector3(0, i * stepHeight, i * stepDepth);
        }
    }
}
