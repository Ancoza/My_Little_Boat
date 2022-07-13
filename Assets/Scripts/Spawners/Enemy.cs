using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audio;

    public GameObject fxExplosion;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audio = _audioSource.clip;
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
