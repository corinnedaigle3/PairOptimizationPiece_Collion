using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    /// Variables
    // Array that references all remaining blocks.
    private List<GameObject> Blocks = new List<GameObject>();

    /// Methods
    /* Find all block breaking component then gets their game objects and adds them to a list.
     */
    private void Start()
    {
        BlockBreaking[] TempList = FindObjectsByType<BlockBreaking>(FindObjectsSortMode.None);
        foreach (BlockBreaking TempItem in TempList)
        {
            Blocks.Add(TempItem.gameObject);
        }
    }
}
