using UnityEngine;

public class Move : MonoBehaviour
{
    public enum Direction{ Forward, Backward }
    private float _speed;
    
    [Header("Direction")]
    public Direction direction;

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        _speed = GameManager.SharedInstance.GetGameVelocity();
        switch (direction)
        {
            case Direction.Forward:
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
                break;
            case Direction.Backward:
                transform.Translate(Vector3.back * _speed * Time.deltaTime);
                break;
        }
    }
}