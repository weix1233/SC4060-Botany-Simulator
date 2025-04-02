using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextDay : MonoBehaviour
{
    Tenkoku.Core.TenkokuModule tenkokuModule;
    private bool skip=false;
    // Start is called before the first frame update
    void Start()
    {
        tenkokuModule = GameObject.Find("TenkokuDynamicSky").gameObject.GetComponent<Tenkoku.Core.TenkokuModule>();
    }

    void Update(){
        if(skip){
            if(tenkokuModule.currentHour==8 & tenkokuModule.currentMinute==0){
                skip = false;
                tenkokuModule.timeMultiplier = 1.0f;
            }
        }
    }
    public void skipDay(){
        tenkokuModule.timeMultiplier = 10000.0f;
        skip = true;
    }
}
