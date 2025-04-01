using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour{
    public GameObject seedPot;
    public GameObject emptyPot;
    public GameObject seedBag;
    
    void OnTriggerEnter(Collider other){
        if(other.tag == "Seed")
        {
            seedBag.SetActive(false);
            seedPot.SetActive(true);
            emptyPot.SetActive(false);
        }
    }
}
