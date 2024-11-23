using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemies;
    [SerializeField]
    private List<GameObject> _ground = new();
    [SerializeField]
    private List<GameObject> _flying = new();
    [SerializeField]
    private float _timer = 25f;
    [SerializeField]
    private int _enemyCount = 10;
    [SerializeField]
    private TextMeshProUGUI _showTimer;
    [SerializeField]
    private Transform _base;
    [SerializeField]
    private EnemySpawner _thisMotherFucker;

    private float[] _enemyHeight = {-3.88f, 5f};
    private int _leftRightSpawn = 250;
    private float mintime = 2;
    private float maxtime = 3;
    private bool _changesides;
    private bool _isCounting;
    private int _airWaveCompansation = 2;

    public int _nextWave;

    // Primeira onda devera ser apenas um inimgo da direita, segunda um da direita e outro da esquerda
    //Ondas subsequentes deverão iniciar com 5 inimigos de cada lado e depois onda anterior * 2
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[0], new Vector3(transform.position.x, _enemyHeight[0], transform.position.z), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>()._spawner = _thisMotherFucker;
            _nextWave--;
            _ground.Add(enemy);
            enemy.SetActive(false);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[1], new Vector3(transform.position.x, _enemyHeight[1], transform.position.z), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>()._spawner = _thisMotherFucker;
            _nextWave--;
            _flying.Add(enemy);
            enemy.SetActive(false);
        }
        _nextWave = 0;
    }

    private void Update()
    {
        /*if (_timer <= 0 && _nextWave <= 0)
        {
            Spawn();
        }
        if(_nextWave > 0)
        {
            _timer = 0;
        }
        int minutes = Mathf.FloorToInt(_timer / 60.0f);
        int seconds = Mathf.FloorToInt(_timer - minutes * 60);
        _showTimer.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);*/

        if (_isCounting)
        {
            _timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timer / 60.0f);
            int seconds = Mathf.FloorToInt(_timer - minutes * 60);
            _showTimer.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (!_isCounting)
        {
            _timer = 0;
            int minutes = Mathf.FloorToInt(_timer / 60.0f);
            int seconds = Mathf.FloorToInt(_timer - minutes * 60);
            _showTimer.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if(_nextWave <= 0 && _timer <= 0)
        {
            Spawn();
        }
    }

    public void CanCall()
    {
        if(_nextWave <= 0)
        {
            _timer = 30;
            _isCounting = true;
        }
    }

    private void Spawn()
    {
            for(int i = 0; i < _enemyCount; i++)
            {
                if(_changesides == true)
                {
                    _leftRightSpawn = -250;
                    float timer = Random.Range(mintime, maxtime);
                    StartCoroutine(SpawnGround(timer, _leftRightSpawn));
                    if(_enemyCount >= 6)
                    {
                        StartCoroutine(SpawnAir(timer, _leftRightSpawn));
                        _airWaveCompansation = 1;
                    }
                }
                else
                {
                    _leftRightSpawn = 250;
                    float timer = Random.Range(mintime, maxtime);
                    StartCoroutine(SpawnGround(timer, _leftRightSpawn));
                    if (_enemyCount >= 6)
                    {
                        StartCoroutine(SpawnAir(timer, _leftRightSpawn));
                        _airWaveCompansation = 1;
                    }
                }
                if(maxtime <= 5)
                {
                    mintime *= 1.2f;
                    maxtime *= 1.2f;
                }
                _changesides = !_changesides;
            }
            _enemyCount *= 2;
            _nextWave = _enemyCount/_airWaveCompansation;
            _isCounting = false;
            //if(_enemyCount >= 10)
            //{
            //    _enemyCount = 10;
            //}
        //if(_enemyCount < 10)
        //{
        //}
        //else
        //{
        //    for(int i = 0; i < _enemyCount; i++)
        //    {
        //        if(_changesides == true)
        //        {
        //            _leftRightSpawn = -250;
        //            float timer = Random.Range(mintime, maxtime);
        //            StartCoroutine(SpawnGround(timer, _leftRightSpawn));
        //            StartCoroutine(SpawnAir(timer, _leftRightSpawn));
        //        }
        //        else
        //        {
        //            _leftRightSpawn = 250;
        //            float timer = Random.Range(mintime, maxtime);
        //            StartCoroutine(SpawnGround(timer, _leftRightSpawn));
        //            StartCoroutine(SpawnAir(timer, _leftRightSpawn));
        //        }
        //        mintime *= 1.2f;
        //        maxtime *= 1.2f;
        //        _changesides = !_changesides;
        //    }
        //    _enemyCount *= 2;
        //    _nextWave = _enemyCount/2;
        //    _isCounting = false;
        //}
    }

    IEnumerator SpawnGround(float waitTime, int side)
    {
        yield return new WaitForSeconds(waitTime);
            GameObject groundEnemy = GetGroundEnemies();
            if(groundEnemy != null)
            {
                int spawnPositionVariation = Random.Range(side - 20, side + 20);
                groundEnemy.transform.position = new Vector3(spawnPositionVariation, _enemyHeight[0], transform.position.z);
                groundEnemy.GetComponent<Health>()._health = 100;
                EnemyBehaviour enemy = groundEnemy.GetComponent<EnemyBehaviour>();
                enemy._basePos = _base;
                enemy._spawner = _thisMotherFucker;
                //groundEnemy.GetComponent<EnemyBehaviour>()._basePos = _base;
                groundEnemy.SetActive(true);
            }
            else
            {
                int spawnPositionVariation = Random.Range(side - 20, side + 20);
                GameObject enemy = Instantiate(_enemies[0],new Vector3(spawnPositionVariation, _enemyHeight[0], transform.position.z) , Quaternion.identity);
                enemy.GetComponent<EnemyBehaviour>()._spawner = _thisMotherFucker;
                _ground.Add(enemy);
                enemy.SetActive(false);
            _nextWave++;
                StartCoroutine(SpawnGround(waitTime, side));
            }
    }

    IEnumerator SpawnAir(float waitTime, int side)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject AirEnemy = GetFlyingEnemies();
        if (AirEnemy != null)
        {
            int spawnPositionVariation = Random.Range(side - 20, side + 20);
            AirEnemy.transform.position = new Vector3(spawnPositionVariation, _enemyHeight[1], transform.position.z);
            AirEnemy.GetComponent<Health>()._health = 100;
            EnemyBehaviour enemy = AirEnemy.GetComponent<EnemyBehaviour>();
            enemy._basePos = _base;
            enemy._spawner = _thisMotherFucker;
            //AirEnemy.GetComponent<EnemyBehaviour>()._basePos = _base;
            AirEnemy.SetActive(true);
        }
        else
        {
            int spawnPositionVariation = Random.Range(side - 20, side + 20);
            GameObject enemy = Instantiate(_enemies[1], new Vector3(spawnPositionVariation, _enemyHeight[1], transform.position.z), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>()._spawner = _thisMotherFucker;
            _flying.Add(enemy);
            enemy.SetActive(false);
            _nextWave++;
            StartCoroutine(SpawnAir(waitTime, side));
        }
    }

    public GameObject GetGroundEnemies()
    {
        for (int i = 0; i < _ground.Count; i++)
        {
            if (!_ground[i].activeInHierarchy)
            {
                return _ground[i];
            }
        }
        return null;
    }

    public GameObject GetFlyingEnemies()
    {
        for (int i = 0; i < _flying.Count; i++)
        {
            if (!_flying[i].activeInHierarchy)
            {
                return _flying[i];
            }
        }
        return null;
    }
}
