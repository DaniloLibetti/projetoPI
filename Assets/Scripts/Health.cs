using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;



    public void ReceiveDamage(float amount)
    {
        _health -= amount;
        
        if(_health <= 0 && transform.parent)
        {
            //gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
        else if(_health <= 0 && !transform.parent)
        {
            gameObject.SetActive(false);
        }
    }
}
