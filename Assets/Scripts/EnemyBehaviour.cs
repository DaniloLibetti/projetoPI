using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _damage;
    public float _speed;
    [SerializeField]
    private Transform _basePos;
    [SerializeField]
    private LayerMask _layerMask;
    private Vector3 _direction;
    public Vector3 velocity;



    // Start is called before the first frame update
    void Start()
    {
        _direction = transform.position - _basePos.position;
        _direction.Normalize();
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.x * _speed * Time.deltaTime, 0, 0);
    }


    private void Shoot()
    {

        Ray shootRay = new Ray(transform.position, Vector3.left);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 2, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, 2, _layerMask))
        {
            Health health = hitInfo.transform.gameObject.GetComponent<Health>();

            if (health == null && (hitInfo.transform.position - transform.position).x <= 2)
            {
                _speed = -5;
            }
            if (health != null && (hitInfo.transform.position - transform.position).x <= 2)
            {
                health.ReceiveDamage(_damage);
                _speed = 0;
            }

        }
        else
        {
            _speed = -5;
        }

        Invoke("Shoot", .2f);

    }

}
