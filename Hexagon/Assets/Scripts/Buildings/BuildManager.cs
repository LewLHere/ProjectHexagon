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
    [SerializeField] float yCorrect = 3.5f;
    [SerializeField] GameObject costTextGO;
    [SerializeField] TextMeshProUGUI textCost, textWhite, textBlue, textGreen, textRed;

    [SerializeField] int selectedButton = 0;
    Transform toBuild;
    int level = 0;
    int costWhite, costGreen, costBlue, costRed;
    int buildingIndex;
    ResourceManager rss;
    Building buildingToBuild;
    Tile tileToBuild;

    void Start()
    {
        rss = FindObjectOfType<ResourceManager>();
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
              
                tileToBuild = toBuild.gameObject.GetComponent<Tile>();
                TryBuildBuilding(selectedButton);
            }
        }
    }


    public void SelectBuildingButton(int buttonNumber)
    {
        if (buttonNumber == 1)
        { selectedButton = 1;
            costTextGO.SetActive(true);
            GetBuildingCost(harvesterPrefab, 0);

        }
        if (buttonNumber == 2)
        { selectedButton = 2;
            costTextGO.SetActive(true);
            GetBuildingCost(towerPrefab, 0);
        }
        if (buttonNumber == 3)
        {
            selectedButton = 3;
            costTextGO.SetActive(true);
            GetBuildingCost(forceFieldPrefab, 0);
        }
    }

    private void TryBuildBuilding(int buildingNumber)
    {   // if (tileToBuild.hasMobOnIt) return;
        if (tileToBuild.isOccupied == true && tileToBuild.GetBuilding() == null) return;
        if (toBuild.gameObject.GetComponent<Tile>() == null) { return; }

       
        if (selectedButton == 0)
        { return; }
        if (selectedButton == 1)
        {
            buildingIndex = 0;
            buildingToBuild = harvesterPrefab.GetComponent<Building>();
            BuildBuilding(harvesterPrefab);
        }
        if (selectedButton == 2)
        {
            buildingIndex = 1;
            buildingToBuild = towerPrefab.GetComponent<Building>();
            BuildBuilding(towerPrefab);
        }

        if (selectedButton == 3)
        {
            buildingIndex = 2;
            buildingToBuild = forceFieldPrefab.GetComponent<Building>();
            BuildBuilding(forceFieldPrefab);
        }
    }

    private void BuildBuilding(GameObject buildGO)
    {

      
        if (tileToBuild.GetBuilding() == null)
        {
            if (rss.GetWhite() < buildingToBuild.GetCostWhite(0)) return;
            if (rss.GetGreen() < buildingToBuild.GetCostGreen(0)) return;
            if (rss.GetBlue() < buildingToBuild.GetCostBlue(0)) return;
            if (rss.GetRed() < buildingToBuild.GetCostRed(0)) return;

            rss.UpdateRss(-buildingToBuild.GetCostWhite(0), "White");
            rss.UpdateRss(-buildingToBuild.GetCostGreen(0), "Green");
            rss.UpdateRss(-buildingToBuild.GetCostBlue(0), "Blue");
            rss.UpdateRss(-buildingToBuild.GetCostRed(0), "Red");

            tileToBuild.isOccupied = true;
            GameObject buildingInstance = Instantiate(buildGO, new Vector3(toBuild.position.x, yCorrect, toBuild.position.z), Quaternion.identity);
            tileToBuild.SetBuilding(buildingInstance.GetComponent<Building>());
            buildingInstance.GetComponent<Building>().SetTile(tileToBuild);
        }
     
        else if (tileToBuild.GetBuilding().GetComponent<Building>().GetBuildingIndex() == buildingIndex)
        {
           
            if (tileToBuild.GetBuilding().GetLevel() < tileToBuild.GetBuilding().maxLevel)
            {
                if (rss.GetWhite() < buildingToBuild.GetCostWhite(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                if (rss.GetGreen() < buildingToBuild.GetCostGreen(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                if (rss.GetBlue() < buildingToBuild.GetCostBlue(tileToBuild.GetBuilding().GetLevel() + 1)) return;
                if (rss.GetRed() < buildingToBuild.GetCostRed(tileToBuild.GetBuilding().GetLevel() + 1)) return;

                rss.UpdateRss(-buildingToBuild.GetCostWhite(tileToBuild.GetBuilding().GetLevel() + 1), "White");
                rss.UpdateRss(-buildingToBuild.GetCostGreen(tileToBuild.GetBuilding().GetLevel() + 1), "Green");
                rss.UpdateRss(-buildingToBuild.GetCostBlue(tileToBuild.GetBuilding().GetLevel() + 1), "Blue");
                rss.UpdateRss(-buildingToBuild.GetCostRed(tileToBuild.GetBuilding().GetLevel() + 1), "Red");

                tileToBuild.GetBuilding().IncreaseBuildingLevel();
            }
        }

        
     
    }

    public void GetBuildingCost(GameObject building, int currenBuildingLevel)
    {
        if (currenBuildingLevel <= building.GetComponent<Building>().maxLevel)
        {
            costWhite = building.GetComponent<Building>().GetCostWhite(currenBuildingLevel);
            costBlue = building.GetComponent<Building>().GetCostBlue(currenBuildingLevel);
            costGreen = building.GetComponent<Building>().GetCostGreen(currenBuildingLevel);
            costRed = building.GetComponent<Building>().GetCostRed(currenBuildingLevel);

            textCost.text = building.GetComponent<Building>().name;
            textWhite.text = "" + costWhite;
            textBlue.text = "" + costBlue;
            textGreen.text = "" + costGreen;
            textRed.text = "" + costRed;
        }
        else
        {
            textCost.text = building.GetComponent<Building>().name;
            textWhite.text = "MAX";
            textBlue.text = "MAX";
            textGreen.text = "MAX";
            textRed.text = "MAX";
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
}
