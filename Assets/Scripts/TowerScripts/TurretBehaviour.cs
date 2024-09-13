using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _fireRate = 1f;
    [SerializeField]
    private float _damage = 20f;
    [SerializeField]
    private float _maxShootDistance = 10f;
    [SerializeField]
    private BoxCollider _shootDistance;

    [SerializeField]
    private Transform _turretPivot;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private ParticleSystem _shootEffect;


    private float _shootCooldown;
    private GameObject _activeTarget;


    public List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;
    private float _minDist = Mathf.Infinity;
    float timer = 0;
    [SerializeField]
    float firerate = 2f;


    // Start is called before the first frame update
    void Start()
    {
        _shootCooldown = _fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(_shootCooldown <= 0 && _lockedEnemy != null)
        {
            _shootCooldown = _fireRate;
            Shoot();
        }

          _shootCooldown -= Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundEnemy"))
        {
            _enemy.Add(other.transform);
            SetTarget();
        }
    }

    public void SetTarget()
    {
        if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeSelf)
        {
            foreach (Transform t in _enemy)
            {
                float dist = Vector3.Distance(t.position, transform.position);
                if (dist < _minDist)
                {
                    Debug.Log(t.name);
                    _lockedEnemy = t;
                    _minDist = dist;
                   
                }
            }
            //_head.Shoot();
        }
    }

    private void Shoot()
    {
        _shootEffect.Play();

        Ray shootRay = new Ray(_firePoint.position, _firePoint.forward);
        Debug.DrawRay(_firePoint.position, _firePoint.TransformDirection(Vector3.forward) * 10, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxShootDistance))
        {
            Health health = hitInfo.transform.gameObject.GetComponent<Health>();
            
            
            if(health == null)
            {
                Debug.LogWarning("acertou algo sem health. . . . . . .");
            }
            else
            {
                health.ReceiveDamage(_damage);
            }
        }

        RemoveInactive();

    }

    public void RemoveInactive()
    {
        if (!_lockedEnemy.gameObject.activeSelf)
        {
            _enemy.Remove(_lockedEnemy);
            _lockedEnemy = null;
            _minDist = Mathf.Infinity;
        }
        SetTarget();
    }

}
