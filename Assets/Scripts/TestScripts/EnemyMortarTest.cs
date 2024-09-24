using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMortarTest : MonoBehaviour
{
    [SerializeField]
    private CharacterController _mobController;
    // Update is called once per frame
    public Vector3 velocity;



    void Update()
    {
        //velocity = new Vector3(-1, 0, 0) * 5;
        //_mobController.Move(velocity * Time.deltaTime);
        transform.Translate(-5 * Time.deltaTime, 0, 0);
    }
}
