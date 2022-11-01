using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject model;
    private BoxCollider bx;

    private void Start()
    {
        bx = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Instantiate(explosion,transform, false);
            model.SetActive(false);
            //bx.enabled = false;
            Destroy(gameObject,0.5f);
        }
    }
    
}
