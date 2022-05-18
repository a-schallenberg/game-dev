using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceBar : MonoBehaviour {
    public static ResourceBar Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI wood;
    [SerializeField] private TextMeshProUGUI stone;
    [SerializeField] private TextMeshProUGUI iron;
    
    public ResourceBar() {
        Instance = this;
    }

    public void SetResourceAmount(ResourceType type, int amount) {
        switch (type) {
            case ResourceType.Wood:
                wood.text = amount.ToString();
                break;
            case ResourceType.Stone:
                stone.text = amount.ToString();
                break;
            case ResourceType.Iron:
                iron.text = amount.ToString();
                break;
        }
    }
}
