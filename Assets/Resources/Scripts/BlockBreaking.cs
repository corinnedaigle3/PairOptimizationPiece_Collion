using UnityEngine;
using System;

public class BlockBreaking : MonoBehaviour
{
    /// Variables
    // Unity event for when block is destroyed
    public static event Action<GameObject> OnBlockDestroyed;

    /// Methods
    /* Destroy the block when it collides with the ball
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    /* Invoke block destroyyed event with the destroyed block as a parameter
     */
    public void OnDestroy()
    {
        OnBlockDestroyed?.Invoke(this.gameObject);
    }
}
