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
    GameObject tileForNecroToMoveTo;
    [SerializeField] List<GameObject> spawnTileList;
    [SerializeField] List<GameObject> finishTileList;
    [SerializeField] GameObject[] mobPrefab = null;
    [SerializeField] int indexToSpawn;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI currentWave;
    [SerializeField] GameObject necromancer;
    [SerializeField] float necroAnimLength;
    [SerializeField] float necromancerRotationSpeed = 1f;

    [SerializeField] AudioSource newWaveAudio;
    int difficulty;
    float countdown;
    public float tileHeight = 2.5f;
    TileGroups tg;
    bool readyForNextWave;
    bool updateCountdownNow;
    System.Random rnd;
    CameraController mainCamera;
    ManageScene manageScene;
    public bool hpAreOn = false;
    
    bool necroReadyToSpawn = false;
    bool necroAnimationDone = true;
    Animator necroAnim;
    public event EventHandler ShowHP;
    public event EventHandler HideHP;
    private void Awake()
    {
       
    }
    private void Start()
    {
        necroAnim = necromancer.GetComponentInChildren<Animator>();
        manageScene = FindObjectOfType<ManageScene>();
        difficulty = manageScene.GetDifficulty();
        mainCamera = FindObjectOfType<CameraController>();
        tg = FindObjectOfType<TileGroups>();

        StartCoroutine("StartWave", 0);
        CreateNextSpawnTile();

    }

    void Update()
    {
        if (randomSpawnTile != null)
        {
            if (Vector3.Distance(new Vector3(necromancer.transform.position.x, 0, 0), new Vector3(randomSpawnTile.transform.position.x, 0, 0)) > .3f && necroAnimationDone)
            {
                necromancer.transform.position = Vector3.MoveTowards(necromancer.transform.position, new Vector3(randomSpawnTile.transform.position.x, necromancer.transform.position.y, randomSpawnTile.transform.position.z + 5), 5f * Time.deltaTime);
                Vector3 dir = randomSpawnTile.transform.position - necromancer.transform.position;
                dir.y = 0;
                Quaternion rot = Quaternion.LookRotation(dir);
                necromancer.transform.rotation = Quaternion.Slerp(transform.rotation, rot, necromancerRotationSpeed * Time.deltaTime);
            }
            else {
                
                necroReadyToSpawn = true; }
           
        }

        if (updateCountdownNow)
        {
            countdownText.text = ("Next wave in " + countdown);
        }
        if (readyForNextWave)
        {
            StartCoroutine("StartWave", waveNumber);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (!hpAreOn)
            {
                ShowHP?.Invoke(this, EventArgs.Empty);
                hpAreOn = true;
            }
            else if (hpAreOn)
            {
               
                HideHP?.Invoke(this, EventArgs.Empty);
                hpAreOn = false;
            }
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
        newWaveAudio.Play();
       int mobsSpawnedThisWave = 0;
       wavesOnCurrentBoard++;
        while (mobsSpawnedThisWave < waveSize[wave])
        {
            necroAnim.SetTrigger("Summon");
            yield return new WaitForSeconds(necroAnimLength);
            SpawnEnemy(waveNumber,0);
            mobsSpawnedThisWave++;
            countdownText.text = "Mobs remaining: " + (waveSize[wave] - mobsSpawnedThisWave);
            if (mobsSpawnedThisWave == waveSize[wave])
            {
                StartCoroutine("StartCountdown");

            }
            yield return new WaitForSeconds(timeBetweenMobs[wave]-necroAnimLength);
            
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
       
        
        if (randomSpawnTile != null)
        {



            GameObject mobInstance;
            mobInstance = Instantiate(mobPrefab[enemyTier], new Vector3(randomSpawnTile.transform.position.x, 2.5f, randomSpawnTile.transform.position.z+1.5f), Quaternion.identity); //gogo
            mobInstance.GetComponent<MobMover>().SetSpeed(speed[waveNumber]);
            mobInstance.GetComponent<MobHealth>().SetHP(hpMultiplier[difficulty]*hp[waveNumber] + hp[waveNumber] * 2 * enemyTier );

            mobInstance.GetComponent<MobMover>().SetLastSpottedOn(randomSpawnTile);
            mobInstance.GetComponent<MobMover>().SetTarget(randomSpawnTile);
        }
        CreateNextSpawnTile();
    }

   
    void CreateNextSpawnTile()
    {
        int y = 0;
        spawnTileList = tg.GetEnemySpawnTiles();
        finishTileList = tg.GetEnemyFinishTiles();
        for (int i = 0; i < spawnTileList.Count; i++)
        {
            if (spawnTileList[i] == null)
            { break; }
            else
            {
                y++;              // Count how many spawntiles exist

            }
        }


        rnd = new System.Random();
        indexToSpawn = rnd.Next(y);                     // Pick Random SpawnTile
        randomSpawnTile = spawnTileList[indexToSpawn];
    }
    public int GetWave()
    { return waveNumber; }

   
      
   
  }
    


    

