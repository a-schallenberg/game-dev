using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateScript : MonoBehaviour {
    [SerializeField] protected List<GameObject> states;

    protected int StateIndex;

    private void Awake() {
        InitState();
    }

    public abstract void InitState();

    public abstract void NextState();

    protected void DisableStates() {
        states.ForEach(state => state.SetActive(false));
    }
}
