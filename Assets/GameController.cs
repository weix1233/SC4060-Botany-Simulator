using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum state{
    HEALTHY,
    EXCESSIVE,
    INSUFFICIENT
}

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public float water = 0.5f;
    public float sunlight = 0.5f;
    public float fertiliserA = 0.0f;
    public float fertiliserB = 0.0f;
    Weather currentWeather, prevWeather;
    public String weatherString;
    private GameObject pot;
    public bool lampState = false;
    public int daysHealthy = 0, daysUnhealthy = 0, daysRequired = 0, prevDay;
    public int totalDays = 0;
    public GameObject[] plantStates;
    public state waterState = state.HEALTHY, sunlightState = state.HEALTHY, fertiliserAState = state.HEALTHY, fertiliserBState = state.HEALTHY;
     
    void Start()
    {
        currentWeather = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().currentWeather;
        prevDay = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().numDays;
        prevWeather = currentWeather;
        if(PlayerPrefs.HasKey("Water")){
            GameObject.FindGameObjectWithTag("Pot").SetActive(false);
            water = PlayerPrefs.GetFloat("Water");
            sunlight = PlayerPrefs.GetFloat("Sunlight");
            fertiliserA = PlayerPrefs.GetFloat("FertiliserA");
            fertiliserB = PlayerPrefs.GetFloat("FertiliserB");
            totalDays = PlayerPrefs.GetInt("TotalDays");
            daysHealthy = PlayerPrefs.GetInt("daysHealthy");
            daysUnhealthy = PlayerPrefs.GetInt("daysUnhealthy");
            daysRequired = PlayerPrefs.GetInt("daysRequired");

            lampState = PlayerPrefs.GetInt("lampState")==1;
            if(lampState){
                GameObject.Find("PushButton").GetComponent<LightSwitch>().switchUsed();
            }

            foreach (GameObject g in plantStates){
                if(g.name==PlayerPrefs.GetString("PlantState")){
                    g.SetActive(true);
                    if(g.name!="plantPot"){
                        GameObject.Find("Seed Bag").SetActive(false);
                    }
                    break;
                }
            }
            weatherString = PlayerPrefs.GetString("CurrentWeather");
            GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().currentWeather = (Weather)Enum.Parse(typeof(Weather), weatherString, true);
            GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().updateWeather();
        }
    }

    void Update()
    {
        currentWeather = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().currentWeather;
        weatherString = currentWeather.ToString();
        if(prevDay!=GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().numDays){
            if(prevWeather!=Weather.SUNNY & lampState==true) sunlight+=0.2f;
            else if(prevWeather==Weather.SUNNY & lampState==true) sunlight+=0.1f;
            else if(prevWeather!=Weather.SUNNY & currentWeather == Weather.SUNNY) sunlight+=0.1f;
            prevWeather = currentWeather;
            if(currentWeather==Weather.DRIZZLE) drizzled();
            else if(currentWeather==Weather.RAINY) rained();
            else if(currentWeather==Weather.CLOUDY) cloudy();
            prevDay = GameObject.Find("TenkokuDynamicSky").GetComponent<WeatherController>().numDays;
            water-=0.1f;
            fertiliserA-=0.05f;
            fertiliserB-=0.05f;
            totalDays+=1;
            if(water<0) water=0;
            else if(water>1) water = 1;
            if(fertiliserA<0) fertiliserA=0.0f;
            else if(fertiliserA>1) fertiliserA = 1.0f;
            if(fertiliserB<0) fertiliserB=0.0f;
            else if(fertiliserB>1) fertiliserB = 1.0f;
            stateTransition();
        }
        stateCheck();
    }

    public void gameOver(){
        Debug.Log("GameOver");
    }
    public void gameWon(){
        Debug.Log("GameWon");
    }
    public void watered(){
        water += 0.1f;
        if(water>1.0f) water = 1.0f;
    }
    public void drizzled(){
        water+= 0.1f;
        if(water>1.0f) water = 1.0f;
        sunlight -= 0.2f;
        if(sunlight<0) sunlight = 0.0f;
    }
    public void rained(){
        water+= 0.2f;
        if(water>1.0f) water = 1.0f;
        sunlight -= 0.2f;
        if(sunlight<0) sunlight = 0.0f;
    }
    public void lamp(){
        if(lampState==true) lampState = false;
        else lampState = true;
    }
    public void cloudy(){
        sunlight -= 0.1f;
        if(sunlight<0) sunlight = 0.0f;
    }
    public void fertiliserAAdded(){
        fertiliserA += 0.5f;
        if(fertiliserA>1.0f) fertiliserA=1.0f;
    }
    public void fertiliserBAdded(){
        fertiliserB += 0.5f;
        if(fertiliserB>1.0f) fertiliserB=1.0f;
    }

    public void stateCheck(){
        if(water<0.5) waterState = state.INSUFFICIENT;
        else if(water>=0.8) waterState = state.EXCESSIVE;
        else waterState = state.HEALTHY;
        if(sunlight<=0.3) sunlightState = state.INSUFFICIENT;
        else if(sunlight>=0.8) sunlightState = state.EXCESSIVE;
        else sunlightState = state.HEALTHY;
        if(fertiliserA>0.5) fertiliserAState = state.EXCESSIVE;
        else fertiliserAState = state.HEALTHY;
        if(fertiliserB>0.5) fertiliserBState = state.EXCESSIVE;
        else fertiliserBState = state.HEALTHY;
    }

    public void stateTransition(){
        pot = GameObject.FindGameObjectWithTag("Plant");

        if(pot.name == "seed"){
            if(water > 0.3) daysHealthy+=1;
            if(daysHealthy == 2){
                daysHealthy = 0;
                plantStates[0].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "germinate"){
            if(water > 0.3) daysHealthy+=1;
            if(daysHealthy == 3){
                daysHealthy = 0;
                plantStates[1].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "sapling"){
            if(daysRequired==0) daysRequired = 7;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
                daysUnhealthy = 0;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 2){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    plantStates[2].SetActive(true);
                    pot.SetActive(false);
                }
            }
            if(fertiliserA>0) daysRequired = 6;
            if(fertiliserB>0) daysRequired = 8;
            if(daysHealthy == daysRequired){
                daysHealthy = 0;
                daysRequired = 0;
                plantStates[3].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "dyingSapling"){
            if(daysRequired==0) daysRequired = 2;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 3){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    gameOver();
                }
            }
            if(daysHealthy==2){
                daysHealthy = 0;
                daysRequired = 0;
                daysUnhealthy = 0;
                plantStates[1].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "youngPlant"){
            if(daysRequired==0) daysRequired = 9;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
                daysUnhealthy = 0;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 2){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    plantStates[4].SetActive(true);
                    pot.SetActive(false);
                }
            }
            if(fertiliserA>0) daysRequired = 8;
            if(fertiliserB>0) daysRequired = 10;
            if(daysHealthy == daysRequired){
                daysHealthy = 0;
                daysRequired = 0;
                plantStates[5].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "dyingYoungPlant"){
            if(daysRequired==0) daysRequired = 2;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 3){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    gameOver();
                }
            }
            if(daysHealthy==2){
                daysHealthy = 0;
                daysRequired = 0;
                daysUnhealthy = 0;
                plantStates[3].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "notSoYoungPlant"){
            if(daysRequired==0) daysRequired = 10;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
                daysUnhealthy = 0;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 2){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    plantStates[6].SetActive(true);
                    pot.SetActive(false);
                }
            }
            if(fertiliserA>0) daysRequired = 11;
            if(fertiliserB>0) daysRequired = 9;
            if(daysHealthy == daysRequired){
                daysHealthy = 0;
                daysRequired = 0;
                plantStates[7].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "dyingNotSoYoungPlant"){
            if(daysRequired==0) daysRequired = 2;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 3){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    gameOver();
                }
            }
            if(daysHealthy==2){
                daysHealthy = 0;
                daysRequired = 0;
                daysUnhealthy = 0;
                plantStates[5].SetActive(true);
                pot.SetActive(false);
            }
        }
        else if(pot.name == "flower"){
            if(daysRequired==0) daysRequired = 9;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
                daysUnhealthy = 0;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 2){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    plantStates[8].SetActive(true);
                    pot.SetActive(false);
                }
            }
            if(fertiliserA>0) daysRequired = 10;
            if(fertiliserB>0) daysRequired = 8;
            if(daysHealthy == daysRequired){
                daysHealthy = 0;
                daysRequired = 0;
                plantStates[9].SetActive(true);
                pot.SetActive(false);
                gameWon();
            }
        }
        else if(pot.name == "dyingFlower"){
            if(daysRequired==0) daysRequired = 2;
            if(waterState == state.HEALTHY & sunlightState == state.HEALTHY & fertiliserAState == state.HEALTHY){
                daysHealthy+=1;
            }
            else{
                daysUnhealthy+=1;
                daysHealthy-=1;
                if(daysUnhealthy == 3){
                    daysUnhealthy = 0;
                    daysRequired = 0;
                    gameOver();
                }
            }
            if(daysHealthy==2){
                daysHealthy = 0;
                daysRequired = 0;
                daysUnhealthy = 0;
                plantStates[7].SetActive(true);
                pot.SetActive(false);
            }
        }
    }
}
