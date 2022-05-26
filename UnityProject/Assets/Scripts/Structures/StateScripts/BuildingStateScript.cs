using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStateScript : StateScript
{
    [SerializeField] private float startingTime;            //Von wie viel Sekunden Runter gezählt wird
    [SerializeField] private Slider progressSlider;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        progressSlider.gameObject.SetActive(false);
        progressSlider.maxValue = startingTime;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Building>().Placed)
        {
            if (StateIndex == 0)                        //Der erste State muss das fertige Gebäude sein damit es auch fertig im BuildMenu angezeigt wird
            {                                           //Deshalb direkt auf den nächsten State wenn placed
                progressSlider.gameObject.SetActive(true);
                NextState();
            }
            if (currentTime <= 0)
            {
                NextState();
            }
            else
            {
                currentTime -= 1 * Time.deltaTime;
                progressSlider.value = startingTime - currentTime;
            }
        }
    }

    public override void InitState()
    {
        DisableStates();
        states[StateIndex].SetActive(true);
    }

    public override void NextState()
    {
        if (StateIndex + 1 < states.Count)
        {
            currentTime = startingTime;
            states[StateIndex].SetActive(false);
            states[++StateIndex].SetActive(true);
            if (StateIndex + 1 == states.Count)         //Wenn man am letzten State ist, bzw das Gebäude fertig ist, kann man damit interagieren
            {
                progressSlider.gameObject.SetActive(false);
                gameObject.GetComponent<Building>().setCanInteract(true);
            }
        }
    }
}
