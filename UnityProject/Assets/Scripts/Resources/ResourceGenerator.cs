using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ResourceGenerator : MonoBehaviour {
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int          minGeneratedAmount;
    [SerializeField] private int          maxGeneratedAmount;

    private int Amount() {
        return new Random().Next(minGeneratedAmount, maxGeneratedAmount + 1);
    }
    
    public void Generate() {
        ResourceHandler.AddResources(resourceType, Amount());
    }
}
