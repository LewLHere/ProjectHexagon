using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MobHealth : MonoBehaviour
{

    [SerializeField] int startHealth;
    [SerializeField] float changeHPtextTime = .3f;
    int currentHealth;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Canvas enemyDisplay;
   
    GameObject pulseZone;
    bool readyToBePulsed = true;
    int level;
    Animator anim;
    SpawnEnemies spawnEnemies;
    MobMover mob;
    int indexOnTile;
    void Start()
    {
       
        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        anim = GetComponent<Animator>();
        currentHealth = startHealth;
        hpText.text = "" + currentHealth;
        enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation;
        mob = GetComponent<MobMover>();

        spawnEnemies.ShowHP += ActivateHP;
        spawnEnemies.HideHP += DeactivateHP;
        if (spawnEnemies.hpAreOn == true)
        { hpText.gameObject.SetActive(true); }
    }
  
      public void TakeDamage(int damage)
    {
       
        if (mob.GetShield().activeSelf != true)
        {
           
           
            StartCoroutine("GotHit",damage);
            currentHealth -= damage;
           
        }
        else if (mob.GetShield().activeSelf == true)
        {
           
            mob.GetShield().SetActive(false);
        }

        hpText.text = ""+currentHealth;

        if (currentHealth <= 0)
        {
            spawnEnemies.ShowHP -= ActivateHP;
            spawnEnemies.HideHP -= DeactivateHP;
            hpText.gameObject.SetActive(false);
            mob.TriggerDie();
           
          
        }
    }

    IEnumerator GotHit(int damage)
    {
      
        if (damage > 0)
        {
            hpText.color = new Color32(255, 122, 122, 255);
            hpText.fontStyle = TMPro.FontStyles.Bold;
            yield return new WaitForSeconds(changeHPtextTime);
            hpText.color = new Color32(255, 255, 255, 255);
            hpText.fontStyle = TMPro.FontStyles.Normal;
        }
        else if (damage < 0 )
        {
            hpText.color = new Color32(122, 255, 122, 255);
            hpText.fontStyle = TMPro.FontStyles.Bold;
            yield return new WaitForSeconds(changeHPtextTime);
            hpText.color = new Color32(255, 255, 255, 255);
            hpText.fontStyle = TMPro.FontStyles.Normal;
        }
        
       
       
    }
    public void UpdateHpTextRotation()
    { enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation; }
    private void ActivateHP(object sender, EventArgs e)
    {
        if (hpText.gameObject != null)
        {
            hpText.gameObject.SetActive(true);
        }
    }

    private void DeactivateHP(object sender, EventArgs e)
    {
        if (hpText.gameObject != null)
        {
            hpText.gameObject.SetActive(false);
        }
    }
    public void SetIndexOnTile(int i)
    { indexOnTile = i; }

    public int GetIndexOnTile()
    { return indexOnTile; }

    public void SetHP(int hp)
    {
        startHealth = hp;
    }

    public int GetStartHP()
    {
        return
              startHealth;
    }

    public void SetCanvasToCamera()
    {
        enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation;
    }

  
}
