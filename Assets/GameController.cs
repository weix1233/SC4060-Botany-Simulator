using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public float water = 0.0f;
    public float sunlight = 0.5f;
    public float fertiliserA = 0.0f;
    public float fertiliserB = 0.0f;
    Weather currentWeather;
     
    void Start()
    {
        currentWeather = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().currentWeather;
    }

    void Update()
    {
        currentWeather = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().currentWeather;
    }

    public void watered(){
        water += 0.1f;
        if(water>1.0f) water = 1.0f;
    }
    public void drizzled(){
        water+= 0.1f;
        if(water>1.0f) water = 1.0f;
        sunlight -= 0.2f;
    }
    public void rained(){
        water+= 0.3f;
        if(water>1.0f) water = 1.0f;
        sunlight -= 0.2f;
    }
    public void lamp(){
        if (currentWeather != Weather.SUNNY) sunlight += 0.2f;
    }
    public void cloudy(){
        sunlight -= 0.1f;
    }
    public void fertiliserAAdded(){
        fertiliserA += 0.5f;
    }
    public void fertiliserBAdded(){
        fertiliserB += 0.5f;
    }
}
