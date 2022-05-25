using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerForceField : Building
{
    [SerializeField] float fireRate = .7f;
    [SerializeField] GameObject forceFieldPrefab = null;
    [SerializeField] int[] damage;
    bool isReadyForForceField = true;
    

    void Start()
    {
        buildingIndex = 2;
    }


    private void Update()
    {
      
    }


    public void TryForceFieldOn()
    {
        if (isReadyForForceField)
        {
            StartCoroutine("ForceFieldOn");
            
        }
    }

    IEnumerator ForceFieldOn()
    {
        forceFieldPrefab.SetActive(true);
        isReadyForForceField = false;

        for (int i = 0; i < tile.GetMobsOnTile().Length; i++)
        {
            if (tile.GetMobsOnTile()[i] != null)
            {
              tile.GetMobsOnTile()[i].TakeDamage(damage[level]);
            }
        }
        yield return new WaitForSeconds(fireRate/2);

       
        forceFieldPrefab.SetActive(false);

               yield return new WaitForSeconds(fireRate/2);
        isReadyForForceField = true;
    }
  
}

