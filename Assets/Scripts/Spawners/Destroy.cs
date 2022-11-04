using UnityEngine;

[RequireComponent(typeof(Move))]
public class Destroy : MonoBehaviour
{
    private Move move;
    
    [Header("Destroy")] 
    [SerializeField][Range(-20,20)]
    float pointDestruction;
    
    private void Start()
    {
        move = GetComponent<Move>();
    }

    private void Update()
    {
        DestroyObject();
    }
    
    void DestroyObject()
    {
        if (transform.position.z >= pointDestruction && move.direction == Move.Direction.Forward)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z <= pointDestruction && move.direction == Move.Direction.Backward)
        {
            Destroy(gameObject);
        }
    }
}
