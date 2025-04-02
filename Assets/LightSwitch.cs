using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lamp;
    private GameObject gameController;
    public GameObject pointLight;
    public GameObject spotLight;
    private bool switchState = false;
    private Material material;

    // Start is called before the first frame update
    void Start(){
        gameController = GameObject.Find("Plant");
        material = lamp.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    public void switchUsed()
    {
        if(switchState==false){
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
