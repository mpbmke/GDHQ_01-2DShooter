using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 8f;
    GameObject _self;

    private void Start()
    {
        _self = gameObject;
    }

    void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 7f && transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else if (transform.position.y > 7f)
        {
            Destroy(_self);
        }

        
    }
}
