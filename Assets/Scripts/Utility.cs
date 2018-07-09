using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Utility : MonoBehaviour {

    public static void setColor(GameObject orb)
    {
        Color color; // cyan green magenta
        switch (Random.Range(0, 6))
        {
            case 0:
                color = Color.blue;
                break;
            case 1:
                color = Color.blue;
                break;
            case 2:
                color = Color.red;
                break;
            case 3:
                color = Color.yellow;
                break;
            case 4:
                color = Color.red;
                break;
            case 5:
                color = Color.yellow;
                break;
            default:
                color = Color.red;
                break;
        }
        orb.GetComponent<Renderer>().material.color = color;
    }
    
}