using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulPointsUpdate : MonoBehaviour
{
    void LateUpdate()
    {
        GetComponent<TextMeshProUGUI>().SetText($"Soul Points: {PlayerManager.playerManager.soulPoints}");
    }
}
