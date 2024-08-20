using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody playerRb;
    [SerializeField] Animator _charAnim;

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

        switch (horizontal)
        {
            case < -.01f:
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                _charAnim.SetBool("Walk", true);
                break;
            case > .01f:
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                _charAnim.SetBool("Walk", true);
                break;
            default:
                _charAnim.SetBool("Walk", false);
                break;
        }

        //if(horizontal < -.01f || horizontal > .01f)
        //{
        //    _charAnim.SetBool("Walk", true);
        //}
        //else if(horizontal == 0)
        //{
        //    _charAnim.SetBool("Walk", false);
        //}
    }
}
