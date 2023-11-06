using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcons : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;

    // Update is called once per frame
    void Update()
    {
        // Disable all heart images in preparation for re-enabling each image based on live count
        for(int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = false;
        }

        // Enable only the number of hearts that represent the Players Lives count
        for (int lives = 0; lives < PlayerManager.playerManager.PlayerLives; lives++)
        {
            heartImages[lives].enabled = true;
        }
    }
}
