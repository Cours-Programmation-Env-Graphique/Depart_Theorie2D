using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Enveloppe l'asset d'actions : le reste du jeu ne lit jamais le clavier ni la manette directement.
/// </summary>
// GameInput s'initialise avant les autres scripts : ses actions existent
// déjà quand les autres s'y inscrivent dans leur OnEnable (cours 9)
[DefaultExecutionOrder(-100)]
public class GameInput : MonoBehaviour
{
    /// <summary>
    /// Déclenché quand le bouton de tir est enfoncé.
    /// </summary>
    public event Action FirePressed;

    /// <summary>
    /// Déclenché quand le bouton de tir est relâché.
    /// </summary>
    public event Action FireReleased;

    /// <summary>
    /// Déclenché à chaque appui sur le bouton de pause.
    /// </summary>
    public event Action PausePressed;

    /// <summary>
    /// Déclenché à chaque appui sur le bouton de sourdine (projet).
    /// </summary>
    public event Action MutePressed;

    private SpaceInputActions _inputActions;

    // Awake et non Start : le joueur interroge GameInput dès son premier Update
    private void Awake()
    {
        _inputActions = new SpaceInputActions();
        _inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        _inputActions.Player.Fire.performed += HandleFirePerformed;
        _inputActions.Player.Fire.canceled += HandleFireCanceled;
        _inputActions.Player.Pause.performed += HandlePausePerformed;
        _inputActions.Player.Mute.performed += HandleMutePerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Fire.performed -= HandleFirePerformed;
        _inputActions.Player.Fire.canceled -= HandleFireCanceled;
        _inputActions.Player.Pause.performed -= HandlePausePerformed;
        _inputActions.Player.Mute.performed -= HandleMutePerformed;
    }

    // L'Input System alloue des ressources natives à libérer explicitement
    private void OnDestroy()
    {
        _inputActions.Player.Disable();
        _inputActions.Dispose();
    }

    /// <summary>
    /// Direction voulue par le joueur, normalisée.
    /// </summary>
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    private void HandleFirePerformed(InputAction.CallbackContext context)
    {
        FirePressed?.Invoke();
    }

    // canceled : le bouton est relâché
    private void HandleFireCanceled(InputAction.CallbackContext context)
    {
        FireReleased?.Invoke();
    }

    private void HandlePausePerformed(InputAction.CallbackContext context)
    {
        PausePressed?.Invoke();
    }

    private void HandleMutePerformed(InputAction.CallbackContext context)
    {
        MutePressed?.Invoke();
    }
}
