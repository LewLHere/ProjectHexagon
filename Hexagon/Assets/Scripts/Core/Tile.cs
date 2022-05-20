using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject[] neighboredTiles = new GameObject[6];
    [SerializeField] float minDistance;
    
    public bool isOccupied = false;
    [SerializeField] Building building;
    TileGroups tg;
    BuildManager bm;
   
   
    private void Awake()
    {
       
        minDistance = 6;
        tg = FindObjectOfType<TileGroups>();
        bm = FindObjectOfType<BuildManager>();


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

    private void OnMouseOver()
    {
        if (building != null)
        {

            bm.GetBuildingCost(building.gameObject, building.GetLevel() + 1);
            return;
        }
      
          
            if (bm.GetButtonSelected() == 1)
            { bm.GetBuildingCost(bm.GetHarvester().gameObject, 0); }

            if (bm.GetButtonSelected() == 2)
            { bm.GetBuildingCost(bm.GetTower().gameObject, 0); }

           // if (bm.GetButtonSelected() == 3)                                                    // For more towers.
           // { bm.GetBuildingCost(building.gameObject, building.GetLevel() + 1); }

    }

   
    public float GetMinDistance()
            { 
        return minDistance; 
    }
    
    public GameObject[] GetNeighboredTiles(GameObject tile)
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
        GetNeighboredTiles(gameObject);
        neighboredTiles[0].SetActive(true);
        neighboredTiles[1].SetActive(true);
        neighboredTiles[2].SetActive(true);
        neighboredTiles[3].SetActive(true);
        neighboredTiles[4].SetActive(true);
        neighboredTiles[5].SetActive(true);
    }

    public void SetBuilding(Building instanceBuilding)
    {
        building = instanceBuilding;
    }
    public Building GetBuilding()
    {
        return building;
    }
  
}

