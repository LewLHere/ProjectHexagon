using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject[] neighboredTiles = new GameObject[6];
    [SerializeField] MobHealth[] mobsOnThisTile = null;
    [SerializeField] float minDistance;
    [SerializeField] GameObject hoveringEmpty;
    [SerializeField] GameObject hoveringHarvester;
    [SerializeField] GameObject buildingButtons;
    [SerializeField] GameObject upgradeButton;
    [SerializeField] GameObject harvestedOnPrefab;
   
    Material startingMaterial;
    GameObject hoverInstance;
    int mobAmount;
    public bool isOccupied = false;
    public bool hasMobOnIt = false;
    [SerializeField] Building building;
    TileGroups tg;
    BuildManager bm;
    ResourceManager rm; 
   
   
    private void Awake()
    {
        rm = FindObjectOfType<ResourceManager>();
        minDistance = 6;
        tg = FindObjectOfType<TileGroups>();
        bm = FindObjectOfType<BuildManager>();


        Material mat = GetComponent<MeshRenderer>().material;

       

        if (mat.name == null) { return; }

        if (mat.name.Contains("Red"))
        {
            tag = "Red";
        }
        else if (mat.name.Contains("Green"))
        {
            tag = "Green";
        }
        else if (mat.name.Contains("Blue"))
        {
            tag = "Blue";
        }
        else if (mat.name.Contains("White"))
        {
            tag = "White";
        }

        tg.CreateTileGroups();
      
    }

    private void Start()
    {
        GetNeighboredTiles(gameObject);
       
    }

   

    private void OnMouseDown()
    {
        bm.DeleteLastMarkedTile();
        hoverInstance = Instantiate(hoveringEmpty, new Vector3(transform.position.x, 2.5f, transform.position.z), Quaternion.identity);
        bm.SaveHoverTile(hoverInstance);
        bm.SetTileToBuild(this);

        if (building == null)
        {
            ActivateBuildingButtons();

            
            return;
        }
        if (building != null && building.GetLevel() < building.maxLevel)
        {
            
            bm.upgradeCostText[0].text = "" + building.GetCostWhite(building.GetLevel()+1);
            bm.upgradeCostText[1].text = "" + building.GetCostGreen(building.GetLevel()+1);
            bm.upgradeCostText[2].text = "" + building.GetCostRed(building.GetLevel()+1);
            bm.upgradeCostText[3].text = "" + building.GetCostBlue(building.GetLevel()+1);
            bm.SetUpdateCostText("Upgrade to Lvl" + (building.GetLevel() + 2));

            ActivateUpgradeButton();
        }
        if (building != null && building.GetLevel() >= building.maxLevel)
        {
           
            bm.upgradeCostText[0].text = "/" + building.GetCostWhite(building.GetLevel() + 1);
            bm.upgradeCostText[1].text = "/" + building.GetCostGreen(building.GetLevel() + 1);
            bm.upgradeCostText[2].text = "/" + building.GetCostRed(building.GetLevel() + 1);
            bm.upgradeCostText[3].text = "/" + building.GetCostBlue(building.GetLevel() + 1);
            bm.SetUpdateCostText("Building Max Lvl");

            ActivateUpgradeButton();
        }

    }

    private void ActivateUpgradeButton()
    {
        if ((rm.GetBlue()) < building.costBlue[building.GetLevel()+1])
        { bm.upgradeCostText[3].color = new Color(1, 0, 0); }
        else bm.upgradeCostText[3].color = new Color(1, 1, 1);

        if ((rm.GetWhite()) < building.costWhite[building.GetLevel() + 1])
        { bm.upgradeCostText[0].color = new Color(1, 0, 0); }
        else bm.upgradeCostText[0].color = new Color(1, 1, 1);

        if ((rm.GetGreen()) < building.costGreen[building.GetLevel() + 1])
        { bm.upgradeCostText[1].color = new Color(1, 0, 0); }
        else bm.upgradeCostText[1].color = new Color(1, 1, 1);

        if ((rm.GetRed()) < building.costRed[building.GetLevel() + 1])
        { bm.upgradeCostText[2].color = new Color(1, 0, 0); }
        else bm.upgradeCostText[2].color = new Color(1, 1, 1);

        if (building != null)
        {
          
        }


        upgradeButton.SetActive(true);
        Button ub = upgradeButton.GetComponent<Button>();
        buildingButtons.SetActive(false);
    }

    private void ActivateBuildingButtons()
    {
        buildingButtons.SetActive(true);
        upgradeButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Mob")
        {
            mobAmount++;
            hasMobOnIt = true;
            for (int i = 0; i < mobsOnThisTile.Length; i++)
            {
                    if(mobsOnThisTile[i] != null) { continue; }

                    mobsOnThisTile[i] = other.GetComponentInParent<MobHealth>();
                    other.GetComponent<MobHealth>().SetIndexOnTile(i);
                    return;
                }
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mob")
        {
            if (building != null)
            {
                if (building.GetComponent<TowerForceField>() != null)
                {
                    building.GetComponent<TowerForceField>().TryForceFieldOn();
                }
            }

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
         }
    

    public void RemoveMobFromTileList(int index)
{
    mobsOnThisTile[index] = null;
    if (mobsOnThisTile[0] = null)
    { hasMobOnIt = false; }

}
    
 /*   private void OnMouseExit()
    {
        Destroy(hoverInstance);
        GetComponent<MeshRenderer>().materials[0] = startingMaterial;
        if (building != null)
        {
            if (building.GetRangeGO() != null)
            {
                for (int i = 0; i < building.GetRangeGO().Length; i++)
                    building.GetRangeGO()[i].GetComponent<MeshRenderer>().enabled=false;

            }
        }
    }
 */

    public MobHealth[] GetMobsOnTile()
    {
        return mobsOnThisTile;
    }
    public float GetMinDistance()
            { 
        return minDistance; 
    }
    
    public GameObject[] GetNeighboredTiles(GameObject tile)
    {

        int neighboredTilesIndex = 0;

        for (int i = 0; i < tg.GetAll().Count; i++)
        {
           
            float distance = Vector3.Distance(tile.transform.position, tg.GetAll()[i].transform.position);
          
            if (distance < minDistance && distance != 0)
            {
                neighboredTiles[neighboredTilesIndex] = tg.GetAll()[i];
                neighboredTilesIndex++;
            }
        }
        return neighboredTiles;
    }
  

    public void ActivateAllNeighbors()
    {
        GetNeighboredTiles(gameObject);
        neighboredTiles[0].SetActive(true);
        neighboredTiles[1].SetActive(true);
        neighboredTiles[2].SetActive(true);
        neighboredTiles[3].SetActive(true);
        neighboredTiles[4].SetActive(true);
        neighboredTiles[5].SetActive(true);
    }

    public void SetBuilding(Building instanceBuilding)
    {
        building = instanceBuilding;
    }
    public Building GetBuilding()
    {
        
        return building;
    }
  
    public void SetOccupied()
    {
        isOccupied = true;
    }

    public void OccupiedByHarvester()
    { 
        GameObject instancedObject = Instantiate(harvestedOnPrefab, new Vector3(transform.position.x, 1.85f, transform.position.z),Quaternion.identity);
        instancedObject.transform.parent = gameObject.transform;
    }
}

