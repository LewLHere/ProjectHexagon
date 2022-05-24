using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    [SerializeField] float fireRate = .5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] MobHealth[] mobsInRange;
    [SerializeField] int[] damage;
  
    GameObject target;
    bool readyToShoot = true;
    private void Start()
    {
        mobsInRange = new MobHealth[300];
        bm = FindObjectOfType<BuildManager>();
        tg = FindObjectOfType<TileGroups>();
        neighboredTiles = tile.GetNeighboredTiles(tile.gameObject);
        buildingIndex = 1;
    }

    public void TryShoot(MobHealth target)
    {
      
        if (!readyToShoot) { return; }
        StartCoroutine("ShootRoutine", target);
        anim.SetTrigger("Shoot");
       
    }

  
    IEnumerator ShootRoutine(MobHealth target)
    {
       
        readyToShoot = false;
        GameObject projectileInstance = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<Projectile>().SetTarget(target);               // Random Target instead of mobs[0]!!! - or the one with shortest Distance!
        projectileInstance.GetComponent<Projectile>().SetSpeed(level);                       // Not used currently
        projectileInstance.GetComponent<Projectile>().SetDamage(damage[level]);
        projectileInstance.GetComponent<Projectile>().projectileType = tile.tag;

        yield return new WaitForSeconds(fireRate);

        readyToShoot = true;
    }

 
 }


