using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputSystemSwitcher : MonoBehaviour
{
  public void Update() {
    if (Keyboard.current.tKey.wasPressedThisFrame) {
      TouchSimulation.Enable();      
      return;
    }
    if (Keyboard.current.yKey.wasPressedThisFrame) {
      TouchSimulation.Disable();
      return;
    }
  }
}
