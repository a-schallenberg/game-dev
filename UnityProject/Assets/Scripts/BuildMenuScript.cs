using UnityEngine;
using UnityEngine.UI;


public class BuildMenuScript : MonoBehaviour {
    [SerializeField]
    private RectTransform foundationView;
    [SerializeField]
    private HorizontalLayoutGroup group;

    private void OnEnable() {
        SetFoundationItems();
    }

    private void SetFoundationItems() {
        var foundations = PlayerScript.Instance.foundations;
        var size = foundations.Count;
        
        var buttonWidthSum = 0f;
        for (var i = 0; i < size; i++) {
            var button = Instantiate(foundations[i], foundationView);
            buttonWidthSum += ((RectTransform) button.transform).rect.width;
        }
        
        var width = group.padding.left + group.padding.right + buttonWidthSum + (size - 1) * group.spacing;
        foundationView.sizeDelta = new Vector2(width , 0);
    }
}
