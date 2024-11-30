using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorSO : ScriptableObject
{
    public Material[] colors;
    Material colorCharacter;
    List<int> availableColorIndices = new List<int>();

    void InitializeAvailableColors()
    {
        availableColorIndices.Clear();
        for (int i = 0; i < colors.Length; i++)
        {
            availableColorIndices.Add(i);
        }
    }

    public Material GetColorBrick()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    public Material GetColorCharacter()
    {
        if (availableColorIndices.Count == 0)
        {
            InitializeAvailableColors();
        }

        int randomIndex = Random.Range(0, availableColorIndices.Count);
        int selectedColorIndex = availableColorIndices[randomIndex];

        availableColorIndices.RemoveAt(randomIndex);

        colorCharacter = colors[selectedColorIndex];
        return colorCharacter;
    }

    public Material GetColorBrickOnCharacter()
    {
        return colorCharacter;
    }
}