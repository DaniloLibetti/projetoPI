using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject _CollectableMaterial;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private Health _enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyHealth.GetComponent<Health>()._health <= 0)
        {
            DropCoin();
        }
    }

    public void DropCoin()
    {
        Vector3 position = transform.position;
        GameObject mat = Instantiate(_CollectableMaterial, position, Quaternion.identity);
    }
}
