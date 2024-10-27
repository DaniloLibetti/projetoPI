using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaserTurret : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private bool _useLaser = false;
    [SerializeField]
    private float _damageOverTime = 50;
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

    private float _minDist = Mathf.Infinity;


    // Update is called once per frame
    void Update()
    {
        if (_lockedEnemy != null && _lockedEnemy.gameObject.activeInHierarchy)
        {
            if (_useLaser)
            {
                FireLaser();
                
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
                    SetTarget();
                }
            }

            return;
        }
    }

    private void FireLaser()
    {

        _targetEnemy.ReceiveDamage(_damageOverTime * Time.deltaTime);

        if (!_lineRenderer.enabled)
        {
            _lineRenderer.enabled= true;
            _laserHitParticle.Play();
            _laserHeadParticle.Play();
        }


        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _lockedEnemy.position);

        Vector3 dir = _firePoint.position - _lockedEnemy.position;

        _laserHitParticle.transform.position = _lockedEnemy.position + dir.normalized * 0.5f;

        _laserHitParticle.transform.rotation = Quaternion.LookRotation(dir);

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
        if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeSelf)
        {
            
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
                    }
                }
            }
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i] == null)
                {
                    _enemy.Remove(_lockedEnemy);
                    _lockedEnemy = null;
                }
            }
        }

        _minDist = Mathf.Infinity;

    }


    /*public void RemoveInactive()
    {
        if (!_lockedEnemy.gameObject.activeSelf)
        {
            _enemy.Remove(_lockedEnemy);
            _lockedEnemy = null;
            _minDist = Mathf.Infinity;

        }
        SetTarget();
    }*/


}
