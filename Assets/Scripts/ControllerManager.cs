using UnityEngine;

/// <summary>
/// Controller input manager.
/// Mira Prism Hackathon 2017
/// </summary>
public class ControllerManager : MonoBehaviour {

    // Variables to limit input calls from controller
    private bool clickHeld = false;
    private bool triggerHeld = false;

    /// <summary>
    /// Standard monobehaviour initializer.
    /// </summary>
    void Start() {
        
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update() {
        // Process any current inputs from the controller
        ProcessControllerInputs();
    }

    /// <summary>
    /// Processes any current inputs from the controller.
    /// </summary>
    private void ProcessControllerInputs() {
        if (MiraController.ClickButtonPressed) {
            // Prevent extraneous input calls
            if (clickHeld) { return; }
            clickHeld = true;

            // do something
            TouchpadPressed();
        }
        else if (MiraController.ClickButtonReleased) {
            // Prevent extraneous input calls
            if (!clickHeld) { return; }
            clickHeld = false;
        }

        if (MiraController.TriggerButtonPressed) {
            // Prevent extraneous input calls
            if (triggerHeld) { return; }
            triggerHeld = true;

            // do something
            TriggerPressed();
        }
        else if (MiraController.TriggerButtonReleased) {
            // Prevent extraneous input calls
            if (!triggerHeld) { return; }
            triggerHeld = false;
        }
    }

    /// <summary>
    /// Called when the controller's Touchpad has been pressed or held down.
    /// </summary>
    private void TouchpadPressed() {
        Debug.Log("TOUCH PAD PRESSED!");
    }

    /// <summary>
    /// Called when the controller's Trigger has been pressed or held down.
    /// </summary>
    private void TriggerPressed() {
        Debug.Log("TRIGGER PRESSED!");
    }

}