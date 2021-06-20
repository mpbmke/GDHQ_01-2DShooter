using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float _missileSpeed = 8f;
    [SerializeField] float _rotationSpeed = 150f;
    GameObject _self;
    SpawnManager _spawnManager;
    GameObject _enemyPool;

    void Start()
    {
        _self = gameObject;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _enemyPool = GameObject.Find("Enemies");

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        if (_enemyPool == null)
        {
            Debug.LogError("Enemy Pool is null");
        }
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float distanceToEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;

        Enemy[] allEnemies = _enemyPool.GetComponentsInChildren<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distance = (currentEnemy.transform.position - _self.transform.position).sqrMagnitude;

            if (currentEnemy.GetComponent<Collider2D>().enabled == true && distance < distanceToEnemy)
            {
                distanceToEnemy = distance;
                closestEnemy = currentEnemy;
            }
        }

        if (closestEnemy != null && closestEnemy.GetComponent<Collider2D>().enabled == true)
        {
            Rigidbody2D selfRB = _self.GetComponent<Rigidbody2D>();
            Vector2 direction = (Vector2)closestEnemy.GetComponent<Rigidbody2D>().position - selfRB.position;
            direction.Normalize();
            float rotationAmount = Vector3.Cross(direction, transform.up).z;

            selfRB.angularVelocity = -rotationAmount * _rotationSpeed;
            selfRB.velocity = transform.up * _missileSpeed;

            /*
            Vector3 rotateDirection = Vector3.RotateTowards(_self.transform.rotation.eulerAngles, closestEnemy.transform.rotation.eulerAngles, _missileSpeed * Time.deltaTime, 360f);

            transform.position = Vector3.MoveTowards(_self.transform.position, closestEnemy.transform.position, _missileSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(rotateDirection);
            */
        }
        else
        {
            transform.Translate(Vector3.up * _missileSpeed * Time.deltaTime);
        }
    }
}
