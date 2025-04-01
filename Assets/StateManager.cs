using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public Material healthy, unhealthy;
    private state waterState, sunlightState, fertiliserAState, fertiliserBState;
    public GameObject water, sun, fertA, fertB;

    void Update()
    {
        waterState = GameObject.Find("Plant").GetComponent<GameController>().waterState;
        sunlightState = GameObject.Find("Plant").GetComponent<GameController>().sunlightState;
        fertiliserAState = GameObject.Find("Plant").GetComponent<GameController>().fertiliserAState;
        fertiliserBState = GameObject.Find("Plant").GetComponent<GameController>().fertiliserBState;

        if(sunlightState == state.HEALTHY)
            sun.GetComponent<Image>().material = healthy;
        else
            sun.GetComponent<Image>().material = unhealthy;
        if(waterState == state.HEALTHY)
            water.GetComponent<Image>().material = healthy;
        else
            water.GetComponent<Image>().material = unhealthy;
        if(fertiliserAState == state.HEALTHY)
            fertA.GetComponent<Image>().material = healthy;
        else
            fertA.GetComponent<Image>().material = unhealthy;
        if(fertiliserBState == state.HEALTHY)
            fertB.GetComponent<Image>().material = healthy;
        else
            fertB.GetComponent<Image>().material = unhealthy;

        water.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = waterState.ToString();
        sun.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = sunlightState.ToString();
        fertA.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = fertiliserAState.ToString();
        fertB.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = fertiliserBState.ToString();
    }
}
