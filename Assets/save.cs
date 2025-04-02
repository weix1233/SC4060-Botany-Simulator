using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    private GameObject gamecontrol;
    // Start is called before the first frame update
    void Start()
    {
        gamecontrol = GameObject.Find("Plant");
    }

    // Update is called once per frame
    public void saveGame()
    {
        PlayerPrefs.SetFloat("Water", gamecontrol.GetComponent<GameController>().water);
        PlayerPrefs.SetFloat("Sunlight", gamecontrol.GetComponent<GameController>().sunlight);
        PlayerPrefs.SetFloat("FertiliserA", gamecontrol.GetComponent<GameController>().fertiliserA);
        PlayerPrefs.SetFloat("FertiliserB", gamecontrol.GetComponent<GameController>().fertiliserB);
        PlayerPrefs.SetString("PlantState", GameObject.FindGameObjectWithTag("Pot").name);
        PlayerPrefs.SetInt("TotalDays", gamecontrol.GetComponent<GameController>().totalDays);
        PlayerPrefs.SetString("CurrentWeather", gamecontrol.GetComponent<GameController>().weatherString);
        PlayerPrefs.SetInt("daysHealthy", gamecontrol.GetComponent<GameController>().daysHealthy);
        PlayerPrefs.SetInt("daysUnhealthy", gamecontrol.GetComponent<GameController>().daysUnhealthy);
        PlayerPrefs.SetInt("daysRequired", gamecontrol.GetComponent<GameController>().daysRequired);
        //1 if lamp is on, else 0
        PlayerPrefs.SetInt("lampState", (gamecontrol.GetComponent<GameController>().lampState==true)?1:0);
        Application.Quit();
    }
}
