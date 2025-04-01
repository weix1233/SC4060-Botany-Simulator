using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilState : MonoBehaviour
{
    public GameObject pot;
    private GameObject gameController;
    private float water;
    public Material dry;
    public Material normal;
    public Material watered;

    void Start()
    {
        gameController = GameObject.Find("Plant");
        water = gameController.GetComponent<GameController>().water;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameController.GetComponent<GameController>().water != water){
            if(gameController.GetComponent<GameController>().water < 0.4){
                Material[] mats = pot.GetComponent<MeshRenderer>().materials; 
                mats[1] = dry; 
                pot.GetComponent<MeshRenderer>().materials = mats;
            }
            else if(gameController.GetComponent<GameController>().water <= 0.6){
                Material[] mats = pot.GetComponent<MeshRenderer>().materials; 
                mats[1] = normal; 
                pot.GetComponent<MeshRenderer>().materials = mats;
            }
            else{
                Material[] mats = pot.GetComponent<MeshRenderer>().materials; 
                mats[1] = watered; 
                pot.GetComponent<MeshRenderer>().materials = mats;
            }
            water = gameController.GetComponent<GameController>().water;
        }   
    }
}
