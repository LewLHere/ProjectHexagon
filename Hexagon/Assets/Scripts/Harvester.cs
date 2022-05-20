using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] GameObject[] closeTiles = new GameObject[6];
    [SerializeField] GameObject harvesterPrefab;
    Tile tile;
    TileGroups tg;
    ResourceManager resourceManager;
  
    


    
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        tg = FindObjectOfType<TileGroups>();
        StartCoroutine(BuildHarvester());
        closeTiles = tile.GetClosestTiles(tile.gameObject);
        GameObject harvesterInstance =  Instantiate(harvesterPrefab, transform);
        harvesterInstance.GetComponent<HarvestMovement>().SetHarvester(this);
       
    }

   public void SetTile(Tile tileToSet)
    {
        tile = tileToSet;
    }
    IEnumerator BuildHarvester()
    {
        yield return new WaitForSeconds(.5f);
        tile.GetClosestTiles(tile.gameObject);
    }

    public GameObject[] GetClosestTiles(GameObject tileGO)
    {
     
        int neighboredTilesIndex = 0;
        GameObject[] neighboredTiles = new GameObject[6];



        for (int i = 0; i < tg.GetAll().Count; i++)
        {
            float distance = Vector3.Distance(tile.transform.position, tg.GetAll()[i].transform.position);

            if (distance < tile.GetMinDistance()  && distance != 0)
            {

                neighboredTiles[neighboredTilesIndex] = tg.GetAll()[i];
                neighboredTilesIndex++;
            }
        }
        return neighboredTiles;
    }
}
