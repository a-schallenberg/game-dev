using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    [SerializeField] private UnityEvent<Collider2D> onInteract;

    public virtual void Interact(Collider2D collider) {
        onInteract.Invoke(collider);
    }
}
