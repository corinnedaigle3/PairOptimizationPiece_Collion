using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// Variables
    // Movement speed multiplier
    [SerializeField] private float moveSpeed = 5f;

    // Input value for up down movement
    private float inputValue;

    // Reference to player input component for reading input actions.
    public PlayerInput PlayerInput;

    private InputAction moveAction;

    /// Methods
    /* Get reference to player input component.
     */
    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        moveAction = PlayerInput.actions["UpDownMovement"];
    }

    /* Subscribe and unsubscribe to the input action for up down movement.
     */
    private void OnEnable()
    {
        moveAction.performed += OnUpDownMovement;
        moveAction.canceled += OnUpDownMovement;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnUpDownMovement;
        moveAction.canceled += OnUpDownMovement;
    }

    /* Read and stores in the input value read from the input action.
     * @param context - The context of the input action that contains information about it.
     */
    public void OnUpDownMovement(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<float>();
    }

    /* Moves player based on input value, speed, and delta time.
     */
    private void FixedUpdate()
    {
        float multiplier = moveSpeed * Time.deltaTime;
        transform.Translate(new Vector2(0, inputValue * multiplier));
    }
}
