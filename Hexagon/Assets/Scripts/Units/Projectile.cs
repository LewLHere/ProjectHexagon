using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MobHealth targetMob;
    [SerializeField] float baseSpeed = 10;
    [SerializeField] float speed = 5;
    [SerializeField] int projectileDamage = 5;

    private void LateUpdate()
    {
            Move();  
    }

     private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Mob")
            {
            Debug.Log("TookDamage");
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
        speed = baseSpeed * (level+1);
    }
}

