using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPulse : Building
{
    [SerializeField] float[] fireRate = new float[5];
    [SerializeField] GameObject[] pulsePrefabs;
    [SerializeField] MobHealth[] mobsInRange;
    [SerializeField] int[] damage;

    GameObject target;
    bool readyToPulse = false;
    private void Start()
    {
        mobsInRange = new MobHealth[300];
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = tile.GetNeighboredTiles(tile.gameObject);
        buildingIndex = 3;
        StartCoroutine("WaitUntilBuilt");
    }
   
    IEnumerator WaitUntilBuilt()
    {
        yield return new WaitForSeconds(buildTime);
        readyToPulse = true;
    }
    public void TryToPulse()
    {

        if (readyToPulse)
        { readyToPulse = false;
            StartCoroutine("Pulse");
        }
    }

    IEnumerator Pulse()
    {
        MobHealth[] mobsOnTile;
        readyToPulse = false;
        
        /*   mobsOnTile = tile.GetMobsOnTile();                              // For this tile
           for (int i = 0; i < mobsOnTile.Length; i++)
           {
               if (mobsOnTile[i] != null)
               {
                   mobsOnTile[i].TakeDamage(damage[level]);
                   mobsOnTile[i].GetComponent<ParticleSystem>().Play();
               }
           }
        */

        for (int j = 0; j < neighboredTiles.Length; j++)                                 // For all surrounding tiles
           {
           
           mobsOnTile= neighboredTiles[j].GetComponent<Tile>().GetMobsOnTile();
           
            for (int i = 0; i < mobsOnTile.Length; i++)
            {
                if (mobsOnTile[i] != null)
                {
             
                mobsOnTile[i].TakeDamage(damage[level]);
              
                }
            }
            if (mobsOnTile.Length > 0)
            { pulsePrefabs[j].SetActive(true); }
           }
        
       
        yield return new WaitForSeconds(fireRate[level]);
        pulsePrefabs[0].SetActive(false);
        pulsePrefabs[1].SetActive(false);
        pulsePrefabs[2].SetActive(false);
        pulsePrefabs[3].SetActive(false);
        pulsePrefabs[4].SetActive(false);
        pulsePrefabs[5].SetActive(false);
        readyToPulse = true;
    }

    public bool GetReadyToPulse()
    { return readyToPulse; }
    public int GetDamage()
    { return damage[level]; }

    public float GetFireRate()
    { return fireRate[level];
    }
 
}

