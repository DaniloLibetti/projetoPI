using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _damage;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KABOOM!");

        if (other.CompareTag("GroundEnemy"))
        { 
            //_seeEnemies.enabled = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.55f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("GroundEnemy"))
                {
                    hitCollider.GetComponent<Health>().ReceiveDamage(_damage);
                }
                //hitCollider.SendMessage("AddDamage");
            }
            gameObject.SetActive(false);
        }
    }
}
