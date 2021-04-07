using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public string myName = "Hortence";
    public int myAge = 97;
    public float mySpeed = 2.3f;
    public int myHealth = 34;
    public int score = 1;
    public int ammo = 0;
    public bool hasKeys = false;
    void Start()
    {
        Debug.Log("My name is " + myName + ". I am " + myAge + " years old and I have " + ammo + " ammunition.");
        Debug.Log("Current health: " + myHealth);
        Debug.Log("Current speed: " + mySpeed);
        Debug.Log("Has all keys: " + hasKeys);
    }
}
