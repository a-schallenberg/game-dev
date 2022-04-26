using UnityEngine;

/// <summary>
/// This script is for the activity toggling.
/// If the <c>GameObject</c> should not always be active,
/// but should be able to be switched on and
/// off by the player, then this <c>GameObject</c> needs
/// this script. The parameter is the <c>GameObject</c>
/// on which the script is placed.
/// </summary>
public class ActivityToggle : MonoBehaviour {
    public GameObject GameObject;

    public void ToggleActivity() {
        GameObject.SetActive(!GameObject.activeSelf);
    }
}
