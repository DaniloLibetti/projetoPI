using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _damage;
    public float _startSpeed = -5f;
    public float _speed;
    [SerializeField]
    private Transform _basePos;
    [SerializeField]
    private LayerMask _layerMask;
    private Vector3 _direction;
    public Vector3 velocity;
    [SerializeField]
    private Rigidbody _rB;



    // Start is called before the first frame update
    void Start()
    {
        _speed = _startSpeed;

        _direction = transform.position - _basePos.position;
        _direction.Normalize();
        transform.LookAt(_direction);
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(_direction.x * _speed * Time.deltaTime, 0, 0);
        //_rB.velocity = new Vector3(_direction.x * _speed * Time.deltaTime, 0, 0);

        //_speed = _startSpeed;
    }

    private void FixedUpdate()
    {
        _rB.velocity = new Vector3(_direction.x * _speed, 0, 0);
    }


    //public void Slow (float pct)
    //{
    //    _speed = _startSpeed * (1f - pct);
    //} 

    private void Shoot()
    {

        Ray shootRay = new Ray(transform.position, -_direction);
        Debug.DrawRay(transform.position, -_direction * 2, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, 2, _layerMask))
        {
            Health health = hitInfo.transform.gameObject.GetComponent<Health>();
            //if (health == null && (hitInfo.transform.position - transform.position).x <= 2)
            //{
            //    //_speed = -5;
            //}
            if (hitInfo.transform.gameObject.layer == 6 || hitInfo.transform.gameObject.layer == 7 || hitInfo.transform.gameObject.layer == 11)
            {
                _speed = 0;
            }
            if (health != null && hitInfo.transform.gameObject.layer == 6)
            {
                health.ReceiveDamage(_damage);
            }
        }
        else
        {
            _speed = -5;
        }

        Invoke("Shoot", .2f);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //        _speed = 0;
    //}
    //
    //private void OnCollisionExit(Collision collision)
    //{
    //    _speed = -5;
    //}

}
