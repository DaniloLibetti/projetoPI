using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject _drone;
    [SerializeField]
    private float _launchate;

    void Start()
    {
        Invoke("Launch", 1);
    }

    private void Launch()
    {
        Instantiate(_drone, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        Invoke("Launch", _launchate);
    }
}
