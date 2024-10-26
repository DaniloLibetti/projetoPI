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

    public TextMeshProUGUI _materialCounterText;

    public int _materialAmount;

    private bool _turretUiOnOff = false;

    private Vector3 _oldTurretPosition;
    private Quaternion _oldTurretRotation;

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
        }
    }

    private void UpgradeTurret()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.right);
        Ray shootRay = new Ray(transform.position, rayDirection);
        Debug.DrawRay(transform.position, rayDirection * 5, Color.red, 2);

        if (Physics.Raycast(shootRay, out RaycastHit hitInfo, 5))
        {
            UpgradeTowers upgrade = hitInfo.transform.GetComponent<UpgradeTowers>();
            Debug.Log(upgrade.gameObject.name);
            if (upgrade)
            {
                _turretsUpgradeUi[upgrade._upgradeUiIndex].SetActive(true);
                _upgradeUiIndex = upgrade._upgradeUiIndex;
                _oldTurretPosition = hitInfo.transform.position;
                _oldTurretRotation = hitInfo.transform.rotation;
                _upgradePrefabs = upgrade._nextTurrent;
                upgrade.Upgrade();
            }
        }
    }

    public void ChooseUpgrade(int nextTurret)
    {
        Instantiate(_upgradePrefabs[nextTurret], _oldTurretPosition, _oldTurretRotation);
        _turretsUpgradeUi[_upgradeUiIndex].SetActive(false);
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
    }

}
