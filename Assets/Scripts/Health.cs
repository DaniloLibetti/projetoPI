using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    public void ReceiveDamage(float amount)
    {
        _health -= amount;
        
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
