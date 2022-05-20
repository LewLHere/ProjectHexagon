using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] bool cameraIsDefault = true;
    TileGroups tg;
   
    Transform tileToCenter;

    void Start()
    { 
        
        tg = FindObjectOfType<TileGroups>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {  

            if (cameraIsDefault)
            {
               
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    cameraIsDefault = false;
                    tileToCenter = hit.transform;
                    if (tileToCenter.GetComponent<Tile>())
                    { ZoomIn(tileToCenter); }


                }
            }
            else if (!cameraIsDefault)
            { cameraIsDefault = true; }
           
        }
    }

    void LateUpdate()
    {
        if(cameraIsDefault)
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 15+tg.GetCurrentBoardSize()*8, mainCamera.transform.position.z);
    }

    public void SetCameraDefault()
    {
        cameraIsDefault = true;
    }
    void ZoomIn(Transform transform)
    {
        mainCamera.transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
    }
}
