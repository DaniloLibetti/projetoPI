using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirBehaviour : MonoBehaviour
{

    public List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;
    private float _minDist = Mathf.Infinity;
    [SerializeField]
    private AntiAirHead _head;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            _enemy.Add(other.transform);
            SetTarget();
        }
    }

    public void SetTarget()
    {
        if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeSelf)
        {
            foreach(Transform t in _enemy)
            {
                float dist = Vector3.Distance(t.position, transform.position);
                if(dist < _minDist)
                {
                    _lockedEnemy = t;
                    _minDist = dist;
                    _head._enemyLocked = _lockedEnemy;
                }
            }
            _head.Shoot();
        }
    }
}
