using UnityEngine;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    /// Variables
   
    // Array that references all remaining blocks.
    public List<GameObject> Blocks = new List<GameObject>();

    // Lives left for the player before they lose the game.
    private int Lives = 3;

    // Reference to the ball game object.
    [SerializeField] private GameObject Ball;

    // Reference to the UI manager.
    [SerializeField] private UIManager UIManager;

    /// Methods

    /* Find all block breaking component then gets their game objects and adds them to a list. Updates lives text on the UI.
     */
    private void Start()
    {
        BlockBreaking[] TempList = FindObjectsByType<BlockBreaking>(FindObjectsSortMode.None);
        foreach (BlockBreaking TempItem in TempList)
        {
            Blocks.Add(TempItem.gameObject);
        }

        UIManager.UpdateLivesText(Lives);
    }

    /* Remove destroued block from list of blocks
     */
    private void RemoveBlock(GameObject destroyedBlock)
    {
        Blocks.Remove(destroyedBlock);
        if (Blocks.Count == 0)
        {
            Debug.Log("Round Complete");
        }
    }

    /* Subscribe and unsubscribe to the events for game play.
     */
    private void OnEnable()
    {
        BallCollision.OnBallExitArea += SubtractLife;
        BlockBreaking.OnBlockDestroyed += RemoveBlock;
    }
    private void OnDisable()
    {
        BallCollision.OnBallExitArea -= SubtractLife;
        BlockBreaking.OnBlockDestroyed -= RemoveBlock;
    }

    /* Subtract a life and check if the player has lost the game.
     */
    private void SubtractLife()
    {
        Lives--;
        UIManager.UpdateLivesText(Lives);

        if (Lives <= 0)
        {
            Ball.SetActive(false);
            LoseGame();
        }
    }

    /* Log that the game is over.
     */
    private void LoseGame()
    {         
        Debug.Log("Game Over");
    }
}
