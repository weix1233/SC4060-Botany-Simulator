using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObject seedPot;
    public GameObject emptyPot;
    public GameObject seedBag;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Seed")
        {
            // Step 1: Seed bag picked up.
            seedBag.SetActive(false);
            GameObject.Find("TutorialManager").GetComponent<TutorialManager>().AdvanceTutorial();

            // Step 2: Seed planted.
            seedPot.SetActive(true);
            emptyPot.SetActive(false);
            GameObject.Find("TutorialManager").GetComponent<TutorialManager>().AdvanceTutorial();
        }
    }
}
