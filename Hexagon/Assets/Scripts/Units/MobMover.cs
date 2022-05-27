using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMover : MonoBehaviour
{
    [SerializeField] float distanceToNextTarget;
    [SerializeField] GameObject currentTarget;
    [SerializeField] GameObject[] closestTiles = null;
    [SerializeField] GameObject isCurrentlyOn = null;
    [SerializeField] float distanceOfTwoTiles = 6f;
    [SerializeField] float speed;
    [SerializeField] float startSpeed = 5f;
    
    [SerializeField] float redMultiplier;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject shieldGO;
    [SerializeField] int tier;
    [SerializeField] float birthTime;
    [SerializeField] float dieTime;
    [SerializeField] float dieTimeWhenThrough = 2f;

    [SerializeField] Animator anim;
    [SerializeField] GameObject mobGO;
    [SerializeField] Material[] trailMats;
    [SerializeField] AudioSource[] leakedAudios;
    [SerializeField] AudioSource gameOverAudio;
    [SerializeField] AudioSource[] dieSounds;
    SpawnEnemies spawnEnemies;
    ManageScene manageScene;
    bool nowMove = false;
    bool isDieing = false;
    float startRotationSpeed = 100;
    public string colour;
    TrailRenderer trail;
    MobHealth mh;
    GameObject[] all;
   
    [SerializeField] GameObject lastTileSpottedOn;
    TileGroups tg = null;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        mobGO.SetActive(false);
        mh = GetComponent<MobHealth>();
        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        tg = FindObjectOfType<TileGroups>();

    }
    private void Start()
    {
        manageScene = FindObjectOfType<ManageScene>();
        all = tg.GetAll().ToArray();
      
        StartCoroutine("WaitForBirth");
      
      
      
        

    }

    private void Update()
    {
        Vector3 dir = currentTarget.transform.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        if(!mobGO.activeSelf)
        { mobGO.SetActive(true); }
        mh.UpdateHpTextRotation();
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, startRotationSpeed * Time.deltaTime);
        if (!nowMove) return;
        distanceToNextTarget = Vector3.Distance(new Vector3(transform.position.x, spawnEnemies.tileHeight, transform.position.z), new Vector3(currentTarget.transform.position.x, spawnEnemies.tileHeight, currentTarget.transform.position.z));
        if (distanceToNextTarget > distanceOfTwoTiles / 30)
        {
            if (isDieing) return;
            
                MoveToTarget();
            
        }
        else
        {
            GetNextTile();
        }

       

    }


    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "White" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Red")
        {
            colour = other.gameObject.tag;                                             // Default Settings.
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            speed = startSpeed;
            shieldGO.SetActive(false);

            if (colour == "White")
            {
                trail.material = trailMats[0];
                speed = startSpeed;
            }
            else if (colour == "Blue")
            {
                trail.material = trailMats[1];
                shieldGO.SetActive(true);
            }
            else if (colour == "Green")
            {
                trail.material = trailMats[2];
                StartCoroutine("WaitForHeal");
              
            }
            else if (colour == "Red")
            {
                trail.material = trailMats[3];
                speed = redMultiplier * startSpeed;
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "White" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Red")
            {
            other.gameObject.GetComponent<Tile>().RemoveMobFromTileList(GetComponent<MobHealth>().GetIndexOnTile());
            }
        if (closestTiles[0] != null && closestTiles[1] != null)
        {
            if (!closestTiles[0].activeSelf && !closestTiles[1].activeSelf)
            {
                MobWentThrough();
            }
        }
    }
    private void MoveToTarget()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentTarget.transform.position.x, spawnEnemies.tileHeight, currentTarget.transform.position.z), Time.deltaTime * speed);
    }

    public void SetLastSpottedOn(GameObject go)
    {
        lastTileSpottedOn = go;
    }
    private void GetNextTile()
    {
        lastTileSpottedOn = currentTarget;

        int index = 0;

        for (int i = 0; i < all.Length; i++)
        {
            float distance = Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), all[i].transform.position);                    // Get Distance
            float zDistance = gameObject.transform.position.z - all[i].transform.position.z;                    // Get Z-Distance

            if (distance <= distanceOfTwoTiles && zDistance > distanceOfTwoTiles / 6)                           // Only Get Tiles further Negative-Z than currentTile.             
            {
                closestTiles[index] = all[i];
                index++;
            }
        }

        if (closestTiles[0] != null && closestTiles[1] == null)
        { currentTarget = closestTiles[0]; }
        else if (closestTiles[1] != null && closestTiles[0] == null)
        { currentTarget = closestTiles[1]; }
        else if (!closestTiles[0].activeSelf)                                                                    // if either of the 2 current potential Targets is not Active, pick the other one as the go-to-next-target.
        { currentTarget = closestTiles[1]; }
        else if (!closestTiles[1].activeSelf)                                                                    // if either of the 2 current potential Targets is not Active, pick the other one as the go-to-next-target.
        { currentTarget = closestTiles[0]; }
        else if (closestTiles[0].activeSelf && closestTiles[1].activeSelf)
        {
            System.Random rnd = new System.Random();
            currentTarget = closestTiles[rnd.Next(2)];
        }
       
        // transform.LookAt(new Vector3(currentTarget.transform.position.x, this.transform.position.y, currentTarget.transform.position.z));
        mh.SetCanvasToCamera();
      
    }

   public void SetTarget(GameObject target)
    { currentTarget = target; }
    IEnumerator WaitForBirth()
    {
        yield return new WaitForSeconds(birthTime);
        startRotationSpeed = rotationSpeed;
        nowMove = true;
    }
    IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(.01f);
        mh.TakeDamage((mh.GetStartHP()/-10));
    }

    IEnumerator WaitForDie(bool wentThrough)
    {
        isDieing = true;
        anim.SetTrigger("Die");

        if (wentThrough)
        {
                     spawnEnemies.SpawnEnemy(spawnEnemies.GetWave(), 1);
            yield return new WaitForSeconds(dieTimeWhenThrough);
        }
        else {
            System.Random rnd = new System.Random();
            int nextAudio = rnd.Next(0,5);
            dieSounds[nextAudio].Play();
            yield return new WaitForSeconds(dieTime); }
       
        Destroy(gameObject);
    }

    public void TriggerDie()
    { StartCoroutine("WaitForDie",false); }

    void MobWentThrough()
    {
        if (tier == 0)
        {
            System.Random rnd = new System.Random();
            int nextAudio = rnd.Next(3);
            leakedAudios[nextAudio].Play();

            StartCoroutine("WaitForDie", true);
          
            
        }
        if (tier == 1)
        {
            StartCoroutine("WaitForDie", false);
            CameraController cc = FindObjectOfType<CameraController>();
            cc.GameOver();
            gameOverAudio.Play();
        }
      
       
    }

    public GameObject GetShield()
    {
        return shieldGO;
    }

    public void SetSpeed(float speed)
    {
        startSpeed = speed;
    }
}

