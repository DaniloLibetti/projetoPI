using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    [SerializeField]
    private bool _isBase = false;
    [SerializeField]
    private bool _isEnemy = false;

    [SerializeField]
    private GameObject _CollectableMaterial;
    [SerializeField]
    private ParticleSystem _bloodEffect;

    public float _health = 100f;

    private CoinDrop _coinDrop;
    public void ReceiveDamage(float amount)
    {
        _health -= amount;
        if(_isBase)
        {
            healthBar.fillAmount = _health / 1000f;
        }

        if (_isEnemy)
        {
            _bloodEffect.Play();
        }
        
        if(_health <= 0 && transform.parent)
        {
            DropCoin();
            transform.parent.transform.position = new Vector3(500, 500, 500);
            //gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            //Destroy(transform.parent.gameObject);

        }
        else if(_health <= 0 && !transform.parent)
        {
            DropCoin();
            transform.position = new Vector3(500, 500, 500);
            gameObject.SetActive(false);
            //Destroy(this.gameObject);

        }
    }

    public void DropCoin()
    {
        if(this.gameObject.CompareTag("GroundEnemy") || this.gameObject.CompareTag("AirEnemy"))
        {
            GameObject mat = Instantiate(_CollectableMaterial, transform.position, Quaternion.identity);
        }
        
    }

}
