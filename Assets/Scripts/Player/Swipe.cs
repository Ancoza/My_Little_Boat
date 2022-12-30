using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        // Record initial touch position.
                        startTouchPosition = touch.position;
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        // Determine direction by comparing the current touch position with the initial one
                        Vector2 direction = touch.position - startTouchPosition;
                        if (direction.x > 0)
                        {
                            _playerController.AnimRight();
                        }
                        else if (direction.x < 0)
                        {
                            _playerController.AnimLeft();
                        }

                        break;

                    case TouchPhase.Ended:
                        // Report that the touch has ended when it ends

                        Vector2 directionf = touch.position - startTouchPosition;
                        if (directionf.x > 0)
                        {
                            _playerController.MoveRight();
                        }
                        else if (directionf.x < 0)
                        {
                            _playerController.MoveLeft();
                        }

                        break;
                }
            }
    }
}

