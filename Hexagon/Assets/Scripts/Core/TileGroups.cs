using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroups : MonoBehaviour
{
    [SerializeField] List<GameObject> currentBoard;
    [SerializeField] Tile startTile;
  
    [SerializeField] List<GameObject> enemyStartingTiles;
    [SerializeField] List<GameObject> enemyFinishTiles;
    [SerializeField] int maxSize = 14;
    [SerializeField] float twoTileDistance = 5;
    [SerializeField] float z = 0;
    public List<GameObject> red, green, blue, white, all;
    int currentBoardSize;
    CameraController cameraController;
    int index = 0;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        CreateTileGroups();
        DeactivateBoard(all);
        startTile.gameObject.SetActive(true);
        IncreaseBoardSize();
    }

 
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseBoardSize();
        }
    }

    public void IncreaseBoardSize()
    {
        if (currentBoardSize <= maxSize)
        {
            cameraController.SetCameraDefault();
            currentBoardSize++;
            GetCurrentBoard();
        }
    }

    public List<GameObject> GetAll()
    { return all; }


    private void ActivateAllNeighbors()
    {
        for (int i = 0; i < all.Count; i++)
        {

            if (all[i].activeSelf && currentBoard.Contains(all[i]))
            {
                all[i].GetComponent<Tile>().ActivateAllNeighbors();
            }
        }
    }

    public List<GameObject> GetEnemySpawnTiles()
    { return enemyStartingTiles; }

    public List<GameObject> GetEnemyFinishTiles()
    { return enemyFinishTiles; }
 
    public int GetCurrentBoardSize()
    {
        return currentBoardSize;
    }
    public void CreateTileGroups()
    {
        red = new List<GameObject>(GameObject.FindGameObjectsWithTag("Red"));
        green = new List<GameObject>(GameObject.FindGameObjectsWithTag("Green"));
        blue = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blue"));
        white = new List<GameObject>(GameObject.FindGameObjectsWithTag("White"));
        all = new List<GameObject>(red.Count +
                                          green.Count +
                                          blue.Count +
                                          white.Count);
        all.AddRange(red);
        all.AddRange(green);
        all.AddRange(blue);
        all.AddRange(white);

    }

    public void GetCurrentBoard()
    {

       

        for (int i = 0; i < all.Count; i++)
        {

            if (all[i].activeSelf)
            {

                currentBoard[i] = all[i];
                
            }
            else if (!all[i].activeSelf)

            { currentBoard[i] = null; }
        }
        ActivateAllNeighbors();
        UpdateEnemyStartTiles();
        UpdateEnemyFinishLineTiles();
    }

    public List<GameObject> GetBoard(int boardSize)
    {
        index = 0;
        for (int i = 0; i < all.Count; i++)
        {
            float distance = Vector3.Distance(new Vector3(0, 0, 0), all[i].transform.position);
           
            if (distance < twoTileDistance*boardSize)
            {
             
                currentBoard[index] = all[i];
                index++;
            }
        }
        return currentBoard; 

    }
   
      public void DeactivateBoard(List<GameObject> toDeactivate)
    {
                  foreach (var tile in toDeactivate)
            {
                if (tile == null) { return; }
                tile.SetActive(false);
            }
    }
  
    public void UpdateEnemyStartTiles()
    {
        for(int i = 0; i < enemyStartingTiles.Count;i++)
        {
            enemyStartingTiles[i] = null;
        }
       
        index = 0;
        z = 0;

        // Get Furthest Positive Z-Value
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i] != null)
            {
                if (all[i].gameObject.activeSelf)
                {
                    if (all[i].transform.position.z > z)
                    {

                        z = all[i].transform.position.z;

                    }
                }
            }

        }

        // Put all tiles with Furthest Z-Value in enemyStartingTiles[]
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].gameObject.activeSelf)
            {
                if (all[i].transform.position.z == z)
                {
                    enemyStartingTiles[index] = all[i];
                    index++;
                }
            }
        }
        }

    public void UpdateEnemyFinishLineTiles()
    {
        for (int i = 0; i < enemyFinishTiles.Count; i++)
        {
            enemyFinishTiles[i] = null;
        }

        index = 0;
        z = 0;

        // Get Furthest Negative Z-Value
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i] != null)
            {
                if (all[i].gameObject.activeSelf)
                {
                    if (all[i].transform.position.z < z)
                    {

                        z = all[i].transform.position.z;

                    }
                }
            }

        }

        // Put all tiles with Furthest Z-Value in enemyStartingTiles[]
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].gameObject.activeSelf)
            {
                if (all[i].transform.position.z == z)
                {
                    enemyFinishTiles[index] = all[i];
                    index++;
                }
            }
        }
    }

}


