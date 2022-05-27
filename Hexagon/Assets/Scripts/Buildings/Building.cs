using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected GameObject[] prefabsYellow = new GameObject[5];
    [SerializeField] protected GameObject[] prefabsBlue = new GameObject[5];
    [SerializeField] protected GameObject[] prefabsGreen = new GameObject[5];
    [SerializeField] protected GameObject[] prefabsRed = new GameObject[5];
    [SerializeField] protected GameObject tileMarkerYellow;
    [SerializeField] protected GameObject tileMarkerBlue;
    [SerializeField] protected GameObject tileMarkerGreen;
    [SerializeField] protected GameObject tileMarkerRed;

    [SerializeField] protected GameObject[] neighboredTiles = new GameObject[6];
   [SerializeField] protected float minDistance = 6;
   [SerializeField] protected Tile tile;
    [SerializeField] public int[] costWhite = new int[5];
   [SerializeField] public int[] costBlue= new int[5];
   [SerializeField] public int[] costGreen= new int[5];
   [SerializeField] public int[] costRed= new int[5];
    [SerializeField] protected int level = 0;
   [SerializeField] protected int buildingIndex;
   [SerializeField] protected GameObject[] rangeGO;
    [SerializeField] float yComponent;
    protected Animator anim;
    protected TileGroups tg;
    protected BuildManager bm;
    public int maxLevel = 4;
    [SerializeField] protected GameObject currentPrefab;
    // No Start() in here or it overrides all Sub-Classes.


    public float GetyComponent()
    { return yComponent; }
    public Tile GetTile()
    { return tile;
    }

    public void SetTile(Tile setTile)
    {
        tile = setTile;
    }
    public GameObject GetNeighboredTile(int number)
    {
        return neighboredTiles[number];
    }

    public int GetCostWhite(int level)
    {
        return costWhite[level];
    }
    public int GetCostBlue(int level)
    {
        return costBlue[level];
    }
    public int GetCostGreen(int level)
    {
        return costGreen[level];
    }
    public int GetCostRed(int level)
    {
        return costRed[level];
    }
    public int GetLevel()
    {
        return level;
    }
    public void IncreaseBuildingLevel()
    {

        
        if (level < maxLevel)
        {
         
            level++;
            currentPrefab.SetActive(false);
            if (tile.tag == "White")
            {
                currentPrefab = Instantiate(prefabsYellow[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);

            }
            else if (tile.tag == "Blue")
            {
                currentPrefab = Instantiate(prefabsBlue[level], new Vector3(transform.position.x,yComponent, transform.position.z), gameObject.transform.rotation);
            }
            else if (tile.tag == "Green")
            {
                currentPrefab = Instantiate(prefabsGreen[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);
            }
            else if (tile.tag == "Red")
            {
                currentPrefab = Instantiate(prefabsRed[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);
            }
            anim = currentPrefab.GetComponent<Animator>();



        }
        else Debug.Log("Has Max Level");
        
    }

    public void BuildFirstLevel()
    {
       
        if (tile.tag == "White")
        {
           // Instantiate(tileMarkerYellow, new Vector3(transform.position.x, transform.position.y -2, transform.position.z), gameObject.transform.rotation);
            currentPrefab = Instantiate(prefabsYellow[level], new Vector3(transform.position.x,yComponent,transform.position.z), gameObject.transform.rotation);
        }
        else if (tile.tag == "Blue")
        {
            //Instantiate(tileMarkerBlue, new Vector3(transform.position.x, transform.position.y -2, transform.position.z), Quaternion.identity);
            currentPrefab = Instantiate(prefabsBlue[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);
        }
        else if (tile.tag == "Green")
        {
            //Instantiate(tileMarkerGreen, new Vector3(transform.position.x, transform.position.y -2, transform.position.z), Quaternion.identity);
            currentPrefab = Instantiate(prefabsGreen[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);
        }
        else if (tile.tag == "Red")
        {
           // Instantiate(tileMarkerRed, new Vector3(transform.position.x, transform.position.y -2, transform.position.z), Quaternion.identity);
            currentPrefab = Instantiate(prefabsRed[level], new Vector3(transform.position.x, yComponent, transform.position.z), gameObject.transform.rotation);
        }
      
        anim = currentPrefab.GetComponent<Animator>();

    }

    public GameObject[] GetRangeGO()
    { return rangeGO; }

    public int GetBuildingIndex()
    { return buildingIndex; }
}
