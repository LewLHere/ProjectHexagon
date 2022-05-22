using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Canvas enemyDisplay;
    MobMover mob;
    int indexOnTile;
    void Start()
    {
        hpText.text = "" + health;
        enemyDisplay.transform.rotation = FindObjectOfType<Camera>().transform.rotation;
        mob = GetComponent<MobMover>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        if (mob.GetShield().activeSelf != true)
        {
            health -= damage;
            Debug.Log(damage);
        }
        else if (mob.GetShield().activeSelf == true)
        {
            Debug.Log("Shiled Gone!");
            mob.GetShield().SetActive(false);
        }

        hpText.text = ""+health;

        if (health <= 0)
        { Destroy(gameObject); }
    }

    public void SetIndexOnTile(int i)
    { indexOnTile = i; }
    public int GetIndexOnTile()
    { return indexOnTile; }
}
