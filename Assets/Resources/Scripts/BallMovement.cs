using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    /// Variables
    // Float for ball's initial movement speed
    [SerializeField] private float moveSpeed = 5f;

    // Rigidbody2D component of the ball for setting velocity.
    private Rigidbody2D rb;

    /// Methods
    /* Method that starts the LaunchBall coroutine and sets rigidbody type.
    */
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(LaunchBall());
    }

    /* Waits for five seconds then launaches ball in direction towards player side.
     */
    private IEnumerator LaunchBall()
    {
        yield return new WaitForSeconds(3f);

        Vector2 initialDirection = new Vector2(Random.Range(-1, 0), Random.Range(-0.8f, 0.8f)).normalized;
        rb.linearVelocity = initialDirection * moveSpeed;
    }
}
