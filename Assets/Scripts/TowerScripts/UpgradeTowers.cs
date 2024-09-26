using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowers : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextTurrent;
    [SerializeField]
    private float _yPosition;
    [SerializeField]
    private float _zPosition;

    public void Upgrade()
    {
        GameObject upgraded = Instantiate(_nextTurrent, transform.position, transform.rotation);
        //upgraded.transform.GetComponentInChildren<BoxCollider>().enabled = true;
        Destroy(this.gameObject);
    }
}
