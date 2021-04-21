using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _tripleShotCollectible;
    [SerializeField] GameObject[] _powerUps;
    [SerializeField] float _spawnHeight;
    [SerializeField] float _spawnLeftBound;
    [SerializeField] float _spawnRightBound;

    [SerializeField] bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            var spawnPos = new Vector3(Random.Range(_spawnLeftBound, _spawnRightBound), _spawnHeight, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            var spawnPos = new Vector3(Random.Range(_spawnLeftBound, _spawnRightBound), _spawnHeight, 0);
            var interval = Random.Range(3f, 7f);
            var randomPowerUp = Random.Range(0, _powerUps.Length);

            Instantiate(_powerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

    public void PlayerKilled()
    {
        _stopSpawning = true;
    }
}
