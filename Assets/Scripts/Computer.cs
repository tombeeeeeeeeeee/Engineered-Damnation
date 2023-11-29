using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Focusable
{
    public override void Init()
    {
        // calling back to base first is important, it enables all the
        // controls and also invokes ShowUI and OnFocus
        base.Init();
        // after base Init adds all the controls, we can remove just the
        // exit control. exiting this menu is handled by the UI instead.
        player.controls.Focused.Exit.performed -= Exit;
    }

    public override void OnFocus()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Gameplay.gameplayActive = false;
    }

    public override void OnUnfocus()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
