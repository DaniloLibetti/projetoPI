using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemies;
    private float[] _enemyHeight = {-3.88f, 5f};
    [SerializeField]
    private List<GameObject> _ground = new();
    [SerializeField]
    private List<GameObject> _flying = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[0], new Vector3(transform.position.x, _enemyHeight[0], transform.position.z), Quaternion.identity);
            enemy.SetActive(false);
            _ground.Add(enemy);
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(_enemies[1], new Vector3(transform.position.x, _enemyHeight[1], transform.position.z), Quaternion.identity);
            enemy.SetActive(false);
            _flying.Add(enemy);
        }
        Spawn();
    }

    private void Spawn()
    {
        //chooses between flying or grounded
        int correctEnemy = Random.Range(0, 2);

        //verifies pool for grounded enemies, instantiates if they are not found in list
        if(correctEnemy == 0)
        {
            GameObject groundEnemy = GetGroundEnemies();
            if(groundEnemy != null)
            {
                groundEnemy.transform.position = new Vector3(transform.position.x, _enemyHeight[0], transform.position.z);
                groundEnemy.SetActive(true);
            }
            else
            {
                GameObject enemy = Instantiate(_enemies[0],new Vector3(transform.position.x, _enemyHeight[0], transform.position.z) , Quaternion.identity);
                enemy.SetActive(false);
                _ground.Add(enemy);
            }
        }
        //verifies pool for flying enemies, instantiates if they are not found
        else if(correctEnemy == 1)
        {
            GameObject groundEnemy = GetFlyingEnemies();
            if (groundEnemy != null)
            {
                groundEnemy.transform.position = new Vector3(transform.position.x, _enemyHeight[1], transform.position.z);
                groundEnemy.SetActive(true);
            }
            else
            {
                GameObject enemy = Instantiate(_enemies[1], new Vector3(transform.position.x, _enemyHeight[1], transform.position.z), Quaternion.identity);
                enemy.SetActive(false);
                _flying.Add(enemy);
            }
        }

        float time = Random.Range(1, 3);
        Invoke("Spawn", time);
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
