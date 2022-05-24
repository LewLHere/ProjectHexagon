using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] int waveNumber;
    [SerializeField] int[] waveSize;
    [SerializeField] float[] timeBetweenMobs;
    [SerializeField] float[] timeBetweenWaves;
    [SerializeField] float[] speed;
    [SerializeField] int[] hp;
    [SerializeField] int[] hpMultiplier;

    [SerializeField] int wavesPerBoard;
    [SerializeField] int wavesOnCurrentBoard;
    [SerializeField] GameObject randomSpawnTile;
    
    [SerializeField] List<GameObject> spawnTileList;
    [SerializeField] List<GameObject> finishTileList;
    [SerializeField] GameObject[] mobPrefab = null;
    [SerializeField] int indexToSpawn;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI currentWave;
    int difficulty;
    float countdown;
    public float tileHeight = 2.5f;
    TileGroups tg;
    bool readyForNextWave;
    bool updateCountdownNow;
    System.Random rnd;
    CameraController mainCamera;
    ManageScene manageScene;

    private void Awake()
    {
       
    }
    private void Start()
    {
        manageScene = FindObjectOfType<ManageScene>();
        difficulty = manageScene.GetDifficulty();
        mainCamera = FindObjectOfType<CameraController>();
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
            SpawnEnemy(waveNumber, 0);
        }
    }

  


     IEnumerator StartWave(int wave)
    {
        currentWave.text = "Current wave: " + wave;
        readyForNextWave = false;
        if (wave == 0)
        {
             yield return new WaitForSeconds(3); 
        }
       int mobsSpawnedThisWave = 0;
       wavesOnCurrentBoard++;
        while (mobsSpawnedThisWave < waveSize[wave])
        {
            
            SpawnEnemy(waveNumber,0);
            mobsSpawnedThisWave++;
            countdownText.text = "Mobs remaining: " + (waveSize[wave] - mobsSpawnedThisWave);
            if (mobsSpawnedThisWave == waveSize[wave])
            {
                StartCoroutine("StartCountdown");

            }
            yield return new WaitForSeconds(timeBetweenMobs[wave]);
            
        }
        if (wavesOnCurrentBoard >= wavesPerBoard)
        {
            {
                mainCamera.SetDefaultSize(tg.GetCurrentBoardSize() + 1);
            }
        }


        yield return new WaitForSeconds(timeBetweenWaves[wave] - timeBetweenMobs[wave]);
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
 
    public void SpawnEnemy(int waveNumber, int enemyTier)
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
            mobInstance = Instantiate(mobPrefab[enemyTier], new Vector3(randomSpawnTile.transform.position.x, 2.5f, randomSpawnTile.transform.position.z+1.5f), Quaternion.identity); //gogo
            mobInstance.GetComponent<MobMover>().SetSpeed(speed[waveNumber]);
            mobInstance.GetComponent<MobHealth>().SetHP(hpMultiplier[difficulty]*hp[waveNumber] + hp[waveNumber] * 2 * enemyTier );

            mobInstance.GetComponent<MobMover>().SetLastSpottedOn(randomSpawnTile);
        }
            }

    public int GetWave()
    { return waveNumber; }
  }
    


    

