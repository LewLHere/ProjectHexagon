using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MobHealth targetMob;
    [SerializeField] float baseSpeed = 10;
    [SerializeField] float speed = 5;
    [SerializeField] int baseDamage = 5;
    [SerializeField] int damageRange = 3;
    [SerializeField] GameObject[] sparksPrefabs;
    public string projectileType;

    private void Start()
    {
        if(projectileType == "Red")
        {
            sparksPrefabs[3].SetActive(true);
        }
        if (projectileType == "Blue")
        {
            sparksPrefabs[2].SetActive(true);
        }
        if (projectileType == "Green")
        {
            sparksPrefabs[1].SetActive(true);
        }
        if (projectileType == "White")
        {
            sparksPrefabs[0].SetActive(true);
        }
    }
    private void LateUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        int multiplier = 2;
        int projectileDamage;
        if (other.GetComponentInParent<MobHealth>() != targetMob) return;
        if (other.tag == "Mob")
        {
            string otherColour = other.gameObject.GetComponentInParent<MobMover>().colour;
           
            if (otherColour == projectileType)
            {
                multiplier = 2;
            }
            else if (otherColour == "Green" && projectileType == "Red" ||
                        otherColour == "Blue" && projectileType == "Green" ||
                    otherColour == "Red" && projectileType == "Blue")
            {
                multiplier = 3;
            }
            else if (otherColour == "Red" && projectileType == "Green" ||
                        otherColour == "Green" && projectileType == "Blue" ||
                    otherColour == "Blue" && projectileType == "Red")
            {
                multiplier = 1;
            }
            else
            {
                multiplier = 2;
            }
            System.Random rnd = new System.Random();
            projectileDamage = rnd.Next((baseDamage * multiplier) - damageRange, (baseDamage * multiplier) + damageRange);
       
       
        other.GetComponentInParent<MobHealth>().TakeDamage(projectileDamage);
        Destroy(gameObject);
    }
}

        private void Move()
        {  
            if (targetMob == null)
                {
                    Destroy(gameObject);
                    return;
                }
            else if (targetMob != null)
                {
           
                    var step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetMob.transform.position.x, transform.position.y, targetMob.transform.position.z), step);
                    float distance = Vector3.Distance(transform.position, new Vector3(targetMob.transform.position.x, transform.position.y, targetMob.transform.position.z));
                }
        }
    
    public void SetTarget(MobHealth target)
    {
        targetMob = target;
    }
    public void SetSpeed(int level)
    { 
        speed = baseSpeed * 1;
    }

    public void SetDamage(int damage)
    {
        baseDamage = damage;
    }
}

