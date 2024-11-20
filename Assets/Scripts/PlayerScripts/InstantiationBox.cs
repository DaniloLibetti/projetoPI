using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationBox : MonoBehaviour
{
    [SerializeField]
    private Material _mat;
    [SerializeField]
    private TowerInstantiations _instantiationScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Instantiation"))
        {
            _instantiationScript._canInstantiate = false;
            Color color = new Color(1, 0, 0, .7f);
            _mat.color = color;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Instantiation"))
        {
            _instantiationScript._canInstantiate = true;
            Color color = new Color(0, 1, 0, .5f);
            _mat.color = color;
        }
    }
}
