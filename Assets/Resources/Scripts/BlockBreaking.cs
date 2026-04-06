using UnityEngine;

public delegate void OnBlockDestroyed(GameObject destroyedBlock);
public class BlockBreaking : MonoBehaviour
{
    /// Variables
    // Unity event for when block is destroyed
    public OnBlockDestroyed onBlockDestroyed;

    /// Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onBlockDestroyed?.Invoke(this.gameObject);
    }
}
