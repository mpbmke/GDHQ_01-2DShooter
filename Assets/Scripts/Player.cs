using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] int _playerLives = 3;

    //Movement Bounds
    private float _upperBound = 0f;
    private float _lowerBound = -4f;
    private float _rightBound = 11.3f;
    private float _leftBound = -11.3f;

    [SerializeField] GameObject _laserPrefab;
    [SerializeField] float _spawnOffset;
    [SerializeField] float _reloadRate = .15f;

    GameObject _self;
    SpawnManager _spawnManager;
    bool _canFire = true;

    void Start()
    {
        _self = gameObject;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    void Update()
    {
        CalculateMovement();
        FireWeapon();
    }

    private void FireWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire == true)
        {
            Vector3 spawnPos;

            spawnPos = new Vector3(transform.position.x, transform.position.y + _spawnOffset, transform.position.z);
            Instantiate(_laserPrefab, spawnPos, Quaternion.identity);
            _canFire = false;
            StartCoroutine(ReloadTimer());
        }
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(_reloadRate);
        _canFire = true;
        Debug.Log("Reloaded");
    }

    private void CalculateMovement()
    {
        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //Boundaries
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _lowerBound, _upperBound), transform.position.z);

        if (transform.position.x >= _rightBound)
        {
            transform.position = new Vector3(_leftBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= _leftBound)
        {
            transform.position = new Vector3(_rightBound, transform.position.y, transform.position.z);
        }
    }

    public void PlayerDamage()
    {
        _playerLives--;
        if (_playerLives <= 0)
        {
            _spawnManager.PlayerKilled();
            Destroy(_self);
        }
    }
}