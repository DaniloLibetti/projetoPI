using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject _drone;
    [SerializeField]
    private float _launchate;
    [SerializeField]
    private Transform _target1, _target2;
    [SerializeField]
    private ParticleSystem _shootEffect;

    void Start()
    {
        StartCoroutine(CallDrones());
        //Invoke("Launch", 1);
    }

    private void Launch()
    {
        _shootEffect.Play();
        GameObject drone = Instantiate(_drone, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        DroneBehaviour targets = drone.GetComponent<DroneBehaviour>();
        targets._launcher = this.GetComponent<DroneLauncher>();
        targets._target1 = _target1;
        targets._target2 = _target2;
        ///Invoke("Launch", _launchate);
    }

    IEnumerator CallDrones()
    {
        yield return new WaitForSeconds(1);
        Launch();
        yield return new WaitForSeconds(3);
        Launch();
    }

    public void Revive(GameObject drone)
    {
        StartCoroutine(Reactivate(drone));
    }

    public IEnumerator Reactivate(GameObject drone)
    {
        
        drone.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        yield return new WaitForSeconds(2);
        DroneBehaviour targets = drone.GetComponent<DroneBehaviour>();
        targets._topHeight = false;
        targets._attack = false;
        StartCoroutine(targets.DroneHeight());
        drone.SetActive(true);
    }
}
