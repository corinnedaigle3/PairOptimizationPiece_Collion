using UnityEngine;

public class BallCollision : MonoBehaviour
{
    /// Methods
    /*
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 directionPaddle = contact.point - (Vector2)collision.gameObject.transform.position; // Direction from the center of the paddle to the contact point
            Debug.Log($"Direction Paddle: {directionPaddle}");
            Vector2 directionBall = GetComponent<Rigidbody2D>().linearVelocity.normalized - contact.point; // Direction from the center of the ball to the contact point
            Debug.Log($"Direction Ball: {directionBall}");
            Vector2 direction = (directionPaddle.normalized + directionBall.normalized) / 2;
            Debug.Log($"Direction: {direction}");
            GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * GetComponent<Rigidbody2D>().linearVelocity.magnitude;
            Debug.Log($"New Velocity: {GetComponent<Rigidbody2D>().linearVelocity.magnitude}");
        }
    }

    /*
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AliveZone"))
        {
            // Reset the ball's position and velocity
            transform.position = Vector2.zero;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
