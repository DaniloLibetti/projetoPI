using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaserTurret : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private bool _useLaser = false;
    [SerializeField]
    private Transform _firePoint;
    public LineRenderer _lineRenderer;




    public List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;
    private float _minDist = Mathf.Infinity;


    // Update is called once per frame
    void Update()
    {
        if (_useLaser)
        {
            FireLaser();
            
        }

        /*if (_lockedEnemy = null)
        {
            if (_useLaser)
            {
                if (_lineRenderer.enabled)
                {
                    _lineRenderer.enabled = false;
                }
            }

            return;
        }*/
    }

    private void FireLaser()
    {
        /*if (!_lineRenderer.enabled)
        {
            _lineRenderer.enabled= true;
        }*/


        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _lockedEnemy.position);

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

                    }
                }
            }
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i] == null)
                {
                    _enemy.RemoveAt(i);
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


}
