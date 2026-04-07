using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// Variables

    // Text that displays the number of lives left for the player.
    [SerializeField] TextMeshProUGUI LivesText;

    // Text that displays the completion status of the round.
    [SerializeField] TextMeshProUGUI CompletionText;

    /// Methods

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

    /* Set the completion text to display whether the player won or lost the round.
     */
    private void DisplayCompletionText(bool win)
    {
        CompletionText.SetText(win ? "You Win!" : "You Lose!");
    }
}
