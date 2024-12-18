using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _fireRate = 1f;
    [SerializeField]
    private BoxCollider _shootDistance;
    [SerializeField]
    private GameObject _laserBullet;
    [SerializeField]
    private float _bulletSpeed = 10;

    [SerializeField]
    private Transform _turretPivot;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private ParticleSystem _shootEffect;
    [SerializeField]
    private AudioSource _shootAudio;


    private float _shootCooldown;
    private GameObject _activeTarget;


    public List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;
    private float _minDist = Mathf.Infinity;


    // Start is called before the first frame update
    void Start()
    {
        _shootCooldown = _fireRate;
    }

    // Update is called once per frame
    void Update()
    {


        if (_shootCooldown <= 0 && _lockedEnemy != null)
        {
            if (!_lockedEnemy.gameObject.activeInHierarchy)
            {
                _enemy.Remove(_lockedEnemy);
                _lockedEnemy = null;
                _shootCooldown = _fireRate;
                SetTarget();
            }
            else
            {
                _shootCooldown = _fireRate;
                Shoot();
            }
        }
        if(_shootCooldown <= 0 && _lockedEnemy == null)
        {
            _shootCooldown = _fireRate;
            SetTarget();
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
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i] == null)
                {
                    _enemy.RemoveAt(i);
                }
            }
            foreach (Transform t in _enemy)
            {
                if (t != null)
                {
                    float dist = Vector3.Distance(t.position, transform.position);
                    if (dist < _minDist)
                    {
                        _lockedEnemy = t;
                        _minDist = dist;

                    }
                }
            }
        }
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


    private void Shoot()
    {
        if(_lockedEnemy != null && _lockedEnemy.gameObject.activeSelf)
        {
            _shootAudio.Play();
            GameObject bullet = Instantiate(_laserBullet, _firePoint.position, _firePoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = _firePoint.forward * _bulletSpeed;
        }
    }

}
