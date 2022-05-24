using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] bool cameraIsDefault = true;
    [SerializeField] float currentDefaultSize;
    [SerializeField] GameObject lightGroup;
    [SerializeField] Light[] light;
    [SerializeField] int[] lightDistance;
    [SerializeField] float[] lightIntensity;
    [SerializeField] int[] lightRange;
    [SerializeField] int[] lightSpotAngle;
    [SerializeField] GameObject zeroTile;
    int boardSize;
    TileGroups tg;
    Vector3 startPosition;
    float lastCameraSize;
   
    Transform tileToCenter;

    void Start()
    {
        startPosition = mainCamera.transform.position;  
        tg = FindObjectOfType<TileGroups>();

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
        currentDefaultSize = 8 + (currentBoardSize-1) * 5;
        StartCoroutine("MoveCameraOut");
         
    }

    IEnumerator MoveCameraOut()
    {
        lightGroup.transform.position = new Vector3(0, lightDistance[boardSize], 0);
        for (int i = 0; i < light.Length; i++)
        {
            light[i].range = lightRange[boardSize];
            light[i].intensity = lightIntensity[boardSize];
            light[i].spotAngle = lightSpotAngle[boardSize];
                }
        while (mainCamera.orthographicSize < currentDefaultSize)
        {
            mainCamera.orthographicSize += .05f;
            yield return new WaitForSeconds(.01f);
        }
    }

    
}
