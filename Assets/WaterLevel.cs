using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    private GameObject gameController;
    public GameObject water;
    public bool filled = false;
    
    void Start(){
        gameController = GameObject.Find("Plant");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Well"){
            filled = true;
            water.SetActive(true);
        }
        if(other.tag == "Pot" & filled == true){
            filled = false;
            gameController.GetComponent<GameController>().watered();
            water.SetActive(false);
        }
    }
}
