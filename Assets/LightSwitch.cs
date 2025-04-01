using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lamp;
    private GameObject gameController;
    public GameObject pointLight;
    public GameObject spotLight;
    private bool switchState = false;
    private Material material;

    void Start(){
        gameController = GameObject.Find("Plant");
        material = lamp.GetComponent<Renderer>().material;
    }

    public void switchUsed()
    {
        if(switchState == false){
            switchState = true;
            material.EnableKeyword("_EMISSION");
            pointLight.SetActive(true);
            spotLight.SetActive(true);
            gameController.GetComponent<GameController>().lamp();
        }
        else{
            switchState = false;
            material.DisableKeyword("_EMISSION");
            pointLight.SetActive(false);
            spotLight.SetActive(false);
            gameController.GetComponent<GameController>().lamp();
        }
    }
}
