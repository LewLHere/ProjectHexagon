using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    float[] distances;
   
   
    [SerializeField] GameObject harvesterPrefab;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] float yCorrect = 3.5f;

    int selectedButton = 0;
    Transform toBuild;
    
    void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                toBuild = hit.transform;
                TryBuildBuilding(selectedButton);
            }
        }
    }

    public void SelectBuildingButton(int buttonNumber)
    {
        if (buttonNumber == 1)
        { selectedButton = 1; }
        if (buttonNumber == 2)
        { selectedButton = 2; }
    }
    private void TryBuildBuilding(int selectedButton)
    {
        if (toBuild.gameObject.GetComponent<Tile>() == null) { return; }
        if (selectedButton == 0)
        { return; }
        if (selectedButton == 1)
        { BuildResourceHarvester(); }
        if (selectedButton == 2)
        { BuildTower(); }
    }

    private void BuildTower()
    {
      
        if (toBuild.gameObject.GetComponent<Tile>().isOccupied)                        // If anything on it.
        {
            return;
        }
        else
        {
            toBuild.gameObject.GetComponent<Tile>().isOccupied = true;
            GameObject towerInstance = Instantiate(towerPrefab, new Vector3(toBuild.position.x, yCorrect, toBuild.position.z), Quaternion.identity);
           toBuild.gameObject.GetComponent<Tile>().tower = towerInstance.GetComponent<Tower>();
           towerInstance.GetComponent<Tower>().tile = toBuild.gameObject;


        }
    }

    void BuildResourceHarvester()
    {
      
        if (toBuild.gameObject.GetComponent<Tile>().harvester != null)               // If Harvester is on it. 
        {
           //Upgrade!
        }
        else if (!toBuild.gameObject.GetComponent<Tile>().isOccupied)               // If nothing at all on it.
        {
            toBuild.gameObject.GetComponent<Tile>().isOccupied = true;
            GameObject harvesterInstance = Instantiate(harvesterPrefab, new Vector3(toBuild.position.x, yCorrect, toBuild.position.z), Quaternion.identity);
            
           toBuild.gameObject.GetComponent<Tile>().harvester = harvesterInstance.GetComponent<Harvester>();
            harvesterInstance.GetComponent<Harvester>().SetTile(toBuild.GetComponent<Tile>());

           
        }
    }
}
