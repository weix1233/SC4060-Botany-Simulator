using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
    SUNNY,
    CLOUDY,
    DRIZZLE,
    RAINY
}

public class WeatherController : MonoBehaviour
{
    Tenkoku.Core.TenkokuModule tenkokuModule;
    int prevDay;
    public int numDays = 0;
    
    public Weather currentWeather;

    void Start()
    {
        tenkokuModule = GameObject.Find("TenkokuDynamicSky").gameObject.GetComponent<Tenkoku.Core.TenkokuModule>();
        prevDay = tenkokuModule.currentDay;
        currentWeather = Weather.SUNNY;
    }

    void Update()
    {
        if (tenkokuModule.currentDay != prevDay)
        {
            numDays += 1;
            int randomWeather = Random.Range(0, 100);
            prevDay = tenkokuModule.currentDay;
            if (randomWeather < 15)
            {
                tenkokuModule.weather_RainAmt = 1.0f;
                tenkokuModule.weather_OvercastAmt = 0.2f;
                tenkokuModule.weather_WindAmt = 0.5f;
                tenkokuModule.weather_lightning = 0.4f;
                tenkokuModule.weather_cloudCumulusAmt = 0.0f;
                currentWeather = Weather.RAINY;
            }
            else if (randomWeather < 25)
            {
                tenkokuModule.weather_RainAmt = 0.3f;
                tenkokuModule.weather_OvercastAmt = 0.15f;
                tenkokuModule.weather_WindAmt = 0.15f;
                tenkokuModule.weather_lightning = 0.1f;
                tenkokuModule.weather_cloudCumulusAmt = 0.0f;
                currentWeather = Weather.DRIZZLE;
            }
            else if (randomWeather < 60)
            {
                tenkokuModule.weather_RainAmt = 0.0f;
                tenkokuModule.weather_OvercastAmt = 0.2f;
                tenkokuModule.weather_WindAmt = 0.0f;
                tenkokuModule.weather_lightning = 0.0f;
                tenkokuModule.weather_cloudCumulusAmt = 0.9f;
                currentWeather = Weather.CLOUDY;
            }
            else
            {
                tenkokuModule.weather_RainAmt = 0.0f;
                tenkokuModule.weather_OvercastAmt = 0.1f;
                tenkokuModule.weather_WindAmt = 0.0f;
                tenkokuModule.weather_lightning = 0.0f;
                tenkokuModule.weather_cloudCumulusAmt = 0.0f;
                currentWeather = Weather.SUNNY;
            }
        }
    }
}
