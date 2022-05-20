using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject[] neighboredTiles = new GameObject[6];
    [SerializeField] float minDistance;
    public bool isOccupied = false;
    public Harvester harvester;
    public Tower tower;
    TileGroups tg;
   
   
    private void Awake()
    {
       
        minDistance = 6;
        tg = FindObjectOfType<TileGroups>();


        Material mat = GetComponent<MeshRenderer>().material;

       

        if (mat.name == null) { return; }

        if (mat.name.Contains("Red"))
        {
            tag = "Red";
        }
        else if (mat.name.Contains("Green"))
        {
            tag = "Green";
        }
        else if (mat.name.Contains("Blue"))
        {
            tag = "Blue";
        }
        else if (mat.name.Contains("White"))
        {
            tag = "White";
        }

        tg.CreateTileGroups();
      
    }

    public float GetMinDistance()
            { 
        return minDistance; 
    }
    
    public GameObject[] GetClosestTiles(GameObject tile)
    {

        int neighboredTilesIndex = 0;



       
        for (int i = 0; i < tg.GetAll().Count; i++)
        {
            float distance = Vector3.Distance(tile.transform.position, tg.GetAll()[i].transform.position);

            if (distance < minDistance && distance != 0)
            {
              
                neighboredTiles[neighboredTilesIndex] = tg.GetAll()[i];
                neighboredTilesIndex++;
            }
        }
        return neighboredTiles;
    }
  

    public void ActivateAllNeighbors()
    {
        GetClosestTiles(gameObject);
        neighboredTiles[0].SetActive(true);
        neighboredTiles[1].SetActive(true);
        neighboredTiles[2].SetActive(true);
        neighboredTiles[3].SetActive(true);
        neighboredTiles[4].SetActive(true);
        neighboredTiles[5].SetActive(true);
    }
}

