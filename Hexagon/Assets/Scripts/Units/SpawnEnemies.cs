using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] int waveNumber;
    [SerializeField] int[] waveSize;
    [SerializeField] float[] timeBetweenMobs;
    [SerializeField] float[] timeBetweenWaves;
    [SerializeField] int wavesPerBoard;
    [SerializeField] int wavesOnCurrentBoard;
    [SerializeField] GameObject randomSpawnTile;
    
    [SerializeField] List<GameObject> spawnTileList;
    [SerializeField] List<GameObject> finishTileList;
    [SerializeField] GameObject mobPrefab = null;
    [SerializeField] bool hasSpawnTile = false;
    [SerializeField] int indexToSpawn;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI currentWave;
    float countdown;
    public float tileHeight = 2.5f;
    TileGroups tg;
    bool readyForNextWave;
    bool updateCountdownNow;
    System.Random rnd;

    private void Start()
    {
        tg = FindObjectOfType<TileGroups>();

        StartCoroutine("StartWave", 0);


    }

    void Update()
    {
        if (updateCountdownNow)
        {
            countdownText.text = ("Next wave in " + countdown);
        }
        if (readyForNextWave)
        {
            StartCoroutine("StartWave", waveNumber);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnEnemy();
        }
    }

  


     IEnumerator StartWave(int wave)
    {
        currentWave.text = "Current wave: " + wave;
        readyForNextWave = false;
      
       int mobsSpawnedThisWave = 0;
        wavesOnCurrentBoard++;
        while (mobsSpawnedThisWave <= waveSize[wave])
        {
            countdownText.text = "Mobs remaining: " + (waveSize[wave] - mobsSpawnedThisWave);
            SpawnEnemy();
            mobsSpawnedThisWave++;
         

            yield return new WaitForSeconds(timeBetweenMobs[wave]);
            
        }
        StartCoroutine("StartCountdown");
        yield return new WaitForSeconds(timeBetweenWaves[wave]);
        updateCountdownNow = false;
        waveNumber++;
        if (wavesOnCurrentBoard >= wavesPerBoard)
        {
            tg.IncreaseBoardSize();
            wavesOnCurrentBoard = 0;

        }
        readyForNextWave = true;
       


    }
   
  IEnumerator StartCountdown()
    {
        updateCountdownNow = true;
        countdown = timeBetweenWaves[waveNumber];
        while (countdown>0)
        {
            countdown--;
                yield return new WaitForSeconds(1); 
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
    


    

