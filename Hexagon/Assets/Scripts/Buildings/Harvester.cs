using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : Building
{
  
    [SerializeField] GameObject harvesterPrefab;
    ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = GetNeighboredTiles();
        StartCoroutine("SpawnHarvester");
        buildingIndex = 0;
      


    }

    IEnumerator SpawnHarvester()
    {
        yield return new WaitForSeconds(1);
        GameObject harvesterInstance = Instantiate(harvesterPrefab,transform);
        harvesterInstance.GetComponent<HarvestMovement>().SetHarvester(this.gameObject);
        neighboredTiles = GetNeighboredTiles();


    }

}
