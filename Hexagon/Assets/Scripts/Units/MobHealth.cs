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

    int level;
    
    MobMover mob;
    int indexOnTile;
    void Start()
    {
        currentHealth = startHealth;
        hpText.text = "" + currentHealth;
        enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation;
        mob = GetComponent<MobMover>();
    }

   
    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
      
        if (mob.GetShield().activeSelf != true)
        {
            currentHealth -= damage;
            Debug.Log(damage);
        }
        else if (mob.GetShield().activeSelf == true)
        {
            Debug.Log("Shield Gone!");
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
