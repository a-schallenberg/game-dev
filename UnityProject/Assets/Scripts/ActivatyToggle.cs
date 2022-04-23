using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatyToggle : MonoBehaviour {
    public GameObject GameObject;

    public void ToggleActivity() {
        GameObject.SetActive(!GameObject.activeSelf);
    }
}
