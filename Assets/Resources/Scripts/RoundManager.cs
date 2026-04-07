using UnityEngine;
using System.Collections.Generic;
using System;

public class RoundManager : MonoBehaviour
{
    /// Variables
   
    // Event for when round is complete, with bool for win or loss.
    public static event Action<bool> OnRoundComplete;

    // Array that references all remaining blocks.
    public List<GameObject> Blocks = new List<GameObject>();

    // Lives left for the player before they lose the game.
    private int Lives = 3;

    // Reference to the ball game object.
    [SerializeField] private GameObject Ball;

    // Reference to the paddle game object.
    [SerializeField] private GameObject Paddle;

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

    /* Remove destroued block from list of blocks. Invoke round complete event with true for a win if none left.
     */
    private void RemoveBlock(GameObject destroyedBlock)
    {
        Blocks.Remove(destroyedBlock);
        if (Blocks.Count == 0)
        {
            WinGame();
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

    /* Disable the paddle and blocks then invokes the round complete event with false for a loss.
     */
    private void LoseGame()
    {         
        foreach (GameObject TempBlock in Blocks)
        {
            TempBlock.SetActive(false);
        }
        Paddle.SetActive(false);

        OnRoundComplete?.Invoke(false);
    }

    /* Set ball inactive then invokes the round complete event with true for a win.
     */
    private void WinGame()
    {
        Ball.SetActive(false);
        OnRoundComplete?.Invoke(true);
    }
}
