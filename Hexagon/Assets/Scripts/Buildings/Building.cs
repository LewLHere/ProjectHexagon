using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected GameObject buildingPrefab;
   [SerializeField] protected GameObject[] neighboredTiles = new GameObject[6];
   [SerializeField] protected float minDistance = 6;
   [SerializeField] protected Tile tile;
    [SerializeField] protected int[] costWhite = new int[5];
   [SerializeField] protected int[] costBlue= new int[5];
   [SerializeField] protected int[] costGreen= new int[5];
   [SerializeField] protected int[] costRed= new int[5];
    [SerializeField] protected int level = 0;
   [SerializeField] protected int buildingIndex;
    protected TileGroups tg;
    protected BuildManager bm;
    public int maxLevel = 4;

                                                                                            // No Start() in here or it overrides all Sub-Classes.
    public GameObject[] GetNeighboredTiles()
    {
        GameObject[] neighboredTilesInstance = new GameObject[6];
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
                neighboredTilesInstance[neighboredTilesIndex] = tg.GetAll()[i];
                neighboredTilesIndex++;
            }
        }
        return neighboredTilesInstance;
    }

  
    public Tile GetTile()
    { return tile;
    }

    public void SetTile(Tile setTile)
    {
        tile = setTile;
    }
    public GameObject GetNeighboredTile(int number)
    {
        return neighboredTiles[number];
    }

    public int GetCostWhite(int level)
    {
        return costWhite[level];
    }
    public int GetCostBlue(int level)
    {
        return costBlue[level];
    }
    public int GetCostGreen(int level)
    {
        return costGreen[level];
    }
    public int GetCostRed(int level)
    {
        return costRed[level];
    }
    public int GetLevel()
    {
        return level;
    }
    public void IncreaseBuildingLevel()
    {
        if (level < maxLevel)
        {
            level++;
        }
        else Debug.Log("Has Max Level");
    }

    public int GetBuildingIndex()
    { return buildingIndex; }
}
