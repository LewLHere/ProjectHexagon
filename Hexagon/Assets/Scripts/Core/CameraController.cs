using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] bool cameraIsDefault = true;
    [SerializeField] float currentDefaultSize;
    [SerializeField] Light lightGO;
    [SerializeField] int[] lightTransformY;
    [SerializeField] float[] lightIntensity;
    [SerializeField] int[] lightRange;
    [SerializeField] GameObject zeroTile;
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject gameOverGO;
    ManageScene sceneM;
    
    int boardSize = 0;
    TileGroups tg;
    Vector3 startPosition;
    float lastCameraSize;
    bool isPaused = false;
    Transform tileToCenter;
    int lightChangeCounter = 0;
    void Start()
    {
        sceneM = FindObjectOfType<ManageScene>();
        startPosition = mainCamera.transform.position;  
        tg = FindObjectOfType<TileGroups>();
        lightGO.transform.position = new Vector3(0, lightTransformY[boardSize], 0);
        lightGO.range = lightRange[boardSize];
        lightGO.intensity = lightIntensity[boardSize];

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {  

            if (cameraIsDefault)
            {
                lastCameraSize = mainCamera.orthographicSize;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    tileToCenter = hit.transform;
                    if (tileToCenter.GetComponent<Tile>())
                    { ZoomIn(tileToCenter); }


                }
            }
            else if (!cameraIsDefault && tileToCenter.GetComponent<Tile>())
            { 
                cameraIsDefault = true;
                mainCamera.gameObject.transform.position = new Vector3(0, 102, -100);
                mainCamera.orthographicSize = lastCameraSize;
            }
           
        }
    }


  public void GameOver()
    {
        gameOverGO.SetActive(true);
                    Time.timeScale = 0;

        }
    public void PauseGame()
    {
      
        if (!isPaused)
        {
         
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
          
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void LoadScene(int sceneToLoad)
    {
        sceneM.LoadBoard(sceneToLoad);
    }
    public void SetCameraDefault()
    {
        cameraIsDefault = true;
    }
    void ZoomIn(Transform transform)
    {

        mainCamera.gameObject.transform.position -= zeroTile.transform.position - transform.position;
        mainCamera.orthographicSize = 10;
        cameraIsDefault = false;
       
    }

    
    public void SetDefaultSize(int currentBoardSize)
    {
        boardSize = currentBoardSize;
        currentDefaultSize = 8 + (currentBoardSize-1) * 3;
        StartCoroutine("MoveCameraOut");
         
    }

    public void StartMusic()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
    }
    IEnumerator MoveCameraOut()
    {

        lightGO.transform.position = new Vector3(0, lightTransformY[boardSize], 0);
        lightGO.range = lightRange[boardSize];
        lightGO.intensity = lightIntensity[boardSize];
            while (mainCamera.orthographicSize < currentDefaultSize)
            {
                mainCamera.orthographicSize += .05f;
                yield return new WaitForSeconds(.01f);
            }
        }
    }

