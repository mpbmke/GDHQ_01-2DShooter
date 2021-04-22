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

    void Start()
    {
        _self = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player component is NULL");
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
            Destroy(_self);
        }
        else if (other.transform.tag == "PlayerWeapon")
        {
            _player.AddScore(10);
            Destroy(other.gameObject);
            Destroy(_self);
        }
    }
}
