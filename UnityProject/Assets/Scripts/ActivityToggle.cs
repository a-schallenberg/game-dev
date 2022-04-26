using UnityEngine;

public class ActivityToggle : MonoBehaviour {
    public GameObject GameObject;

    public void ToggleActivity() {
        GameObject.SetActive(!GameObject.activeSelf);
    }
}
