using UnityEngine;
using UnityEngine.UI;

namespace Game.Structures.StateScripts {
	public class BuildingStateScript : StateScript {
		private const string TagInProgress = "Structure_InProgress";
		private const string TagFinished   = "Structure_Finished";

		[SerializeField] private float    startingTime; //Von wie viel Sekunden Runter gezählt wird
		[SerializeField] private Slider   progressSlider;
		[SerializeField] private Building building;

		private float _currentTime;

		// Start is called before the first frame update
		private void Start() {
			progressSlider.gameObject.SetActive(false);
			progressSlider.maxValue = startingTime;
			_currentTime            = startingTime;
		}

		// Update is called once per frame
		private void Update() {
			if (building.Placed) {
				if (StateIndex == 0) //Der erste State muss das fertige Gebäude sein damit es auch fertig im BuildMenu angezeigt wird
				{                    //Deshalb direkt auf den nächsten State wenn placed
					progressSlider.gameObject.SetActive(true);
					NextState();
				}

				if (_currentTime <= 0) {
					NextState();
				} else {
					_currentTime         -= 1 * Time.deltaTime;
					progressSlider.value =  startingTime - _currentTime;
				}
			}
		}

		public override void InitState() {
			DisableStates();
			states[StateIndex].SetActive(true);
		}

		public override void NextState() {
			if (states[StateIndex].tag.Equals(TagInProgress)) {
				_currentTime = startingTime;
				states[StateIndex].SetActive(false);
				states[++StateIndex].SetActive(true);
				if (states[StateIndex].tag.Equals(TagFinished)) //Wenn man am letzten State ist, bzw das Gebäude fertig ist, kann man damit interagieren
				{
					progressSlider.gameObject.SetActive(false);
				}
			}
		}
	}
}