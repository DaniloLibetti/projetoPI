using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _damage;
    [SerializeField]
    private BoxCollider _boxColiider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turret"))
        {
            other.GetComponent<Health>().ReceiveDamage(_damage);
        }
    }
}
