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
    [SerializeField] float timeAtTile = .5f;
    [SerializeField] GameObject shieldGO;
    [SerializeField] int livesToLose = 1;
    SpawnEnemies spawnEnemies;

    public string colour;
    MobHealth mh;
    GameObject[] all;
    PlayerLives playerLives;
    [SerializeField] GameObject lastTileSpottedOn;
    TileGroups tg = null;

    private void Awake()
    {
        playerLives = FindObjectOfType<PlayerLives>();
        mh = GetComponent<MobHealth>();
        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        tg = FindObjectOfType<TileGroups>();

    }
    private void Start()
    {

        Debug.Log(mh);
       
      
        all = tg.GetAll().ToArray();
        GetNextTile();

    }

    private void Update()
    {

        distanceToNextTarget = Vector3.Distance(new Vector3(transform.position.x, spawnEnemies.tileHeight, transform.position.z), new Vector3(currentTarget.transform.position.x, spawnEnemies.tileHeight, currentTarget.transform.position.z));
        if (distanceToNextTarget > distanceOfTwoTiles / 30)
        {
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
                speed = startSpeed;
            }
            else if (colour == "Blue")
            {
                shieldGO.SetActive(true);
            }
            else if (colour == "Green")
            {
                StartCoroutine("WaitForHeal");
              
            }
            else if (colour == "Red")
            {
                speed = 1.2f * startSpeed;
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "White" || other.gameObject.tag == "Blue" || other.gameObject.tag == "Green" || other.gameObject.tag == "Red")
            {
            other.gameObject.GetComponent<Tile>().RemoveMobFromTileList(GetComponent<MobHealth>().GetIndexOnTile());
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
        if (!closestTiles[0].activeSelf)                                                                    // if either of the 2 current potential Targets is not Active, pick the other one as the go-to-next-target.
        { currentTarget = closestTiles[1]; }
        else if (!closestTiles[1].activeSelf)
        { currentTarget = closestTiles[0]; }
        else if (closestTiles[0].activeSelf && closestTiles[1].activeSelf)
        {
            System.Random rnd = new System.Random();
            currentTarget = closestTiles[rnd.Next(2)];
        }
        if (!closestTiles[0].activeSelf && !closestTiles[1].activeSelf)
        {
            MobWentThrough();
        }
      
    }

    IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(.01f);
        mh.TakeDamage((mh.GetStartHP()/-10));
    }
    void MobWentThrough()
    {
        playerLives.LoseLife(livesToLose);
        Destroy(gameObject);
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

