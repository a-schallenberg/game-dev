using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopSIMP : MonoBehaviour {
    [SerializeField] private Transform     group;
    [SerializeField] private Button     buttonPrefab;
    [SerializeField] private List<Building> prefabs;

    private readonly Dictionary<Button, Building> _buttons = new();

    private void OnEnable() {
        Init();
    }

    private void Init() {
        foreach (var building in prefabs) {
            Button button = Instantiate(buttonPrefab, group);
            button.image.sprite = building.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            button.GetComponent<WorkshopButton>().Init(building);
        }
    }
}
