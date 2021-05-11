using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 4f;
    [SerializeField] GameObject _enemyLaser;

    float _topBound = 7.5f;
    float _bottomBound = -5.45f;

    GameObject _self;
    Player _player;
    Animator _anim;
    SoundFX _soundFXSource;

    IEnumerator enemyFireCoroutine;


    void Start()
    {
        _self = gameObject;
        _anim = _self.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _soundFXSource = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SoundFX>();


        if (_player == null)
        {
            Debug.LogError("Player component is NULL");
        }

        if (_anim == null)
        {
            Debug.LogError("Enemy Animator component is NULL");
        }

        if (_soundFXSource == null)
        {
            Debug.LogError("SoundFX Source on Enemy is null");
        }

        if (_enemyLaser == null)
        {
            Debug.LogError("Enemy Laser prefab is null");
        }

        enemyFireCoroutine = EnemyFire();
        StartCoroutine(enemyFireCoroutine);
    }
    
    IEnumerator EnemyFire()
    {
        var fireInterval = Random.Range(.5f, 2.5f);

        yield return new WaitForSeconds(fireInterval);
        Instantiate(_enemyLaser, transform.position, Quaternion.Euler(0, 0, 180));
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime, Space.World);

        if (transform.position.y <= _bottomBound)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, _topBound, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StopCoroutine(enemyFireCoroutine);

        if (other.transform.tag == "Player")
        {
            _player.PlayerDamage();
            _enemySpeed = 1.5f;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(_self, 2.8f);
        }
        else if (other.transform.tag == "PlayerWeapon")
        {
            _player.AddScore(10);
            _enemySpeed = 1.5f;
            _anim.SetTrigger("OnEnemyDeath");
            _soundFXSource.ExplosionAudio();
            GetComponent<Collider2D>().enabled = false;
            Destroy(other.gameObject);
            Destroy(_self, 2.8f);
        }
    }
}
