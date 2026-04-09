using UnityEngine;
using System;
using System.Collections;

public class BlockBreaking : MonoBehaviour
{
    /// Variables
    /// 
    // Unity event for when block is destroyed
    public static event Action OnBlockDestroyed;

    /// Methods
  
    /* Disable the block when it collides with the ball
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    /* Invoke block destroyyed event with the disabled block as a parameter
     */
    private void OnDisable()
    {
        OnBlockDestroyed?.Invoke();
    }
}
