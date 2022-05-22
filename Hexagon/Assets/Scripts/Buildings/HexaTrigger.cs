using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaTrigger : MonoBehaviour
{
    [SerializeField] Building parentBuilding;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mob")
        {
            if (parentBuilding.GetComponent<Tower>() != null)
            {
               
                parentBuilding.GetComponent<Tower>().TryShoot(other.GetComponentInParent<MobHealth>());
            }
            else if (parentBuilding.GetComponent<TowerForceField>() != null)
            {
                parentBuilding.GetComponent<TowerForceField>().TryForceFieldOn();
            }
        }
    }
}
