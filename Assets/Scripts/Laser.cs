using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 12f;
    GameObject _self;

    private void Start()
    {
        _self = gameObject;
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 7f && transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else if (transform.position.y > 7f || transform.position.y < -5.5f)
        {
            Destroy(_self, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_self.tag == "Enemy" && other.tag == "Player")
        {
            other.GetComponent<Player>().PlayerDamage();
        }
        else
        {
            return;
        }
    }
}
