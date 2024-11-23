using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShotBehaviour : MonoBehaviour
{
    //[SerializeField]
    //private SphereCollider _seeEnemies;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private ParticleSystem _fireMortarParticle;
    [SerializeField]
    private GameObject _shotRender;
    [SerializeField]
    private Rigidbody _shotRB;
    [SerializeField]
    private bool _isMine;
    [SerializeField]
    private bool _isFire;
    [SerializeField]
    private ParticleSystem _hitGroundEffect;
    [SerializeField]
    private AudioSource _hitAudio;
    [SerializeField]
    private AudioClip _thump;
    [SerializeField]
    private AudioClip _fire;
    

    private float _fireRoutine = 0;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Ground") || other.CompareTag("GroundEnemy")) && _isFire == false && _isMine == false)
        {
            _hitGroundEffect.Play();
            _hitAudio.Play();
            //_seeEnemies.enabled = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.55f);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.CompareTag("GroundEnemy"))
                {
                    hitCollider.GetComponent<Health>().ReceiveDamage(_damage);
                }
                //hitCollider.SendMessage("AddDamage");
            }
            //gameObject.SetActive(false);
            Destroy(this.gameObject, 2);
        }

        else if((other.CompareTag("Ground") || other.CompareTag("GroundEnemy")) && _isFire == true)
        {
            _hitGroundEffect.Play();
            _hitAudio.Play();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.55f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("GroundEnemy"))
                {
                    hitCollider.GetComponent<Health>().ReceiveDamage(_damage);
                }
                //hitCollider.SendMessage("AddDamage");
            }

            FireDamage();
            _shotRender.SetActive(false);
            _fireMortarParticle.Play();
            _shotRB.isKinematic = true;
        }

        else if (other.CompareTag("Ground") && _isMine == true)
        {
            _hitGroundEffect.Play();
            _hitAudio.clip = _thump;
            _hitAudio.Play();
            _shotRB.isKinematic = true;
            _shotRender.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void FireDamage()
    {
        if(_fireRoutine <= 3.7f)
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8.55f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("GroundEnemy"))
                {
                    hitCollider.GetComponent<Health>().ReceiveDamage(10);
                }
                //hitCollider.SendMessage("AddDamage");
            }
            if(_fireRoutine >= .3f)
            {
                _hitAudio.Stop();
                _hitAudio.clip = _fire;
                _hitAudio.Play();
            }
            if(_fireRoutine >= 2.6f)
            {
                _fireMortarParticle.Stop();
            }
            _fireRoutine += .2f;
            Invoke("FireDamage", 3.7f);

        }
        else
        {
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

    }

}
