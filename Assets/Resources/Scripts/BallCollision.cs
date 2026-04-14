using System;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    /// Variables
    // Rigidbody component of the ball
    private Rigidbody2D rb;
    // Unity event for when block is destroyed
    public static event Action OnBallExitArea;

    /// Methods
    /* Get rigidbody component of the ball
     */
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /* When colliding with the paddle, change the direction of the ball based on where it hit the paddle and the direction it was going before the collision.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 directionPaddle = contact.point - (Vector2)collision.gameObject.transform.position; // Direction from the center of the paddle to the contact point
            Vector2 directionBall = rb.linearVelocity.normalized; // Direction the ball is going before the collision

            //Vector2 direction = (directionPaddle.normalized + directionBall.normalized) / 2;
            Vector2 direction = (directionPaddle.normalized + directionBall) / 2; // directionBall doesn't need to be normalized twice

            rb.linearVelocity = direction.normalized * rb.linearVelocity.magnitude;
        }
    }

    /* When exiting the alive zone, invoke the onBallExitArea event and reset ball velocity and position.
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AliveZone"))
        {
            OnBallExitArea?.Invoke();
            rb.linearVelocity = Vector2.zero;
            transform.position = Vector2.zero;
        }
    }
}
