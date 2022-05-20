using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    TileGroups tg;
    public Transform target;
    [SerializeField] int colourToWalkID = 0;
    List<GameObject> colourToWalk = null;
    [SerializeField] int indexToWalk = 0;
    [SerializeField] float distanceTolerance = .5f;
    [SerializeField] float distance;



    private void Awake()
    
    {
        tg = FindObjectOfType<TileGroups>();
        if (colourToWalkID == 0)
        { colourToWalk = tg.red; }

        if (colourToWalkID == 1)
        { colourToWalk = tg.green; }

        if (colourToWalkID == 2)
        { colourToWalk = tg.blue; }

        if (colourToWalkID == 3)
        { colourToWalk = tg.white; }


    }

    
   
        void Update()
        {


        MoveToTile();
       

    }

    void MoveToTile()
    {
        if (indexToWalk >= colourToWalk.Count) { return; }
        target = colourToWalk[indexToWalk].transform;
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z), step);
        distance = Vector3.Distance(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z));
        if (distance <= distanceTolerance)
        {
           
            indexToWalk++;
        }

    }
}
