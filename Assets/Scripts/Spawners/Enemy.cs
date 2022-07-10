using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audio;
    private GameObject _model;

    public GameObject fxExplosion;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audio = _audioSource.clip;
        _model = gameObject.transform.GetChild(0).gameObject;
        _model.transform.rotation = Random.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audio);
            Instantiate(fxExplosion, transform, false);
        }
    }
}
