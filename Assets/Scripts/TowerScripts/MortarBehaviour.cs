using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject _mortarShot;
    [SerializeField]
    private float _verticalShotForce = 700;
    [SerializeField]
    private float _horizontalShotForce = 200;
    [SerializeField]
    private float _fireRate;
    [SerializeField]
    private ParticleSystem _shootEffect;
    [SerializeField]
    private AudioSource _shootAudio;
    
    

    private void Start()
    {
        if(transform.rotation.y == 1)
        {
            _horizontalShotForce *= -1;
        }
        Invoke("Shoot", 1);
    }

    private void Shoot()
    {
        _shootEffect.Play();
        _shootAudio.Play();
        GameObject shot = Instantiate(_mortarShot, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        shot.GetComponent<Rigidbody>().AddForce(new Vector3(_horizontalShotForce, _verticalShotForce, 0));
        Invoke("Shoot", _fireRate);
    }
}
