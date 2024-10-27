using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirBehaviour2 : MonoBehaviour
{
    bool m_Started;
    public LayerMask m_LayerMask;
    [SerializeField]
    private float _timer;
    public Transform _lockedEnemy;
    [SerializeField]
    private AntiAirHead _head;
    private float _minDist = Mathf.Infinity;
    [SerializeField]
    private bool _burst;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        //m_Started = true;
    }

    void FixedUpdate()
    {
        _timer += Time.deltaTime;        
        if(_timer >= 2)
        {
            DetectEnemy();
            _timer = 0;
        }
    }

    private void DetectEnemy()
    {
        Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x + 20, transform.position.y + 10, transform.position.z), transform.localScale * 40, Quaternion.identity, m_LayerMask);
        int i = 0;

        if(hitColliders.Length > 0)
        {
            while (i < hitColliders.Length)
            {
                if(_lockedEnemy != null && hitColliders[i].gameObject == _lockedEnemy)
                {
                    _head._enemyLocked = _lockedEnemy;
                    if (_burst)
                    {
                        StartCoroutine(Burst());
                    }
                    else
                        _head.Shoot();
                    return;
                }
                else
                {
                    float dist = Vector3.Distance(hitColliders[i].transform.position, transform.position);
                    if (dist < _minDist && hitColliders[i].gameObject.activeInHierarchy)
                    {
                        _lockedEnemy = hitColliders[i].transform;
                        _minDist = dist;
                    }
                }
                i++;
            }
            _minDist = Mathf.Infinity;
            _head._enemyLocked = _lockedEnemy;
            if (_burst)
            {
                StartCoroutine(Burst());
            }
            else
                _head.Shoot();
        }
    }

    IEnumerator Burst()
    {
        yield return new WaitForSeconds(.2f);
        _head.Shoot();
        yield return new WaitForSeconds(.2f);
        _head.Shoot();
        yield return new WaitForSeconds(.2f);
        _head.Shoot();
    }
}
