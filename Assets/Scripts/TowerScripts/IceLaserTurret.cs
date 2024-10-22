using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceLaserTurret : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private bool _useLaser = false;
    [SerializeField]
    private float _damageOverTime = 50;
    //[SerializeField]
    //private float _slowAmount = 0.5f;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private LineRenderer _lineRenderer;

    [Header("Particles")]
    [SerializeField]
    private ParticleSystem _laserHeadParticle;
    [SerializeField]
    private ParticleSystem _laserHitParticle;


    [SerializeField]
    private float _timer;

    public List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;

    private Health _targetEnemy;
    private EnemyBehaviour _targetEnemyBehaviour;
    private float _minDist = Mathf.Infinity;


    // Update is called once per frame
    void Update()
    {
        if (_lockedEnemy != null && _lockedEnemy.gameObject.activeInHierarchy)
        {
            if (_useLaser)
            {
                IceLaser();
        
            }
        
        }
        
        if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeInHierarchy)
        {
            if (_useLaser)
            {
                if (_lineRenderer.enabled)
                {
                    _lineRenderer.enabled = false;
                    _laserHitParticle.Stop();
                    _laserHeadParticle.Stop();
                }
            }
        
            return;
        }
    }

    private void IceLaser()
    {

        _targetEnemy.ReceiveDamage(_damageOverTime * Time.deltaTime);
        

        if (!_lineRenderer.enabled)
        {
            _lineRenderer.enabled = true;
            _laserHitParticle.Play();
            _laserHeadParticle.Play();
        }


        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _lockedEnemy.position);

        Vector3 dir = _firePoint.position - _lockedEnemy.position;

        _laserHitParticle.transform.position = _lockedEnemy.position + dir.normalized * 0.5f;

        _laserHitParticle.transform.rotation = Quaternion.LookRotation(dir);

        //_targetEnemyBehaviour.Slow(_slowAmount);
        if(_targetEnemyBehaviour._speed == _targetEnemyBehaviour._startSpeed)
        {
            _targetEnemyBehaviour._speed *= .5f;
        }
    }

    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            SetTarget();
            _timer = 0;
        }
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
        if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeInHierarchy)
        {
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (!_enemy[i].gameObject.activeInHierarchy)
                {
                    _enemy.Remove(_lockedEnemy);
                    _lockedEnemy = null;
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
                        _targetEnemy = _lockedEnemy.GetComponent<Health>();
                        _targetEnemyBehaviour = _lockedEnemy.GetComponent<EnemyBehaviour>();
                    }
                }
            }
        }

        _minDist = Mathf.Infinity;

    }

    /*private void OnDisable()
    {
        if (_targetEnemy.gameObject.activeInHierarchy && _targetEnemy != null)
        {
            _targetEnemyBehaviour._speed = -5f;
        }
    }*/

    private void Shoot()
    {
        if(_lockedEnemy != null && _lockedEnemy.gameObject.activeInHierarchy)
        {
            while (_lockedEnemy.gameObject.activeInHierarchy)
            {
                if (!_lineRenderer.enabled)
                {
                    _lineRenderer.enabled = true;
                    _laserHitParticle.Play();
                    _laserHeadParticle.Play();
                }

                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, _lockedEnemy.position);

                Vector3 dir = _firePoint.position - _lockedEnemy.position;
 
                _laserHitParticle.transform.SetPositionAndRotation(_lockedEnemy.position + dir.normalized * 0.5f, Quaternion.LookRotation(dir));

                //_targetEnemyBehaviour.Slow(_slowAmount);
            }
        }
        _lineRenderer.enabled = false;
        _laserHitParticle.Stop();
        _laserHeadParticle.Stop();
    }


    /*public void RemoveInactive()
    {
        if (!_lockedEnemy.gameObject.activeInHierarchy)
        {
            _enemy.Remove(_lockedEnemy);
            _lockedEnemy = null;
            _minDist = Mathf.Infinity;

        }
        SetTarget();
    }*/


}
