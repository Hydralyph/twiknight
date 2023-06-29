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
        for(int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = false;
        }

        for (int lives = 0; lives < PlayerManager.playerManager.PlayerLives; lives++)
        {
            heartImages[lives].enabled = true;
        }
    }
}
