using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowers : MonoBehaviour
{
    public GameObject[] _nextTurrent;
    public int[] _cost;
    [SerializeField]
    private float _yPosition;
    [SerializeField]
    private float _zPosition;

    public int _upgradeUiIndex;

    [SerializeField]
    private GameObject _toDestroy;

    public void Upgrade()
    {
        //GameObject upgraded = Instantiate(_nextTurrent[correctTurret], transform.position, transform.rotation);
        //upgraded.transform.GetComponentInChildren<BoxCollider>().enabled = true;
        Destroy(_toDestroy);
    }
}
