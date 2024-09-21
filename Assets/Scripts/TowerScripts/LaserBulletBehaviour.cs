using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserBulletBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundEnemy"))
        {
            other.GetComponent<Health>().ReceiveDamage(20);
            this.gameObject.SetActive(false);
        }
    }
}
