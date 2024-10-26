
using UnityEngine;


public class Collectable : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TowerInstantiations updateMaterialText = other.GetComponent<TowerInstantiations>();
            updateMaterialText._materialAmount += 3;
            updateMaterialText.UpdateMaterialCounter();
            Destroy(this.gameObject);
        }
    }
}
