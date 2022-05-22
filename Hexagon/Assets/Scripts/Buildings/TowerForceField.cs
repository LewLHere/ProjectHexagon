using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerForceField : Building
{
    [SerializeField] float fireRate = .5f;
    [SerializeField] GameObject forceFieldPrefab = null;
    [SerializeField] int[] damage;
    [SerializeField] float finalScale = 3.0f;
    [SerializeField] float scaleSpeed = 1f;
    bool isReadyForForceField = true;
    bool scaleForceFieldUp = false;

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

        isReadyForForceField = false;

        for (int i = 0; i < tile.GetMobsOnTile().Length; i++)
        {
            if (tile.GetMobsOnTile()[i] != null)
            {
                forceFieldPrefab.SetActive(true);

                tile.GetMobsOnTile()[i].TakeDamage(damage[level]);
            }
        }
        yield return new WaitForSeconds(fireRate/2);

       
        forceFieldPrefab.SetActive(false);

               yield return new WaitForSeconds(fireRate/2);
        isReadyForForceField = true;
    }
  
}

