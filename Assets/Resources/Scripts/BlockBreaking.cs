using UnityEngine;
using System;

public class BlockBreaking : MonoBehaviour
{
    /// Variables
    // Unity event for when block is destroyed
    public static event Action<GameObject> onBlockDestroyed;

    /// Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        //onBlockDestroyed?.Invoke(this.gameObject);
    }

    public void OnDestroy()
    {
        onBlockDestroyed?.Invoke(this.gameObject);
    }
}
