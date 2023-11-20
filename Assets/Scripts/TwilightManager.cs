using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightManager : MonoBehaviour
{
    public int twilightcount;
    public int twilightrescue;
    public bool isTwilightActive;
    public GameObject twilightbutton;
    public int NumOfTwilightNPCS;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (twilightcount == NumOfTwilightNPCS)
        {
            Debug.Log("Twilight active");
            twilightbutton.SetActive(true);
            isTwilightActive = true;
        }
    }
}
