using UnityEngine;

/// <summary>
/// Controller input manager.
/// Mira Prism Hackathon 2017
/// </summary>
public class ControllerManager : MonoBehaviour {
    
    public static ControllerManager Instance;

    // Variables to limit input calls from controller
    public bool clickHeld = false;
    public bool triggerHeld = false;

    private int UpdateCount = 0;

    /// <summary>
    /// Standard monobehaviour initializer.
    /// </summary>
    void Start() {
        ControllerManager.Instance = this;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update() {
        /*
        // Process real-time gyroscope data (every 10 updates)
        if (UpdateCount % 10 == 0) { ProcessGyro(); }
        UpdateCount += 1;
        */

        // Process real-time gyroscope data (every single update)
        ProcessGyro();

        // Process any current button inputs from the controller
        ProcessButtons();
    }

    private void ProcessGyro() {
        // Get the vector of the current controller orientation
        Vector3 angles = MiraController.Orientation.eulerAngles;
        float x = angles.x;
        if (x >= 180) {
            x -= 360;
        }
        x = -x;
        GameManager.Instance.setCannonPitch(x);
    }

    /// <summary>
    /// Processes any current inputs from the controller.
    /// </summary>
    private void ProcessButtons() {
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

            TriggerReleased();
        }
    }

    /// <summary>
    /// Called when the controller's Touchpad has been pressed or held down.
    /// </summary>
    private void TouchpadPressed() {
        // Log to console
        Debug.Log("TOUCH PAD PRESSED!");

        // Reset Flyer position
        // TODO flyer.resetFlyer();
    }

    /// <summary>
    /// Called when the controller's Trigger has been pressed or held down.
    /// </summary>
    private void TriggerPressed() {
        // Log to console
        Debug.Log("TRIGGER PRESSED!");

        // Charge the Flyer projectile
        // see GameManager.cs:L48
    }

    private void TriggerReleased() {
        // Log to console
        Debug.Log("TRIGGER RELEASED!");

        // Fire the Flyer projectile
        GameManager.Instance.fire();
    }

}