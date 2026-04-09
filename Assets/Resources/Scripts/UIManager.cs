using UnityEngine;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    /// Variables

    // Text that displays the number of lives left for the player.
    [SerializeField] private TextMeshProUGUI LivesText;

    // Text that displays the completion status of the round.
    [SerializeField] TextMeshProUGUI CompletionText;

    /// Methods

    /*
     */
    private void Start()
    {
        CompletionText.SetText("Press space to start. W/S or arrow keys to move up and down.");
    }

    /* Update the text that displays the number of lives left for the player.
     * @param lives - The number of lives left for the player.
     */
    public void UpdateLivesText(int lives)
    {
        LivesText.SetText("Lives: {0}", lives);
    }

    /* Subscribe and unsubscribe to the events for round completion.
     */
    private void OnEnable()
    {
        RoundManager.OnRoundComplete += DisplayCompletionText;
    }
    private void OnDisable()
    {
        RoundManager.OnRoundComplete -= DisplayCompletionText;
    }

    /* Enable completion text and display whether the player won or lost the round.
     */
    private void DisplayCompletionText(bool win)
    {
        CompletionText.gameObject.SetActive(true);
        CompletionText.SetText((win ? "You Win!" : "You Lose!") + "\n Press space to restart.");
    }

    /* Update lives text and disable completion text for the start of a new round.
     */
    public void ResetTextForRound(int lives)
    {
        UpdateLivesText(lives);
        CompletionText.gameObject.SetActive(false);
    }
}
