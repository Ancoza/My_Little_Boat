using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audio;
    private GameObject _model;

    public GameObject _fxExplosion;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audio = _audioSource.clip;
        _model = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audio);
            
            //_model.SetActive(false);
            GameObject fx = Instantiate(_fxExplosion, transform, false);
        }
    }
}
