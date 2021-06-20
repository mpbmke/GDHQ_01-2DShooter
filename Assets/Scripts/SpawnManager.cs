using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject[] _powerUps;
    [SerializeField] float _spawnHeight;
    [SerializeField] float _spawnLeftBound;
    [SerializeField] float _spawnRightBound;

    [SerializeField] bool _stopSpawning = false;

    public void NewGameSM()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);
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
        yield return new WaitForSeconds(0.5f);
        while (_stopSpawning == false)
        {
            var spawnPos = new Vector3(Random.Range(_spawnLeftBound, _spawnRightBound), _spawnHeight, 0);
            var interval = Random.Range(3f, 5f);
            var randomPowerUp = Random.Range(0, _powerUps.Length);

            Instantiate(_powerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

    public void PlayerKilled()
    {
        _stopSpawning = true;
    }

    public int EnemyCount()
    {
        return _enemyContainer.transform.childCount;
    }
}
