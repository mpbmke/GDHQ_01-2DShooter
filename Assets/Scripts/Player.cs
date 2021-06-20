using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    GameObject _self;
    SpawnManager _spawnManager;
    UIManager _uiManager;
    GameManager _gameManager;
    SoundFX _soundFXSource;
    
    //Movement Bounds
    private float _upperBound = 3.5f;
    private float _lowerBound = -4f;
    private float _rightBound = 11.3f;
    private float _leftBound = -11.3f;

    //Prefab Assignments
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _homingMissilePrefab;
    [SerializeField] GameObject[] _damageAnims;
    [SerializeField] GameObject _shield;
    [SerializeField] float _laserSpawnOffset;
    [SerializeField] float _missleSpawnOffset;
    
    float _reloadRate = .15f;
    float _speed = 5f;
    float _thrustMultiplier = 1.5f;
    int _playerLives = 3;
    int _ammoCount = 15;
    int _maxAmmo = 15;
    int _missileAmmo = 0;
    bool _canFire = true;
    int _score;
    
    //Power-Ups
    bool _tripleShotActive = false;
    float _tripleShotDuration = 5f;
    float _speedBoostMultiplier = 1.5f;
    float _speedBoostDuration = 5f;
    bool _shieldsActive = false;
    bool _heatSeekingActive = false;

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
            if (_heatSeekingActive == false)
            {
                if (_ammoCount > 0)
                {
                    Vector3 laserSpawnPos;
                    laserSpawnPos = new Vector3(transform.position.x, transform.position.y + _laserSpawnOffset, transform.position.z);

                    if (_tripleShotActive == true)
                    {
                        Instantiate(_tripleShotPrefab, laserSpawnPos, Quaternion.identity);
                    }
                    else if (_tripleShotActive == false)
                    {
                        Instantiate(_laserPrefab, laserSpawnPos, Quaternion.identity);
                    }

                    _soundFXSource.LaserAudio();
                    _canFire = false;
                    _ammoCount--;
                    StartCoroutine(ReloadTimer());

                    if (_ammoCount < 0)
                    {
                        _ammoCount = 0;
                    }
                }
                else if (_ammoCount <= 0)
                {
                    _soundFXSource.NoAmmo();
                }
            }
            else if (_heatSeekingActive == true)
            {
                if (_missileAmmo > 0)
                {
                    Vector3 missileSpawnPos;
                    missileSpawnPos = new Vector3(transform.position.x, transform.position.y + _missleSpawnOffset, transform.position.z);
                    Instantiate(_homingMissilePrefab, missileSpawnPos, Quaternion.identity);
                    _missileAmmo--;
                }
                if (_missileAmmo <= 0)
                {
                    _heatSeekingActive = false;
                }
            }
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * _speed * _thrustMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

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
        StartCoroutine(Camera.main.GetComponent<CameraBehavior>().CameraShake());

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
            PlayerDamageState();

            if (_playerLives < 0)
            {
                _spawnManager.PlayerKilled();
                _gameManager.GameOver();
                _self.SetActive(false);
            }
        }
    }

    private void PlayerDamageState()
    {
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
    }

    public void AddAmmo()
    {
        if (_ammoCount < _maxAmmo)
        {
            _ammoCount += 5;
        }
        if (_ammoCount > _maxAmmo)
        {
            _ammoCount = _maxAmmo; 
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
        _speed *= _speedBoostMultiplier;
        StartCoroutine(DeactivateSpeedBoost());
    }

    IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(_speedBoostDuration);
        _speed /= _speedBoostMultiplier;
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

    public void ActivateRepair()
    {
        if (_playerLives < 3)
        {
            _playerLives ++;
            _uiManager.UpdateLives(_playerLives);
            PlayerDamageState();
        }
    }

    public void ActivateHeatSeeking()
    {
        _missileAmmo = 3;
        _heatSeekingActive = true;
    }
}