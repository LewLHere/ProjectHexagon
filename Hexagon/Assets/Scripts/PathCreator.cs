using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] GameObject startTile;
    [SerializeField] float[] distances;
    [SerializeField] float minDistance;
    [SerializeField] GameObject minDistanceGO;
    TileGroups tg;
    GameObject currentGO;
    

    private void Start()
    {
        distances = new float[(GetComponentsInChildren<MeshRenderer>().Length)] ;
        minDistance = 100;
        tg = FindObjectOfType<TileGroups>();
        currentGO = startTile;
        
      
       
       
    }

  
  
  
   
}
