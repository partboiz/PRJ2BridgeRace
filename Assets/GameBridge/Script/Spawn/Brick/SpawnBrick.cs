using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrick : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int numberOfRows = 5;
    [SerializeField] private int numberOfColumns = 5;
    [SerializeField] private float spacing = 2f;
    [SerializeField] private Transform parentObject;
    GameObject newBrick;
    Vector3 spawnPosition;
    private void Start()
    {
        SpawnBricks();
    }
    public void SpawnBricks()
    {
        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                spawnPosition = transform.position + new Vector3(col * spacing, 0, row * spacing);
                newBrick = Instantiate(brickPrefab, spawnPosition, Quaternion.identity, parentObject);
                Material randomBrickColors = DataManager.Instance.colorData.GetColorBrick();
                newBrick.GetComponent<Renderer>().material = randomBrickColors;
            }
        }
    }
}
