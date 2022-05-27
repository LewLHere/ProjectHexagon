using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestMovement : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] float baseSpeed = 3f;
    [SerializeField] float distanceTolerance = .1f;
    [SerializeField] GameObject harvesterBuilding;
    [SerializeField] int indexToHarvest = 0;
    [SerializeField] float[] waitTime;
    [SerializeField] GameObject[] sparks;
    Transform targetTransform;
    bool nextUpHarvest = true;
    bool readyToMove = true;
    float speed;
    ResourceManager rssManager;
    GameObject rssInstance;
    ManageScene ms;
    void Start()
    {
        ms = FindObjectOfType<ManageScene>();
        rssManager = FindObjectOfType<ResourceManager>();
        for (int i = 0; i < tiles.Length; i++)
        { tiles[i] = harvesterBuilding.GetComponent<Harvester>().GetNeighboredTile(i); }
    }

    void Update()
    {
        if (!readyToMove) return;
       Move();
    }

    void Move()
    {
        speed = baseSpeed*((harvesterBuilding.GetComponent<Building>().GetLevel())+1);
        if (nextUpHarvest)
        {
            if (tiles[0].activeSelf == true || tiles[1].activeSelf == true || tiles[2].activeSelf == true || tiles[3].activeSelf == true || tiles[4].activeSelf == true || tiles[5].activeSelf == true) // avoid while-infinite-loop on start-tile.
            { 
                while (tiles[indexToHarvest].activeSelf == false)                       // Making sure it's never stuck - or running into inactive ones.
                {
                    indexToHarvest++;
                    if (indexToHarvest >= 6)
                    { indexToHarvest = 0; }

                }
            }
        }

            if (nextUpHarvest == true)                                                 // Deciding if target = RSS or Building
            {
                targetTransform = tiles[indexToHarvest].transform;          
              
            }


            else if (nextUpHarvest == false)
            {
                targetTransform = harvesterBuilding.gameObject.transform;

            }


            var step = speed * Time.deltaTime;                                       // Moving until distanceTolerance reached
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetTransform.position.x, transform.position.y, targetTransform.transform.position.z), step);
            float distance = Vector3.Distance(transform.position, new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z));
        if (distance <= distanceTolerance)
        {
            StartCoroutine("WaitAtLocation");
            string colourToHarvest = tiles[indexToHarvest].tag;
            if (nextUpHarvest == true)                                                // Logic of moving back and forth - and cycling the Ressources.
            {
               
                nextUpHarvest = false;
                sparks[0].SetActive(false);
              
                if (colourToHarvest == "White")
                { sparks[1].SetActive(true); }
                
                else if (colourToHarvest == "Blue")
                { sparks[2].SetActive(true); }

                else if (colourToHarvest == "Green")
                { sparks[3].SetActive(true); }

                else if (colourToHarvest == "Red")
                { sparks[4].SetActive(true); }

            }
            else if (nextUpHarvest == false)                                         // When Returning to base update RSSMgmt.
            {

                sparks[0].SetActive(true);
                sparks[1].SetActive(false);
                sparks[2].SetActive(false);
                sparks[3].SetActive(false);
                sparks[4].SetActive(false);
             
                if (colourToHarvest == "Blue")                    
                {
                    harvesterBuilding.GetComponent<Harvester>().SetTriggerColour(colourToHarvest);
                    rssManager.UpdateRss(1, "Blue"); 
                }
                else if (colourToHarvest == "White")
                {
                    harvesterBuilding.GetComponent<Harvester>().SetTriggerColour(colourToHarvest);
                    rssManager.UpdateRss(1, "White"); }
                else if (colourToHarvest == "Green")
                {
                    harvesterBuilding.GetComponent<Harvester>().SetTriggerColour(colourToHarvest);
                    rssManager.UpdateRss(1, "Green"); }
                else if (colourToHarvest == "Red")
                {
                    harvesterBuilding.GetComponent<Harvester>().SetTriggerColour(colourToHarvest);
                    rssManager.UpdateRss(1, "Red"); }

           
                indexToHarvest++;
            if (indexToHarvest >= 6)
                { indexToHarvest = 0; }
             nextUpHarvest = true;
               
            }

        }
            }
    

    IEnumerator WaitAtLocation()
    {
        readyToMove = false;
        yield return new WaitForSeconds(waitTime[ms.GetDifficulty()]);
        readyToMove = true;
    }
    public void SetHarvester(GameObject harvester)
    {
        harvesterBuilding = harvester;
    }

 
}
