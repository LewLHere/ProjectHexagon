using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    [SerializeField] float fireRate = .5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float range = 10f;
    [SerializeField] MobHealth[] mobs = new MobHealth[50];

    bool readyToGetDistances = true;
    private void Start()
    {
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = GetNeighboredTiles();
        buildingIndex = 1;


    }

    void Update()
    {
        if (readyToGetDistances)
        {
            StartCoroutine("GetDistances");
        }
    }

    IEnumerator GetDistances()
    { 
        readyToGetDistances = false;
        mobs = FindObjectsOfType<MobHealth>();
        float[] distances = new float[mobs.Length];

        for (int j = 0; j < mobs.Length; j++)
        {
            distances[j] = Vector3.Distance(transform.position, mobs[j].transform.position);
            {
               
            }
        }
        if (mobs.Length > 0)
        {
            System.Random rnd = new System.Random();
            Transform target = mobs[rnd.Next(mobs.Length)].transform;
            GameObject projectileInstance = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.GetComponent<MobHealth>());               // Random Target instead of mobs[0]!!! - or the one with shortest Distance!
            projectileInstance.GetComponent<Projectile>().SetSpeed(level);
        }
        yield return new WaitForSeconds(fireRate);

        readyToGetDistances = true;
    }

}


