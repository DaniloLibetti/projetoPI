using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerInstantiations : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _turrents;
    [SerializeField]
    private Transform _instantiationTarget;
    [SerializeField]
    private GameObject _turretChoiceUi;

    public GameObject[] _turretsUpgradeUi;
    private int _upgradeUiIndex;

    public GameObject[] _upgradePrefabs;
    [SerializeField]
    private int[] _cost;

    public TextMeshProUGUI _materialCounterText;
    [SerializeField]
    private TextMeshProUGUI _shhipMaterialCounter;

    public int _materialAmount;
    private int _shipMaterial;

    private bool _turretUiOnOff = false;
    public bool _isNearShip = false;

    private Vector3 _oldTurretPosition;
    private Quaternion _oldTurretRotation;

    [SerializeField]
    private WinLoseManager _winLoseManager;

    private void Start()
    {
        UpdateMaterialCounter();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            _turretChoiceUi.SetActive(!_turretUiOnOff);
            _turretUiOnOff = !_turretUiOnOff;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeTurret();
            if(_isNearShip && _materialAmount >= 10)
            {
                _materialAmount -= 10;
                _shipMaterial += 10;
                _winLoseManager._shipMaterials += 10;
                UpdateMaterialCounter();
            }
        }
    }

    private void UpgradeTurret()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.right);
        Ray shootRay = new Ray(transform.position, rayDirection);
        Debug.DrawRay(transform.position, rayDirection * 5, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, 5))
        {
            Debug.Log(hitInfo.collider.name);
            UpgradeTowers upgrade = hitInfo.transform.GetComponent<UpgradeTowers>();
            if(upgrade != null)
            {
                if (upgrade)
                {
                    _turretsUpgradeUi[upgrade._upgradeUiIndex].SetActive(true);
                    _upgradeUiIndex = upgrade._upgradeUiIndex;
                    _oldTurretPosition = hitInfo.transform.position;
                    _oldTurretRotation = hitInfo.transform.rotation;
                    _upgradePrefabs = upgrade._nextTurrent;
                    _cost = upgrade._cost;
                    upgrade.Upgrade();
                }
            }
        }
    }

    public void ChooseUpgrade(int nextTurret)
    {
        if (_cost[nextTurret] <= _materialAmount)
        {
            Instantiate(_upgradePrefabs[nextTurret], _oldTurretPosition, _oldTurretRotation);
            _turretsUpgradeUi[_upgradeUiIndex].SetActive(false);
        }
        else
            return;
    }

    public void MortarInstatiate()
    {
        
        if(_materialAmount >= 10)
        {
            Instantiate(_turrents[0], new Vector3(_instantiationTarget.position.x, -4.366f, _instantiationTarget.position.z), transform.rotation);
            _turretChoiceUi.gameObject.SetActive(false);
            _materialAmount -= 10;
            UpdateMaterialCounter();
        }
    }

    public void TurretInstantiate()
    {
        
        if (_materialAmount >= 10)
        {
            Instantiate(_turrents[1], new Vector3(_instantiationTarget.position.x, -4.18f, _instantiationTarget.position.z), _instantiationTarget.rotation);
            _turretChoiceUi.gameObject.SetActive(false);
            _materialAmount -= 10;
            UpdateMaterialCounter();
        }
    }

    public void BarrierInstantiate()
    {
        
        if (_materialAmount >= 10)
        {
            Instantiate(_turrents[2], new Vector3(_instantiationTarget.position.x, -4.18f, _instantiationTarget.position.z), Quaternion.Euler(0, 0, 0));
            _turretChoiceUi.gameObject.SetActive(false);
            _materialAmount -= 10;
            UpdateMaterialCounter();
        }
    }

    public void UpdateMaterialCounter()
    {
        _materialCounterText.text = "Materials: " + _materialAmount;
        _shhipMaterialCounter.text = "Ship Materials: " + _shipMaterial + "/100";
    }

}
