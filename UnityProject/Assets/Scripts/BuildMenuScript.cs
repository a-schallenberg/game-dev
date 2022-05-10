using UnityEngine;
using UnityEngine.UI;


public class BuildMenuScript : MonoBehaviour {
	[SerializeField] private Button                button;
	[SerializeField] private RectTransform         foundationView;
	[SerializeField] private HorizontalLayoutGroup group;

	private void OnEnable() {
		SetFoundationItems();
	}

	private void SetFoundationItems() {
		var foundations = PlayerScript.Instance.foundations;
		var size        = foundations.Count;

		var buttonWidthSum = 0f;
		foreach (var foundation in foundations) {
			var btn = Instantiate(button, foundationView);
			btn.image.sprite = foundation.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
			btn.onClick.AddListener(() => StructureHandler.Instance.OnInstantiateButtonClicked(foundation.gameObject));

			buttonWidthSum += ((RectTransform) btn.transform).rect.width;
		}

		var width = group.padding.left + group.padding.right + buttonWidthSum + (size - 1) * group.spacing;
		foundationView.sizeDelta = new Vector2(width, 0);
	}
}