using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirMissleBehaviour : MonoBehaviour
{

    public Transform _lockedTarget;
    [SerializeField]
    private Rigidbody _rB;
    public AntiAirBehaviour _behaviour;


    private void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_lockedTarget != null && _lockedTarget.gameObject.activeSelf)
        {
            transform.LookAt(_lockedTarget);
            Vector3 enemyPos = new(_lockedTarget.position.x - 5, _lockedTarget.position.y, _lockedTarget.position.z);
            Vector3 direction = transform.position - enemyPos;
            _rB.AddForce(-direction, ForceMode.Force);
        }
        else
        {
            _rB.AddForce(Vector3.forward * 5, ForceMode.Force);
            Invoke("NoTarget", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirEnemy"))
        {
            //_behaviour._enemy.Remove(other.transform);
            _behaviour.SetTarget();
            other.GetComponent<Health>().ReceiveDamage(50);
            this.gameObject.SetActive(false);
        }
    }

    private void NoTarget()
    {
        this.gameObject.SetActive(false);
    }
}
