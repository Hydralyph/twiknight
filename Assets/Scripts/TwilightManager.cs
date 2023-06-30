using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightManager : MonoBehaviour
{
    public int twilightcount;
    public GameObject twilightbutton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (twilightcount == 2)
        {
            Debug.Log("Twilight active");
            twilightbutton.SetActive(true);
        }
    }
}
