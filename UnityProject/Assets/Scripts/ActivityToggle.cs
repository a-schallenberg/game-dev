using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     This script is for the activity toggling.
///     If the <c>GameObject</c> should not always be active,
///     but should be able to be switched on and
///     off by the player, then this <c>GameObject</c> needs
///     this script. The parameter is the <c>GameObject</c>
///     on which the script is placed.
/// </summary>
public class ActivityToggle : MonoBehaviour {
	[SerializeField] private UnityEvent onToggle;

	/// <summary>
	/// Toggles activity of the <c>GameObject</c>.
	/// Triggers onToggle.
	/// </summary>
	public void ToggleActivity() {
		gameObject.SetActive(!gameObject.activeSelf);
		onToggle.Invoke();
	}

	/// <summary>
	/// Sets activity of the <c>GameObject</c>.
	/// Triggers onToggle.
	/// </summary>
	/// <param name="active">True, to enable the <c>GameObject</c>, false to disable.</param>
	public void SetActivity(bool active) {
		gameObject.SetActive(active);
		onToggle.Invoke();
	}
}