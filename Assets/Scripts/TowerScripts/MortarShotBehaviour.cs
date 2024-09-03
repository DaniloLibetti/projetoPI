using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShotBehaviour : MonoBehaviour
{
    [SerializeField]
    private SphereCollider _seeEnemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
        {
            //_seeEnemies.enabled = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.55f);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.CompareTag("Enemy"))
                {
                    Debug.Log(hitCollider.name);
                    hitCollider.GetComponent<Health>().ReceiveDamage(100);
                }
                //hitCollider.SendMessage("AddDamage");
            }
            this.gameObject.SetActive(false);
        }
    }
}
