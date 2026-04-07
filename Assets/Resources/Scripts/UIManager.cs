using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// Variables

    // Text that displays the number of lives left for the player.
    [SerializeField] TextMeshProUGUI LivesText;

    /// Methods

    /* Update the text that displays the number of lives left for the player.
     * @param lives - The number of lives left for the player.
     */
    public void UpdateLivesText(int lives)
    {
        LivesText.SetText("Lives: {0}", lives);
    }
}
