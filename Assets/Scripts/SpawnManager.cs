using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] float _spawnHeight;
    [SerializeField] float _spawnLeftBound;
    [SerializeField] float _spawnRightBound;

    [SerializeField] bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            var spawnPos = new Vector3(Random.Range(_spawnLeftBound, _spawnRightBound), _spawnHeight, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }

    public void PlayerKilled()
    {
        _stopSpawning = true;
    }
}
