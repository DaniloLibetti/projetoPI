using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirHead : MonoBehaviour
{
    public Transform _enemyLocked;
    [SerializeField]
    private GameObject _missle;
    [SerializeField]
    private float _verticalShotForce = 0;
    [SerializeField]
    private float _horizontalShotForce = 200;
    [SerializeField]
    private Transform _launchPos;
    [SerializeField]
    private AntiAirBehaviour _behaviour;
    [SerializeField]
    private List<GameObject> _missles = new();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject missles = Instantiate(_missle, _launchPos.position, Quaternion.identity);
            missles.SetActive(false);
            _missles.Add(missles);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyLocked != null)
        {
            transform.LookAt(_enemyLocked);
        }
        else
            transform.rotation = Quaternion.Euler(0, 90, 90);
    }

    public void Shoot()
    {

        GameObject missle = GetMissles();
        if (missle != null)
        {
            missle.transform.position = _launchPos.position;
            missle.GetComponent<Rigidbody>().AddForce(new Vector3(_horizontalShotForce, _verticalShotForce, 0));
            AntiAirMissleBehaviour behaviour = missle.GetComponent<AntiAirMissleBehaviour>();
            behaviour._head = GetComponent<AntiAirHead>();
            behaviour._lockedTarget = _enemyLocked;
            behaviour._behaviour = _behaviour;
            missle.SetActive(true);
        }
        else
        {
            GameObject missles = Instantiate(_missle, _launchPos.position, Quaternion.identity);
            missles.SetActive(false);
            _missles.Add(missles);
        }
        if(_enemyLocked == null || !_enemyLocked.gameObject.activeSelf)
        {
            _behaviour.RemoveInactive();
        }


        //if (_enemyLocked != null && _enemyLocked.gameObject.activeSelf)
        //{
        //    GameObject missle = Instantiate(_missle, _launchPos.position, Quaternion.identity);
        //    missle.GetComponent<Rigidbody>().AddForce(new Vector3(_horizontalShotForce, _verticalShotForce, 0));
        //    AntiAirMissleBehaviour behaviour = missle.GetComponent<AntiAirMissleBehaviour>();
        //    behaviour._lockedTarget = _enemyLocked;
        //    behaviour._behaviour = _behaviour;
        //    //Invoke("Shoot", 2); //esta usando timer manual no AntiAirBehaviour
        //}
    }

    public GameObject GetMissles()
    {
        for (int i = 0; i < _missles.Count; i++)
        {
            if (!_missles[i].activeInHierarchy)
            {
                return _missles[i];
            }
        }
        return null;
    }
}
