using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMaterials : MonoBehaviour
{
    [SerializeField]
    private TowerInstantiations _playerIsNear;
    private void OnTriggerEnter(Collider other)
    {
        _playerIsNear._isNearShip = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerIsNear._isNearShip = false;
    }
}
