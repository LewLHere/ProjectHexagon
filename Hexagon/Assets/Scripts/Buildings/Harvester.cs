using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : Building
{
  
    [SerializeField] GameObject harvesterPrefab;
    [SerializeField] float timeToResend;
    [SerializeField] float timeToWaitAtTile;
    ResourceManager resourceManager;
   
   
    private void Start()
    {
       
        resourceManager = FindObjectOfType<ResourceManager>();
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = tile.GetNeighboredTiles(tile.gameObject);
        StartCoroutine("WaitUntilBuilt");
       
        buildingIndex = 0;
        CreateOccupiedTiles();



    }

    IEnumerator WaitUntilBuilt()
    {
        yield return new WaitForSeconds(buildTime);
       
        StartCoroutine("SpawnHarvester");
    }
    private void CreateOccupiedTiles()
    {
        for (int i = 0; i < neighboredTiles.Length; i++)
        {
            neighboredTiles[i].GetComponent<Tile>().SetOccupied();
          //  if (neighboredTiles[i].activeSelf)
            {
                neighboredTiles[i].GetComponent<Tile>().OccupiedByHarvester();
            }
        }
    }

    IEnumerator SpawnHarvester()
    {
        yield return new WaitForSeconds(timeToResend);
        GameObject harvesterInstance = Instantiate(harvesterPrefab,new Vector3(transform.position.x, 3f,transform.position.z), Quaternion.identity);
        harvesterInstance.GetComponent<HarvestMovement>().SetHarvester(this.gameObject);
        neighboredTiles = tile.GetNeighboredTiles(tile.gameObject);


    }


    public void SetTriggerColour(string triggerColour)
    {
        anim.SetTrigger(triggerColour);
    }

}
