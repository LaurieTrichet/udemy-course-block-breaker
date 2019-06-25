using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float units = 16f;
    private float minX = 1f;
    private float maxX = 15f;

    private GameSession gameSession;
    private Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float mousePositionInUnits = GetXPos();
        float clampedPosition = Mathf.Clamp(mousePositionInUnits, minX, maxX);
        Vector2 direction = new Vector2(clampedPosition, transform.position.y);
        gameObject.transform.position = direction;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * units;
        }
    }
}
