using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestMovement : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] float speed = 3f;
    [SerializeField] float distanceTolerance = .1f;
    [SerializeField] Harvester harvesterBuilding;
    [SerializeField] int indexToHarvest = 0;

    Transform targetTransform;
    bool nextUpHarvest = true;
    ResourceManager rssManager;
    int currentResource;

    void Start()
    {
        rssManager = FindObjectOfType<ResourceManager>();
        tiles = harvesterBuilding.GetClosestTiles(harvesterBuilding.gameObject);

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (nextUpHarvest)
        {
            while (tiles[indexToHarvest].activeSelf == false)                       // Making sure it's never stuck - or running into inactive ones.
            {
                indexToHarvest++;
                if (indexToHarvest >= 6)
                { indexToHarvest = 0; }

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
            if (nextUpHarvest == true)                                                // Logic of moving back and forth - and cycling the Ressources.
            {   
                
                nextUpHarvest = false;
              
            }
            else if (nextUpHarvest == false)                                         // When Returning to base update RSSMgmt.
            {
                if (tiles[indexToHarvest].tag == "Blue")                    
                { rssManager.UpdateRss(1, "Blue"); }
                else if (tiles[indexToHarvest].tag == "White")
                { rssManager.UpdateRss(1, "White"); }
                else if (tiles[indexToHarvest].tag == "Green")
                { rssManager.UpdateRss(1, "Green"); }
                else if (tiles[indexToHarvest].tag == "Red")
                { rssManager.UpdateRss(1, "Red"); }

                indexToHarvest++;
            if (indexToHarvest >= 6)
                { indexToHarvest = 0; }
             nextUpHarvest = true;
               
            }

        }
            }
    

 
    public void SetHarvester(Harvester harvester)
    {
        harvesterBuilding = harvester;
    }
}
