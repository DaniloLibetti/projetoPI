using System.Collections;
using System.Collections.Generic;
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
    private float _time = 25f;
    [SerializeField]
    private int _enemyCount = 10;

    private float[] _enemyHeight = {-3.88f, 5f};
    private int _leftRightSpawn = 250;
    private float mintime = 2;
    private float maxtime = 3;
    private bool _changesides;

    // Primeira onda devera ser apenas um inimgo da direita, segunda um da direita e outro da esquerda
    //Ondas subsequentes deverão iniciar com 5 inimigos de cada lado e depois onda anterior * 2
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[0], new Vector3(transform.position.x, _enemyHeight[0], transform.position.z), Quaternion.identity);
            enemy.SetActive(false);
            _ground.Add(enemy);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[1], new Vector3(transform.position.x, _enemyHeight[1], transform.position.z), Quaternion.identity);
            enemy.SetActive(false);
            _flying.Add(enemy);
        }
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        if(_time <= 0)
        {
            Spawn();
            _time = 60;
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
                StartCoroutine(SpawnAir(timer, _leftRightSpawn));
            }
            else
            {
                _leftRightSpawn = 250;
                float timer = Random.Range(mintime, maxtime);
                StartCoroutine(SpawnGround(timer, _leftRightSpawn));
                StartCoroutine(SpawnAir(timer, _leftRightSpawn));
            }
            mintime *= 1.2f;
            maxtime *= 1.2f;
            _changesides = !_changesides;
        }
        _enemyCount *= 2;
        //_leftRightSpawn = 250;
    }

    IEnumerator SpawnGround(float waitTime, int side)
    {
        yield return new WaitForSeconds(waitTime);
            GameObject groundEnemy = GetGroundEnemies();
            if(groundEnemy != null)
            {
                groundEnemy.transform.position = new Vector3(side, _enemyHeight[0], transform.position.z);
                groundEnemy.GetComponent<Health>()._health = 100;
                groundEnemy.SetActive(true);
            }
            else
            {
                GameObject enemy = Instantiate(_enemies[0],new Vector3(side, _enemyHeight[0], transform.position.z) , Quaternion.identity);
                enemy.SetActive(false);
                _ground.Add(enemy);
                StartCoroutine(SpawnGround(waitTime, side));
            }
    }

    IEnumerator SpawnAir(float waitTime, int side)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject AirEnemy = GetFlyingEnemies();
        if (AirEnemy != null)
        {
            AirEnemy.transform.position = new Vector3(side, _enemyHeight[1], transform.position.z);
            AirEnemy.GetComponent<Health>()._health = 100;
            AirEnemy.SetActive(true);
        }
        else
        {
            GameObject enemy = Instantiate(_enemies[1], new Vector3(side, _enemyHeight[1], transform.position.z), Quaternion.identity);
            enemy.SetActive(false);
            _flying.Add(enemy);
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
