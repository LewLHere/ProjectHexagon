using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject[] neighboredTiles = new GameObject[6];
    [SerializeField] float minDistance = 6;
    public GameObject tile;
    TileGroups tg;

    void Start()
    {
        tg = FindObjectOfType<TileGroups>();
        GetClosestTiles(tile);
    }

    void GetClosestTiles(GameObject tile)
    {

        int neighboredTilesIndex = 0;
        neighboredTiles[0] = null;
        neighboredTiles[1] = null;
        neighboredTiles[2] = null;
        neighboredTiles[3] = null;
        neighboredTiles[4] = null;
        neighboredTiles[5] = null;



        for (int i = 0; i < tg.GetAll().Count; i++)
        {
            float distance = Vector3.Distance(tile.transform.position, tg.GetAll()[i].transform.position);

            if (distance < minDistance && distance != 0)
            {
                neighboredTiles[neighboredTilesIndex] = tg.GetAll()[i];
                neighboredTilesIndex++;
            }
        }

    }


}
