using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirMissleBehaviour : MonoBehaviour
{

    public Transform _lockedTarget;
    [SerializeField]
    private Rigidbody _rB;
    public AntiAirBehaviour2 _behaviour;
    public AntiAirHead _head;
    public EnemyBehaviour enemy;
    private float _minDist = Mathf.Infinity;
    public LayerMask m_LayerMask;

    private void Start()
    {
        if(_lockedTarget != null)
        {
            enemy = _lockedTarget.GetComponent<EnemyBehaviour>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_lockedTarget != null && enemy == null)//so vai rodar ruma vez, caso lockedtarget seja null no start
        {
            enemy = _lockedTarget.GetComponent<EnemyBehaviour>();
        }
        if (_lockedTarget != null && _lockedTarget.gameObject.activeSelf)
        {
            transform.LookAt(_lockedTarget);
            Vector3 enemyPos = new Vector3(_lockedTarget.position.x, _lockedTarget.position.y, _lockedTarget.position.z) + enemy.velocity * .2f;
            Vector3 direction = enemyPos - transform.position;
            Vector3 velocity = _rB.velocity;
            direction.Normalize();
            direction *= 15;//velocidade de movimento
            float t = 2f * Time.deltaTime;//aceleraçao
            velocity = Vector3.Lerp(velocity, direction, t);
            _rB.velocity = velocity;
            //_rB.AddForce(sdirection, ForceMode.Force);
        }
        else
        {
            _rB.AddForce(Vector3.forward, ForceMode.Force);
            Invoke("NoTarget", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            other.GetComponent<Health>().ReceiveDamage(50);
            _head._enemyLocked = null;
            _behaviour._lockedEnemy = null;
            this.gameObject.SetActive(false);
        }
    }

    private void NoTarget()
    {
        if (_lockedTarget == null || !_lockedTarget.gameObject.activeInHierarchy)
        {
            Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x + 20, transform.position.y + 10, transform.position.z), transform.localScale * 40, Quaternion.identity, m_LayerMask);
            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders.Length > 0)
                {
                    float dist = Vector3.Distance(hitColliders[i].transform.position, transform.position);
                    if (dist < _minDist && hitColliders[i].gameObject.activeInHierarchy)
                    {
                        _lockedTarget = hitColliders[i].transform;
                        enemy = _lockedTarget.GetComponent<EnemyBehaviour>();
                        _minDist = dist;
                    }
                    i++;
                }
                else
                {
                    transform.position = new Vector3(500, 500, 500);
                    this.gameObject.SetActive(false);
                }
            }
            _minDist = Mathf.Infinity;
            Invoke("NoTarget", 1);
        }
        else
            return;
    }
}
