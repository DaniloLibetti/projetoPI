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
    private Vector3 _start;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private Vector3 _target;
    [SerializeField]
    private Transform _drone;
    [SerializeField]
    private AnimationCurve _heightCurve;
    [SerializeField]
    private AnimationCurve _idleCurve;
    ///[SerializeField]
    ///private AnimationCurve _idleCurve;
    private bool _topHeight;
    private bool _firstRound = false;

    // Start is called before the first frame update
    void Start()
    {
        _start = transform.position;
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
            if (transform.position.x == _start.x || transform.position.x == _start.x - 10)
            {
                _pingpongX = _pingpongX == 0 ? 1 : 0;
            }
            _currentHeight = Mathf.MoveTowards(_currentHeight, _pingpongHeight, 1f * Time.deltaTime);
            _currentX = Mathf.PingPong(.1f * Time.time, 1);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, 10, _start.z), new Vector3(transform.position.x, 12, _start.z), _idleCurve.Evaluate(_currentHeight));
            transform.position = Vector3.Lerp(new Vector3(_start.x + _t, transform.position.y, _start.z), new Vector3(_start.x - 10, transform.position.y, _start.z), _idleCurve.Evaluate(_currentX));
            if(transform.position.x <= _start.x - 5 && _firstRound == false)
            {
                _t = 10;
                _firstRound = true;
            }
        }

    }



    private IEnumerator DroneHeight()
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
        _t = 0;
        yield return null;
    }
}
