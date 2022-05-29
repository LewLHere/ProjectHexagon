using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    float[] distances;
   
   
    [SerializeField] GameObject harvesterPrefab;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] GameObject forceFieldPrefab;
    [SerializeField] GameObject pulsePrefab;
    [SerializeField] float yCorrect = 4.5f;
    [SerializeField] GameObject costTextGO;
    [SerializeField] TextMeshProUGUI textCost, textWhite, textBlue, textGreen, textRed;
    [SerializeField] AudioSource levelUpAudio;
    [SerializeField] int selectedButton = 0;
    [SerializeField] public TextMeshProUGUI[] upgradeCostText;
    Transform toBuild;
    int level = 0;
    int costWhite, costGreen, costBlue, costRed;
    int buildingIndex;
    ResourceManager rss;
    Building buildingToBuild;
    Tile tileToBuild;
    GameObject tileInstance;

    void Start()
    {
        rss = FindObjectOfType<ResourceManager>();
    }

 /*   private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                toBuild = hit.transform;
              
                tileToBuild = toBuild.gameObject.GetComponent<Tile>();
                TryBuildBuilding(selectedButton);
            }
        }
    }

    */

    public void SetUpdateCostText(string txt)
    { textCost.text = txt; }
    public void DeleteLastMarkedTile()
    { Destroy(tileInstance); }

    public void SaveHoverTile(GameObject hoverInstance)
    { tileInstance = hoverInstance; }
    public void SetTileToBuild(Tile tile)
    { tileToBuild = tile;}
    public void Buildlvl1(int buttonNumber)
    {
        if (buttonNumber == 1)
        { selectedButton = 1;
         
            BuildBuilding(harvesterPrefab);


        }
        if (buttonNumber == 2)
        { selectedButton = 2;

            BuildBuilding(towerPrefab);
        }
        if (buttonNumber == 3)
        {
            selectedButton = 3;

            BuildBuilding(forceFieldPrefab);
        }
        if (buttonNumber == 4)
        {
            selectedButton = 4;

            BuildBuilding(pulsePrefab);
        }
    }

    public void BuildBuilding(GameObject buildGO)
    {
        Debug.Log(tileToBuild);
         if(tileToBuild != null)
       
        {
          
            if (tileToBuild.GetBuilding() == null && tileToBuild.isOccupied == false)
            {
                if (rss.GetWhite() < buildGO.GetComponent<Building>().GetCostWhite(0)) return;
                if (rss.GetGreen() < buildGO.GetComponent<Building>().GetCostGreen(0)) return;
                if (rss.GetBlue() < buildGO.GetComponent<Building>().GetCostBlue(0)) return;
                if (rss.GetRed() < buildGO.GetComponent<Building>().GetCostRed(0)) return;

                rss.UpdateRss(-buildGO.GetComponent<Building>().GetCostWhite(0), "White");
                rss.UpdateRss(-buildGO.GetComponent<Building>().GetCostGreen(0), "Green");
                rss.UpdateRss(-buildGO.GetComponent<Building>().GetCostBlue(0), "Blue");
                rss.UpdateRss(-buildGO.GetComponent<Building>().GetCostRed(0), "Red");

                tileToBuild.isOccupied = true;
                GameObject buildingInstance = Instantiate(buildGO, new Vector3(tileToBuild.transform.position.x, yCorrect, tileToBuild.transform.position.z), Quaternion.identity); ;
                tileToBuild.SetBuilding(buildingInstance.GetComponent<Building>());
                buildingInstance.GetComponent<Building>().SetTile(tileToBuild);
                tileToBuild.GetBuilding().BuildFirstLevel();
                levelUpAudio.Play();

            }
            else if (tileToBuild.GetBuilding() != null)
            {

                if (tileToBuild.GetBuilding().GetComponent<Building>() != null)
                {


                    if (tileToBuild.GetBuilding().GetLevel() < tileToBuild.GetBuilding().maxLevel)
                    {
                        Debug.Log(tileToBuild);
                        Debug.Log(tileToBuild.GetBuilding().GetComponent<Building>());
                        Debug.Log(tileToBuild.GetBuilding().GetLevel());
                        Debug.Log(tileToBuild.GetBuilding().GetComponent<Building>().GetCostWhite(tileToBuild.GetBuilding().GetLevel()));

                        if (rss.GetWhite() < tileToBuild.GetBuilding().GetComponent<Building>().GetCostWhite(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                        if (rss.GetGreen() < tileToBuild.GetBuilding().GetComponent<Building>().GetCostGreen(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                        if (rss.GetBlue() < tileToBuild.GetBuilding().GetComponent<Building>().GetCostBlue(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                        if (rss.GetRed() < tileToBuild.GetBuilding().GetComponent<Building>().GetCostRed(tileToBuild.GetBuilding().GetLevel() + 1)) return;

                        rss.UpdateRss(-tileToBuild.GetBuilding().GetComponent<Building>().GetCostWhite(tileToBuild.GetBuilding().GetLevel() + 1), "White");
                        rss.UpdateRss(-tileToBuild.GetBuilding().GetComponent<Building>().GetCostGreen(tileToBuild.GetBuilding().GetLevel() + 1), "Green");
                        rss.UpdateRss(-tileToBuild.GetBuilding().GetComponent<Building>().GetCostBlue(tileToBuild.GetBuilding().GetLevel() + 1), "Blue");
                        rss.UpdateRss(-tileToBuild.GetBuilding().GetComponent<Building>().GetCostRed(tileToBuild.GetBuilding().GetLevel() + 1), "Red");

                        tileToBuild.GetBuilding().IncreaseBuildingLevel();
                        levelUpAudio.Play();
                        GetBuildingCost(tileToBuild.GetBuilding().gameObject, tileToBuild.GetBuilding().GetLevel());
                    }
                }
            }

        }
      

        
     
    }

  
    public void GetBuildingCost(GameObject building, int currentBuildingLevel)
    {
        if (currentBuildingLevel <= building.GetComponent<Building>().maxLevel)
        {
            costWhite = building.GetComponent<Building>().GetCostWhite(currentBuildingLevel);
            costBlue = building.GetComponent<Building>().GetCostBlue(currentBuildingLevel);
            costGreen = building.GetComponent<Building>().GetCostGreen(currentBuildingLevel);
            costRed = building.GetComponent<Building>().GetCostRed(currentBuildingLevel);

            if(building.GetComponent<Harvester>() != null)
            {
                textCost.text = "Harvester Level " + (currentBuildingLevel + 1);
            }
            if (building.GetComponent<TowerPulse>() != null)
            {
                textCost.text = "Impulse Level " + (currentBuildingLevel+ 1);
            }
            if (building.GetComponent<Tower>() != null)
            {
                textCost.text = "Tower Level " + (currentBuildingLevel + 1);
            }
            if (building.GetComponent<TowerForceField>() != null)
            {
                textCost.text = "Forcefield Level " + (currentBuildingLevel + 1);
            }
            
           
            textWhite.text = "" + costWhite;
            textBlue.text = "" + costBlue;
            textGreen.text = "" + costGreen;
            textRed.text = "" + costRed;
        }
        else
        {
            if (building.GetComponent<Harvester>() != null)
            {
                textCost.text = "Harvester Max";
            }
            if (building.GetComponent<TowerPulse>() != null)
            {
                textCost.text = "Impulse Max";
            }
            if (building.GetComponent<Tower>() != null)
            {
                textCost.text = "Tower Max";
            }
            if (building.GetComponent<TowerForceField>() != null)
            {
                textCost.text = "Forcefield Max";
            }
            textWhite.text = "";
            textBlue.text = "";
            textGreen.text = "";
            textRed.text = "";
        }

    }

    public int GetButtonSelected()
    { return selectedButton; }

    public void SetButtonSelected(int buttonNumber)
    { selectedButton = buttonNumber; }

    public Building GetHarvester()
    { return harvesterPrefab.GetComponent<Building>(); }

    public Building GetTower()
    { return towerPrefab.GetComponent<Building>(); }

    public Building GetForceField()
    { return forceFieldPrefab.GetComponent<Building>(); }

    public Building GetPulse()
    { return pulsePrefab.GetComponent<Building>(); }
}
