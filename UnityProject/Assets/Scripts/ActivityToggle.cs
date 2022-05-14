using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is for the activity toggling.
/// If the <c>GameObject</c> should not always be active,
/// but should be able to be switched on and
/// off by the player, then this <c>GameObject</c> needs
/// this script. The parameter is the <c>GameObject</c>
/// on which the script is placed.
/// </summary>
public class ActivityToggle : MonoBehaviour {
	[SerializeField] private UnityEvent onToggle;

	public void ToggleActivity() {
		gameObject.SetActive(!gameObject.activeSelf);
		onToggle.Invoke();
	}

	public void SetActivity(bool active) {
		gameObject.SetActive(active);
		onToggle.Invoke();
	}
}