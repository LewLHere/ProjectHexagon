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

    [SerializeField] GameObject rssPrefab;
    Transform targetTransform;
    bool nextUpHarvest = true;
    float speed;
    ResourceManager rssManager;
    GameObject rssInstance;
    void Start()
    {
        rssManager = FindObjectOfType<ResourceManager>();
        for (int i = 0; i < tiles.Length; i++)
        { tiles[i] = harvesterBuilding.GetComponent<Harvester>().GetNeighboredTile(i); }
    }

    void Update()
    {
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
            
            if (nextUpHarvest == true)                                                // Logic of moving back and forth - and cycling the Ressources.
            {
               
                nextUpHarvest = false;

                rssPrefab.SetActive(true);
                if (tiles[indexToHarvest].tag == "White")

                { rssPrefab.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1); }
                else if (tiles[indexToHarvest].tag == "Blue")

                { rssPrefab.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1); }
                else if (tiles[indexToHarvest].tag == "Green")

                { rssPrefab.GetComponent<MeshRenderer>().material.color = new Color(0,1,0); }
                else if (tiles[indexToHarvest].tag == "Red")
                { rssPrefab.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0); }

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

                rssPrefab.SetActive(false);
                indexToHarvest++;
            if (indexToHarvest >= 6)
                { indexToHarvest = 0; }
             nextUpHarvest = true;
               
            }

        }
            }
    

    public void SetHarvester(GameObject harvester)
    {
        harvesterBuilding = harvester;
    }

 
}
