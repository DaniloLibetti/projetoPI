using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    private float _duration = 2f;
    private float _t = 0f;
    private float _currentHeight = 0;
    private float _currentX = 0;
    private float _pingpongHeight = 1;
    private float _pingpongX = 1;
    private float _attackSpeed;
    private Vector3 _start;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private Vector3 _target;
    [SerializeField]
    private AnimationCurve _heightCurve;
    [SerializeField]
    private AnimationCurve _idleCurve;
    [SerializeField]
    private AnimationCurve _attackCurve;
    [SerializeField]
    private Transform _lockedEnemy;
    [SerializeField]
    private float _timer;
    private float _minDist = Mathf.Infinity;
    public LayerMask m_LayerMask;
    public bool _topHeight;
    public bool _attack = false;
    private bool _firstRound = false;
    public DroneLauncher _launcher;

    public Transform _target1, _target2, _currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        _start = this.transform.position;
        StartCoroutine(DroneHeight());
    }

    // Update is called once per frame
    void Update()
    {
        if(_topHeight == true)
        {
            if (transform.position.y == 10 || transform.position.y == 12)
            {
                _pingpongHeight = _pingpongHeight == 0 ? 1 : 0;
            }
            if (transform.position.x == _start.x + 10 || transform.position.x == _start.x - 10)
            {
                _pingpongX = _pingpongX == 0 ? 1 : 0;
            }
            _currentHeight = Mathf.MoveTowards(_currentHeight, _pingpongHeight, 1f * Time.deltaTime);
            _currentX = Mathf.MoveTowards(_currentX, _pingpongX, .15f * Time.deltaTime);
            this.transform.position = Vector3.Lerp(new Vector3(transform.position.x, 10, _start.z), new Vector3(transform.position.x, 12, _start.z), _idleCurve.Evaluate(_currentHeight));
            this.transform.position = Vector3.Lerp(new Vector3(_start.x + _t, transform.position.y, _start.z), new Vector3(_start.x - 10, transform.position.y, _start.z), _idleCurve.Evaluate(_currentX));
            if(transform.position.x <= _start.x - 10 && _firstRound == false)
            {
                _t = 10;
                _firstRound = true;
            }
        }
        else if(_attack == true && _lockedEnemy != null && _lockedEnemy.gameObject.activeInHierarchy)
        {
            _attackSpeed = Mathf.MoveTowards(0, 1, 4f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, _lockedEnemy.position, _attackCurve.Evaluate(_attackSpeed));
        }
    }

    void FixedUpdate()
    {
        if(_topHeight == true && (_lockedEnemy == null || !_lockedEnemy.gameObject.activeInHierarchy))
        {
            _timer += Time.deltaTime;
            if (_timer >= 2)
            {
                if(_attack == false)
                    DetectEnemy();
                _timer = 0;
            }
        }
    }

    private void DetectEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50, m_LayerMask);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Color.red, 2);
        int i = 0;

        if (hitColliders.Length > 0)
        {
            while (i < hitColliders.Length)
            {
                float dist = Vector3.Distance(hitColliders[i].transform.position, transform.position);
                if (dist < _minDist && hitColliders[i].gameObject.activeInHierarchy)
                {
                    _lockedEnemy = hitColliders[i].transform;
                    _minDist = dist;
                    _topHeight = false;
                    _attack = true;
                }
                i++;
            }
            _minDist = Mathf.Infinity;
        }
    }



    public IEnumerator DroneHeight()
    {

        while (_t < 1)
        {
            _t += Time.deltaTime / _duration;

            if (_t > 1) _t = 1;
            float t1 = _heightCurve.Evaluate(_t);
            transform.position = Vector3.Lerp(_start,new Vector3(transform.position.x, _target.y, transform.position.z), t1);

            yield return null;
        }
        _topHeight = true;
        _currentX = 0;
        _t = 0;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            other.GetComponent<Health>().ReceiveDamage(70);
            _lockedEnemy = null;
            _attack = false;
            if (_launcher.isActiveAndEnabled)
            {
                _launcher.Revive(this.gameObject);
            }
            this.gameObject.SetActive(false);
        }
    }
}
