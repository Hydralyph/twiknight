using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightManager : MonoBehaviour
{
    public int twilightcount; // Currently hard-set to aim for 2 max for testing purposes, will become variable later on.
    public GameObject twilightbutton; // On-screen testing/debug indicator that Twilight is enabled

    // Update is called once per frame
    void Update() // For testing purposes, will be expanded upon the Twilight system being properly implemented
    {
        if (twilightcount == 2)
        {
            Debug.Log("Twilight active");
            twilightbutton.SetActive(true);
        }
    }
}
