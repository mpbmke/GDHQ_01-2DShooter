using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    GameObject _self;

    void Start()
    {
        _self = gameObject;
        Destroy(_self, 2.8f);
    }
}
