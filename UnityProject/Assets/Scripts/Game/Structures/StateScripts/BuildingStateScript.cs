using Game.Structures.StateScripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Structures.StateScripts
{
	public class BuildingStateScript : StateScript
	{
		[SerializeField] private float    startingTime; //Von wie viel Sekunden Runter gezählt wird
		[SerializeField] private Slider   progressSlider;
		[SerializeField] private Building building;

		private float _currentTime;

		// Start is called before the first frame update
		private void Start()
		{
			progressSlider.gameObject.SetActive(false);
			progressSlider.maxValue = startingTime;
			_currentTime            = startingTime;
		}

		// Update is called once per frame
		private void Update()
		{
			if (!building.Placed) return;
			
			switch (CurrentState)
			{
				case DisplayState: //Der erste State muss das fertige Gebäude sein damit es auch fertig im BuildMenu angezeigt wird
					NextState();   //Deshalb direkt auf den nächsten State wenn placed
					break;
				case ConstructionState when _currentTime <= 0f: // If construction is finished
					progressSlider.gameObject.SetActive(false);
					NextState();
					break;
				case ConstructionState:               // If is in construction
					if (_currentTime >= startingTime) // If construction has not started yet
						progressSlider.gameObject.SetActive(true);
					
					_currentTime         -= 1 * Time.deltaTime;
					progressSlider.value =  startingTime - _currentTime;
					break;
			}
		}

		public override void InitState()
		{
			DisableStates();
			states[StateIndex].gameObject.SetActive(true);
		}

		public override void NextState()
		{
			if (IsInFinalState()) return;

			CurrentState.gameObject.SetActive(false);
			StateIndex++; // Next State
			CurrentState.gameObject.SetActive(true);
			
			if (CurrentState is ConstructionState)
				_currentTime = startingTime;
		}
	}
}