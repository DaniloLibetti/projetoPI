using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _fireRate = 0.05f;
    [SerializeField]
    private float _damage = 20f;
    [SerializeField]
    private float _rotationSpeed = 5f;
    [SerializeField]
    private float _maxShootDistance = 10f;
    [SerializeField]
    private BoxCollider _shootDistance;

    [SerializeField]
    private Transform _turretPivot;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private ParticleSystem _shootEffect;

    [Header("Targets")]
    [SerializeField]
    private List<GameObject> _targets;

    private float _shootCooldown;
    private GameObject _activeTarget;


    // Start is called before the first frame update
    void Start()
    {
        _shootCooldown = _fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Olha pro proximo alvo
        LookAtTarget();
            if (_shootCooldown <= 0)
            {
                _shootCooldown = _fireRate;
                if (_activeTarget != null)
                {
                    Shoot();
                }
            }
            else
            {
                _shootCooldown -= Time.deltaTime;
            }
        
    }

    private GameObject GetNextTarget()
    {
        if (_targets.Count > 0)
        {
            GameObject target = _targets[0];
            _targets.Remove(target);
            return target;
        }

        return null;
    }



    private void LookAtTarget()
    {
        //Achar o proximo alvo
        if(_activeTarget == null || !_activeTarget.activeSelf)
        {
            _activeTarget = GetNextTarget();
        }

        //Caso nao tenha mais alvos
        if(_activeTarget == null)
        {
            return;
        }

        //Pegar a direção
        Vector3 direction = _activeTarget.transform.position - _turretPivot.position;
        //Pegar a rotação
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        //Rotacao suave
        Vector3 targetRotation = Quaternion.Slerp(_turretPivot.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        //rotacionar só no eixo y
        _turretPivot.rotation = Quaternion.Euler(Vector3.Scale(targetRotation, _turretPivot.up));

    }


    private void Shoot()
    {
        _shootEffect.Play();

        Ray shootRay = new Ray(_firePoint.position, _firePoint.forward);

        if(Physics.Raycast(shootRay, out RaycastHit hitInfo, _maxShootDistance))
        {
            Health health = hitInfo.transform.gameObject.GetComponent<Health>();
            
            if(health == null)
            {
                Debug.LogWarning("acertou algo sem health. . . . . . .");
            }
            else
            {
                health.ReceiveDamage(_damage);
            }
        }

    }

}
