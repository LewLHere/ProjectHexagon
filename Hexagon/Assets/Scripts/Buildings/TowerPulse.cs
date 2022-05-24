using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPulse : Building
{
    [SerializeField] float[] fireRate = new float[5];
    [SerializeField] GameObject pulsePrefab;
    [SerializeField] MobHealth[] mobsInRange;
    [SerializeField] int[] damage;

    GameObject target;
    bool readyToPulse = true;
    private void Start()
    {
        mobsInRange = new MobHealth[300];
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = tile.GetNeighboredTiles(tile.gameObject);
        buildingIndex = 3;
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
        mobsOnTile = tile.GetMobsOnTile();                              // For this tile
        for (int i = 0; i < mobsOnTile.Length; i++)
        {
            if (mobsOnTile[i] != null)
            {
                mobsOnTile[i].TakeDamage(damage[level]);
                mobsOnTile[i].GetComponent<ParticleSystem>().Play();
            }
        }


        for (int j = 0; j < neighboredTiles.Length; j++)                                 // For all surrounding tiles
           {
            
           mobsOnTile= neighboredTiles[j].GetComponent<Tile>().GetMobsOnTile();
           
            for (int i = 0; i < mobsOnTile.Length; i++)
            {
                if (mobsOnTile[i] != null)
                { 
                mobsOnTile[i].TakeDamage(damage[level]);
                mobsOnTile[i].GetComponent<ParticleSystem>().Play();
                }
            }
           }
        
       
        yield return new WaitForSeconds(fireRate[level]);

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

