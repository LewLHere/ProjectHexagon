using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject[] neighboredTiles = new GameObject[6];
    [SerializeField] MobHealth[] mobsOnThisTile = null;
    [SerializeField] float minDistance;
    [SerializeField] GameObject hoveringEmpty;
    [SerializeField] GameObject hoveringHarvester;
    [SerializeField] GameObject hoveringTower;
    [SerializeField] GameObject hoveringForceField;
    [SerializeField] GameObject harvestedOnPrefab;
    Material startingMaterial;
    GameObject hoverInstance;
    int mobAmount;
    public bool isOccupied = false;
    public bool hasMobOnIt = false;
    [SerializeField] Building building;
    TileGroups tg;
    BuildManager bm;
    
   
   
    private void Awake()
    {
       
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

    private void OnMouseEnter()
    {
        if (bm.GetButtonSelected() == 0)
        {
            hoverInstance = Instantiate(hoveringEmpty, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        }
        else if (bm.GetButtonSelected() == 1)
        {
            hoverInstance = Instantiate(hoveringHarvester, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        }
        else if (bm.GetButtonSelected() == 2)
        {
            hoverInstance = Instantiate(hoveringTower, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        }
        else if (bm.GetButtonSelected() == 3)
        {
            hoverInstance = Instantiate(hoveringForceField, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        }


        if (building != null)
        {
            if (building.GetRangeGO() != null)
            {
                for(int i = 0; i < building.GetRangeGO().Length; i++)
                building.GetRangeGO()[i].GetComponent<MeshRenderer>().enabled = true;
            }
            bm.GetBuildingCost(building.gameObject, building.GetLevel() + 1);
            return;
        }
      
          
            if (bm.GetButtonSelected() == 1)
            { bm.GetBuildingCost(bm.GetHarvester().gameObject, 0); }

            if (bm.GetButtonSelected() == 2)
            { bm.GetBuildingCost(bm.GetTower().gameObject, 0); }

            if (bm.GetButtonSelected() == 3)                                                    // For more towers.
            { bm.GetBuildingCost(bm.GetForceField().gameObject, 0); }

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

                    mobsOnThisTile[i] = other.GetComponent<MobHealth>();
                    other.GetComponent<MobHealth>().SetIndexOnTile(i);
                    return;
                }
            }
  
    }
    private void OnTriggerStay(Collider other)
    {
        if (building != null)
        {
            if (building.GetComponent<TowerForceField>() != null)
            { building.GetComponent<TowerForceField>().TryForceFieldOn(); }
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
    
    private void OnMouseExit()
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
        GameObject instancedObject = Instantiate(harvestedOnPrefab, new Vector3(transform.position.x, 2f, transform.position.z),Quaternion.identity);
        instancedObject.transform.parent = gameObject.transform;
    }
}

