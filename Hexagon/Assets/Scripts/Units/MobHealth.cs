using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobHealth : MonoBehaviour
{

    [SerializeField] int startHealth;
    int currentHealth;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Canvas enemyDisplay;
    GameObject pulseZone;
    bool readyToBePulsed = true;
    int level;
    Animator anim;
    
    MobMover mob;
    int indexOnTile;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startHealth;
        hpText.text = "" + currentHealth;
        enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation;
        mob = GetComponent<MobMover>();
        
    }
  
      public void TakeDamage(int damage)
    {
      
        if (mob.GetShield().activeSelf != true)
        {
            currentHealth -= damage;
           
        }
        else if (mob.GetShield().activeSelf == true)
        {
           
            mob.GetShield().SetActive(false);
        }

        hpText.text = ""+currentHealth;

        if (currentHealth <= 0)
        { Destroy(gameObject); }
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

  
}
