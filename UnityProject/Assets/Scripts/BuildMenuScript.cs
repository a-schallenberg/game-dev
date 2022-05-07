using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenuScript : MonoBehaviour {
    [SerializeField]
    private Transform foundationView;

    private void OnEnable() {
        foreach (var foundation in PlayerScript.Instance.Foundations) {
            var button = new Button();
            // TODO add image to button
            // TODO add button to foundationView
        }
    }
}
