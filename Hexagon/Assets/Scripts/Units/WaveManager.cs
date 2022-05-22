using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    SpawnEnemies spawner;
    [SerializeField] int wave;
    [SerializeField] int boardSize;

    private void Start()
    {
        spawner = FindObjectOfType<SpawnEnemies>();


    }
}
