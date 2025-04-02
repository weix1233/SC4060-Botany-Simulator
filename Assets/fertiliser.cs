using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fertiliser : MonoBehaviour
{
    private GameObject gameController;
    public GameObject fertiliserBag;
    private Vector3 location;
    // Start is called before the first frame update
    void Start(){
        gameController = GameObject.Find("Plant");
        location = fertiliserBag.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pot"){
            if(fertiliserBag.name=="FertilizerA"){
                gameController.GetComponent<GameController>().fertiliserAAdded();
            }
            if(fertiliserBag.name=="FertilizerB"){
                gameController.GetComponent<GameController>().fertiliserBAdded();
            }
            fertiliserBag.SetActive(false);
            fertiliserBag.SetActive(true);
            fertiliserBag.transform.position = location;
        }
    }
}
