using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle;
    private bool hasStarted = false;

    [SerializeField] float xPush = 2.0f;
    [SerializeField] float yPush = 15.0f;
    [SerializeField] float factor = 2.0f;

    private Vector2 paddleToBallVector;

    private AudioSource audioSource;

    private Rigidbody2D ballRigidBody;

    [SerializeField] AudioClip[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
        ballRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            CheckPlayerLaunchBall();
            LockBallToPaddle();
        }
    }

    private void CheckPlayerLaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            paddle.shouldMove = true;
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            var sound = sounds[Random.Range(0, sounds.Length - 1)];
            audioSource.PlayOneShot(sound);
            PreventBallBlocking();
        }
    }

    private void PreventBallBlocking()
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, factor), Random.Range(0, factor));
        var currentVelocity = ballRigidBody.velocity;
        if (currentVelocity.x == 0)
        {
            var x = Mathf.Clamp(Mathf.Abs(currentVelocity.x), 2f, 20f);
            currentVelocity.x = x;

            Debug.Log("tweaked x: " + currentVelocity.x);
        }
        else if (currentVelocity.y == 0)
        {
            var y = Mathf.Clamp(Mathf.Abs(currentVelocity.y), 2f, 20f);
            currentVelocity.y = y;
            Debug.Log("tweaked y: " + currentVelocity.y);
        }
        ballRigidBody.velocity = currentVelocity;
    }
}
