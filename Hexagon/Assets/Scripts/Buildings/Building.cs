using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected Mesh[] buildingMeshes;
   [SerializeField] protected GameObject[] neighboredTiles = new GameObject[6];
   [SerializeField] protected float minDistance = 6;
   [SerializeField] protected Tile tile;
    [SerializeField] protected int[] costWhite = new int[5];
   [SerializeField] protected int[] costBlue= new int[5];
   [SerializeField] protected int[] costGreen= new int[5];
   [SerializeField] protected int[] costRed= new int[5];
    [SerializeField] protected int level = 0;
   [SerializeField] protected int buildingIndex;
    [SerializeField] protected float[] range = new float[5];
    [SerializeField] protected GameObject[] rangeGO;
    protected TileGroups tg;
    protected BuildManager bm;
    public int maxLevel = 4;

                                                                                            // No Start() in here or it overrides all Sub-Classes.
 
  
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
            Debug.Log("Level Up!");
            level++;
            gameObject.GetComponent<MeshFilter>().mesh = buildingMeshes[level];

        }
        else Debug.Log("Has Max Level");
        
    }

    public GameObject[] GetRangeGO()
    { return rangeGO; }

    public int GetBuildingIndex()
    { return buildingIndex; }
}
