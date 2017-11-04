using UnityEngine;

/// <summary>
/// Laser beam + input controller
/// Mira Prism Hackathon 2017
/// </summary>
public class Laser : MonoBehaviour {

    // Variables to limit input calls from controller
    private bool clickHeld = false;
    private bool triggerHeld = false;

    // standard initializer
    void Start() {

    }

    void Update() {
        // Process any current inputs from the controller
        ProcessControllerInputs();
    }

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

    private void TouchpadPressed() {
        Debug.Log("TOUCH PAD PRESSED!");
    }

    private void TriggerPressed() {
        Debug.Log("TRIGGER PRESSED!");
    }

}