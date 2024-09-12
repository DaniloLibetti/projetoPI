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
    float timer = 0;
    [SerializeField]
    float firerate = 2;
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
                    Debug.Log(t.name);
                    _lockedEnemy = t;
                    _minDist = dist;
                    _head._enemyLocked = _lockedEnemy;
                    timer = firerate;//vai começar atirando, ao inves de esperar (firerate) segundos pra começar a atirar
                }
            }
            //_head.Shoot();
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > firerate)
        {
            _head.Shoot();
            timer %= firerate;
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
