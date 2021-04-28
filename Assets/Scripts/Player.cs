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
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject[] _damageAnims;
    [SerializeField] float _laserSpawnOffset;
    [SerializeField] float _reloadRate = .15f;

    GameObject _self;
    SpawnManager _spawnManager;
    UIManager _uiManager;
    GameManager _gameManager;
    SoundFX _soundFXSource;


    bool _canFire = true;
    [SerializeField] int _score;
    
    //Power-Ups
    [SerializeField] bool _tripleShotActive = false;
    [SerializeField] float _tripleShotDuration = 5f;
    
    [SerializeField] float _speedMultiplier = 1.5f;
    [SerializeField] float _speedBoostDuration = 5f;

    [SerializeField] GameObject _shield;
    [SerializeField] bool _shieldsActive = false;

    void Start()
    {
        _self = gameObject;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _soundFXSource = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SoundFX>();

        transform.position = new Vector3(0, 0, 0);

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }

        if (_damageAnims == null)
        {
            Debug.LogError("Damage Animation/s is/are null");
        }

        if (_soundFXSource == null)
        {
            Debug.LogError("SoundFX Source on Player is null");
        }

        _uiManager.UpdateLives(_playerLives);

        foreach (var anim in _damageAnims)
        {
            anim.SetActive(false);
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
            spawnPos = new Vector3(transform.position.x, transform.position.y + _laserSpawnOffset, transform.position.z);

            if (_tripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, spawnPos, Quaternion.identity);
            }
            else if (_tripleShotActive == false)
            {
                Instantiate(_laserPrefab, spawnPos, Quaternion.identity);
            }

            _soundFXSource.LaserAudio();
            _canFire = false;
            StartCoroutine(ReloadTimer());
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
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
        if (_shieldsActive == true)
        {
            _shieldsActive = false;

            if (_shield != null)
            {
                _shield.SetActive(false);
            }
            else
            {
                Debug.LogError("Shield GameObject is NULL");
            }
        }
        else if (_shieldsActive == false)
        {
            _soundFXSource.ExplosionAudio();
            _playerLives--;
            _uiManager.UpdateLives(_playerLives);

            switch (_playerLives)
            {
                case 3:
                    foreach (var anim in _damageAnims)
                    {
                        anim.SetActive(false);
                    }
                    break;
                case 2:
                    _damageAnims[0].SetActive(true);
                    break;
                case 1:
                    _damageAnims[1].SetActive(true);
                    break;
                case 0:
                    _damageAnims[2].SetActive(true);
                    break;
            }
            
            if (_playerLives < 0)
            {
                _spawnManager.PlayerKilled();
                _gameManager.GameOver();
                _self.SetActive(false);
            }
        }
    }

    public void ActivateShields()
    {
        _shieldsActive = true;

        if (_shield != null)
        {
            _shield.SetActive(true);
        }
        else
        {
            Debug.LogError("Shield GameObject is NULL");
        }
    }
    
    public void ActivateSpeedBoost()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(DeactivateSpeedBoost());
    }

    IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(_speedBoostDuration);
        _speed /= _speedMultiplier;
    }
    public void ActivateTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(_tripleShotDuration);
        _tripleShotActive = false;
    }
}