using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    [SerializeField] internal ColorSO colorData;
}
