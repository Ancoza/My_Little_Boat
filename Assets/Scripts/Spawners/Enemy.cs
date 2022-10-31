using UnityEngine;
public class Enemy : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audio;

    public GameObject fxExplosion;
    private Spawner _spawner;

    private void Start()
    {
        _spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
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
        if (other.CompareTag("Bullet"))
        {
            _spawner.GenerateReward();
        }
    }
}
