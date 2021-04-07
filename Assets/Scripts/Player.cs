using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _upperBound = 0f;
    [SerializeField] private float _lowerBound = -4f;
    [SerializeField] private float _rightBound = 11.3f;
    [SerializeField] private float _leftBound = -11.3f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CalculateMovement();
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
}