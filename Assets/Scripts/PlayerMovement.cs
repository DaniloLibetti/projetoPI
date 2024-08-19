using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody playerRb;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //float Vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, 0f) * speed * Time.deltaTime;

        playerRb.MovePosition(transform.position + movement);
    }
}
