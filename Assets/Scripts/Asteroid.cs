using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    GameObject _self;
    SpawnManager _spawnManager;
    SoundFX _soundFXSource;
    [SerializeField] GameObject _explosionFX;
    float _speed = 30f;

    private void Start()
    {
        _self = gameObject;
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _soundFXSource = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SoundFX>();

        if (_explosionFX == null)
        {
            Debug.LogError("Explosion FX prefab is null");
        }

        if (_soundFXSource == null)
        {
            Debug.LogError("SoundFX Source on Asteroid is null");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.back * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerWeapon")
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject, 1f);
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            _soundFXSource.ExplosionAudio();
            _spawnManager.NewGameSM();
            Destroy(_self, .5f);
        }
    }
}
