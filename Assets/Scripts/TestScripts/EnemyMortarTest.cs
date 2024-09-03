using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMortarTest : MonoBehaviour
{
    [SerializeField]
    private CharacterController _mobController;
    // Update is called once per frame
    void Update()
    {
        _mobController.Move(new Vector3(-1, 0, 0) * Time.deltaTime);
    }
}
