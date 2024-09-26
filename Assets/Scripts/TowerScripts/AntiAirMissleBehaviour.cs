using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirMissleBehaviour : MonoBehaviour
{

    public Transform _lockedTarget;
    [SerializeField]
    private Rigidbody _rB;
    public AntiAirBehaviour _behaviour;
    public EnemyBehaviour enemy;

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
            _behaviour._enemy.Remove(other.transform);
            _behaviour._lockedEnemy = null;
            _behaviour._minDist = Mathf.Infinity;
            _behaviour.SetTarget();
            //_behaviour.RemoveInactive();
            this.gameObject.SetActive(false);
        }
    }

    private void NoTarget()
    {
        this.gameObject.SetActive(false);
    }
}
