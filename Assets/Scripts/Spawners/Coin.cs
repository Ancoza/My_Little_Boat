using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audio;
    private GameObject _model;

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
            _model.SetActive(false);
        }
    }
    
}
