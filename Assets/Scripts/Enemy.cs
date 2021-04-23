using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 4f;

    float _topBound = 7.5f;
    float _bottomBound = -5.45f;

    GameObject _self;
    Player _player;
    Animator _anim;

    void Start()
    {
        _self = gameObject;
        _anim = _self.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player component is NULL");
        }

        if (_anim == null)
        {
            Debug.LogError("Enemy Animator component is NULL");
        }
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
            Destroy(other.gameObject);
            Destroy(_self, 2.8f);
        }
    }
}
