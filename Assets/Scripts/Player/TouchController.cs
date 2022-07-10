using UnityEngine;

public class TouchController : MonoBehaviour
{
    private float _screenWidth;

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        _screenWidth = Screen.width;
        Debug.Log(_screenWidth);
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > _screenWidth / 2)
            {
                player.GetComponent<PlayerController>().MoveSides(1.0f);
            }
            if (Input.GetTouch(i).position.x < _screenWidth / 2)
            {
                player.GetComponent<PlayerController>().MoveSides(-1.0f);
            }
            ++i;
        }
        
    }
}
