using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretUpgrade : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField]
    private GameObject _baseTurretUpgradeUi;
    [SerializeField]
    private GameObject _kineticTurretUpgradeUi;
    [SerializeField]
    private GameObject _laserTurretUpgradeUi;

    [Header("Kinectic Upgrades")]
    [SerializeField]
    private GameObject _kineticTurret;
    [SerializeField]
    private GameObject _sniperTurret;
    [SerializeField]
    private GameObject _machineGunTurret;

    [Header("Laser Upgrades")]
    [SerializeField]
    private GameObject _laserTurret;
    [SerializeField]
    private GameObject _fireTurret;
    [SerializeField]
    private GameObject _iceTurret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaserUpgrade()
    {
        GameObject upgraded = Instantiate(_laserTurret, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void FireUpgrade()
    {
        GameObject upgraded = Instantiate(_fireTurret, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void IceUpgrade()
    {
        GameObject upgraded = Instantiate(_iceTurret, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
