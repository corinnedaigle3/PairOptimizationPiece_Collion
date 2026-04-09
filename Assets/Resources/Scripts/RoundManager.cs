using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class RoundManager : MonoBehaviour
{
    /// Variables
   
    // Event for when round is complete, with bool for win or loss.
    public static event Action<bool> OnRoundComplete;

    // Array that references all remaining blocks.
    public List<GameObject> Blocks = new List<GameObject>();

    // Int for the number of blocks left in the round.
    private int ActiveBlocks;

    // Lives left for the player before they lose the game.
    private int Lives = 3;

    // Reference to the ball game object.
    [SerializeField] private GameObject Ball;

    // Reference to the paddle game object.
    [SerializeField] private GameObject Paddle;

    // Reference to the UI manager.
    [SerializeField] private UIManager UIManager;

    // Reference to the player input for starting a new round.
    [SerializeField] private PlayerInput PlayerInput;

    /// Methods

    /* Find all block breaking component then gets their game objects and adds them to a list. Set blocks, ball, and paddle sprite to inactive.
     * Register input action for new round start.
     */
    private void Start()
    {
        BlockBreaking[] TempList = FindObjectsByType<BlockBreaking>(FindObjectsSortMode.None);
        foreach (BlockBreaking TempItem in TempList)
        {
            Blocks.Add(TempItem.gameObject);
            TempItem.gameObject.SetActive(false);
        }
        Ball.SetActive(false);
        Paddle.GetComponent<SpriteRenderer>().enabled = false;

        PlayerInput.actions["NewRound"].performed += StartNewRound;
    }

    /* Remove destroued block from list of blocks. Invoke round complete event with true for a win if none left.
     */
    private void RemoveBlock()
    {
        ActiveBlocks--;
        if (ActiveBlocks == 0)
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

    /* Disable the blocks and paddle sprite then invokes the round complete event with false for a loss. Register input action for new round start.
     */
    private void LoseGame()
    {         
        foreach (GameObject Block in Blocks)
        {
            Block.SetActive(false);
        }
        Paddle.GetComponent<SpriteRenderer>().enabled = false;

        OnRoundComplete?.Invoke(false);
        PlayerInput.actions["NewRound"].performed += StartNewRound;
    }

    /* Set ball inactive then invokes the round complete event with true for a win. Register input action for new round start.
     */
    private void WinGame()
    {
        Ball.SetActive(false);
        OnRoundComplete?.Invoke(true);
        PlayerInput.actions["NewRound"].performed += StartNewRound;
    }

    /* Set all blocks, the paddle sprite, and the ball active. Reset the ball and paddle position, lives, and active block count. Update Lives UI
     * and completion text. Run the launch ball function to start the round. Deregister input action for new round start.
     * @param context - Unused context of the input action that contains information about it.
     */
    private void StartNewRound(InputAction.CallbackContext context)
    {
        PlayerInput.actions["NewRound"].performed -= StartNewRound;

        foreach (GameObject Block in Blocks)
        {
            Block.SetActive(true);
        }
        ActiveBlocks = Blocks.Count;

        Paddle.GetComponent<SpriteRenderer>().enabled = true;
        Paddle.transform.position = new Vector2(Paddle.transform.position.x, 0);

        Ball.SetActive(true);
        Ball.transform.position = Vector2.zero;
        
        Lives = 3;
        UIManager.ResetTextForRound(Lives);

        Ball.GetComponent<BallMovement>().RunLaunchBall();
    }
}
