using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour {
	public static Overlay Instance { get; private set; }

	[SerializeField] private ActivityToggle activityToggle;

	public Overlay() {
		Instance = this;
	}

	public ActivityToggle ActivityToggle {
		get { return activityToggle; }
	}
}
