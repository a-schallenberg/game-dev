using UnityEditor;

/// <summary>
/// This class organizes all menus. Every menu can be enabled by executing the EnableMenu method.
/// DisableMenu will disable the current menu and enable the default menu.
/// </summary>
public static class MenuHandler {
	public static IMenu ActiveMenu    { get; private set; }
	public static bool  OverlayActive { get; private set; }

	
	static MenuHandler() {
		EnableMenu(DefaultMenu.Instance);
	}

	public static void EnableMenu(IMenu menu, bool overlay = true) {
		#pragma warning disable CS0618
		ActiveMenu?.Disable();
		ActiveMenu = menu;
		ActiveMenu.Enable();
		#pragma warning restore CS0618
		SetOverlayActive(overlay);
	}

	public static void DisableMenu() {
		EnableMenu(DefaultMenu.Instance);
		SetOverlayActive(true);
	}

	public static bool IsActiveMenu(IMenu menu) {
		if (ActiveMenu == null) {
			return menu == null;
		}

		return ActiveMenu.Equals(menu);
	}

	public static void SetOverlayActive(bool active) {
		if (OverlayActive != active) {
			Overlay.Instance.ActivityToggle.SetActivity(active);
			OverlayActive = active;
		}
	}
}