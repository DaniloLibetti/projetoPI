using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _enemy = new List<Transform>();
    [SerializeField]
    private Transform _lockedEnemy;
    [SerializeField]
    private float _minDist = Mathf.Infinity;
    [SerializeField]
    private AntiAirHead _head;
    [SerializeField]
    public float timer = 0;
    [SerializeField]
    float firerate = 2;

    private void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            _enemy.Add(other.transform);
            SetTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            _enemy.Remove(other.transform);
            _lockedEnemy = null;
            SetTarget();
        }
    }

    public void SetTarget()
    {
        //if (_lockedEnemy == null || !_lockedEnemy.gameObject.activeSelf)
        //{
        //}
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (!_enemy[i].gameObject.activeSelf)
                {
                    _enemy.Remove(_enemy[i]);
                }
            }
            foreach (Transform t in _enemy)
            {
                float dist = Vector3.Distance(t.position, transform.position);
                if(dist < _minDist)
                {
                    _lockedEnemy = t;
                    _minDist = dist;
                    _head._enemyLocked = _lockedEnemy;
                    timer = firerate;//vai começar atirando, ao inves de esperar (firerate) segundos pra começar a atirar
                }
            }
            if(_lockedEnemy != null)
            {
                _head.Shoot();
                timer = firerate;

            }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer > firerate)
        {
            _head.Shoot();
            timer = firerate;
        }
    }
    public void RemoveInactive()
    {
        if (_lockedEnemy == null)
            return;
        else if(!_lockedEnemy.transform.gameObject.activeSelf || _lockedEnemy.transform != null)
        {
            _enemy.Remove(_lockedEnemy);
            _lockedEnemy = null;
            _minDist = Mathf.Infinity;
        }
        SetTarget();
    }
}
