using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 4f;

    float _topBound = 7.5f;
    float _bottomBound = -5.45f;

    GameObject _self;

    void Start()
    {
        _self = gameObject;
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
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.PlayerDamage();
                Destroy(_self);
            }
            else
            {
                Debug.LogError("Player component is null");
            }
            
        }
        else if (other.transform.tag == "PlayerWeapon")
        {
            Destroy(other.gameObject);
            Destroy(_self);
        }
    }
}
