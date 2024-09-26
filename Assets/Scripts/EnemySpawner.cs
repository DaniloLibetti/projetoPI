using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemies;
    private float[] _enemyHeight = {-3.88f, 5f};

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        int correctEnemy = Random.Range(0, 2);
        GameObject enemy = Instantiate(_enemies[correctEnemy],new Vector3(transform.position.x, _enemyHeight[correctEnemy], transform.position.z) , Quaternion.identity);
        float time = Random.Range(0, 5);
        Invoke("Spawn", time);
    }
}
