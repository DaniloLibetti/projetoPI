using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstantiations : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _turrents;
    [SerializeField]
    private Transform _instantiationTarget;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(_turrents[0], new Vector3(_instantiationTarget.position.x, -4.366f, _instantiationTarget.position.z), transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(_turrents[1], new Vector3(_instantiationTarget.position.x, -4.18f, _instantiationTarget.position.z), _instantiationTarget.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(_turrents[2], new Vector3(_instantiationTarget.position.x, -4.18f, _instantiationTarget.position.z), Quaternion.Euler(0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeTurret();
        }
    }

    private void UpgradeTurret()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.right);
        Ray shootRay = new Ray(transform.position, rayDirection);
        Debug.DrawRay(transform.position, rayDirection * 5, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, 5))
        {
            UpgradeTowers upgrade = hitInfo.transform.GetComponent<UpgradeTowers>();
            if (upgrade)
            {
                upgrade.Upgrade();
            }
        }
    }
}
