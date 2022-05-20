using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
   
    [SerializeField] GameObject randomSpawnTile;
   
    
    [SerializeField] List<GameObject> spawnTileList;
    [SerializeField] List<GameObject> finishTileList;
    [SerializeField] GameObject mobPrefab = null;
    [SerializeField] bool hasSpawnTile = false;
    [SerializeField] int indexToSpawn;
    public float tileHeight = 2.5f;
    TileGroups tg;
    System.Random rnd;
    private void Start()
    {
        tg = FindObjectOfType<TileGroups>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
         
            SpawnEnemy();



        }
    }

    private void SpawnEnemy()
    {

        int y = 0;
        spawnTileList = tg.GetEnemySpawnTiles();
        finishTileList = tg.GetEnemyFinishTiles();
        for (int i= 0; i < spawnTileList.Count; i++)
        {
            if(spawnTileList[i] == null)
            { break; }
            else
            { y++;              // Count how many spawntiles exist
              
            }
        }

       
                rnd = new System.Random();
                indexToSpawn = rnd.Next(y);                     // Pick Random SpawnTile
                randomSpawnTile = spawnTileList[indexToSpawn];
        if (randomSpawnTile != null)
        {
            GameObject mobInstance;
            mobInstance = Instantiate(mobPrefab, new Vector3(randomSpawnTile.transform.position.x, 2.5f, randomSpawnTile.transform.position.z), Quaternion.identity); //gogo
            mobInstance.GetComponent<MobMover>().SetLastSpottedOn(randomSpawnTile);
        }
            }
        }
    


    

