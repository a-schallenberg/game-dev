using System;
using UnityEngine;

/// <summary>
/// This is a standard menu to display the normal gameplay, which takes place outside of all other menus, as a menu.
/// </summary>
public class DefaultMenu : MonoBehaviour, IMenu {
    public static DefaultMenu Instance { get; private set; }

    public DefaultMenu() {
        Instance = this;
    }
    
    public void OnBuildMenuButtonPressed() {
        if (MenuHandler.IsActiveMenu(this)) {
            MenuHandler.EnableMenu(BuildMenu.Instance);
        } else if (MenuHandler.IsActiveMenu(BuildMenu.Instance)) {
            MenuHandler.DisableMenu();
        }
    }

    [Obsolete(IMenu.EnableObsoleteMessage, true)]
    public void Enable() {
        InputActions.Game.Enable();
    }
    
    [Obsolete(IMenu.DisableObsoleteMessage, true)]
    public void Disable() {
        InputActions.Game.Disable();
    }
}
