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
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }


    GameObject collisionObject = null;
    int blockedCounter = 0;
    int collisionCounter = 0;
    ContactPoint2D cachedPosition = new ContactPoint2D();
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2(Random.Range(0, factor), Random.Range(0, factor));
        if (hasStarted)
        {
            var sound = sounds[Random.Range(0, sounds.Length - 1)];
            audioSource.PlayOneShot(sound);
            Debug.Log(ballRigidBody.velocity);


            var currentVelocity = ballRigidBody.velocity;
            var x = Mathf.Clamp(Mathf.Abs(currentVelocity.x), 2f, 20f);
            var y = Mathf.Clamp(Mathf.Abs(currentVelocity.y), 2f, 20f);
            currentVelocity.x = currentVelocity.x > 0 ? x : x * -1;
            currentVelocity.y = currentVelocity.y > 0 ? y : y * -1;
            ballRigidBody.velocity = currentVelocity;

            Debug.Log(ballRigidBody.velocity);
            Debug.Log("---");
        }
    }
}
