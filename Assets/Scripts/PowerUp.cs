using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float _speed = 3f;
    [SerializeField] int _powerupID;
    GameObject _self;

    void Start()
    {
        _self = gameObject;
    }

    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            switch (_powerupID)
            {
                case 0:
                    player.ActivateTripleShot();
                    Destroy(_self);
                    break;
                case 1:
                    player.ActivateSpeedBoost();
                    Destroy(_self);
                    break;
                case 2:
                    player.ActivateShields();
                    Destroy(_self);
                    break;
            }
        }
    }
    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.15f)
        {
            Destroy(_self);
        }
    }
}