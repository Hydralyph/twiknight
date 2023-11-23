using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TwilightTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    [SerializeField] GameObject timeUpBox;
    [SerializeField] TextMeshProUGUI timeUpDesc;
    [SerializeField] TwilightManager twiManager;
    public int npcCount;
    public string sceneName;


    // Update is called once per frame
    void Update()
    {
        if(twiManager.twilightrescue == npcCount)
        {
            timerText.color = Color.green;
        }
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 10)
            {
                // Color color1;
                // ColorUtility.TryParseHtmlString("FFA500", out color1);
                timerText.color = new Color(1, 0.49f, 0.149f);
            }
            
        }
        else if (remainingTime < 0)
        {
            int rescueCount = twiManager.twilightrescue;
            remainingTime = 0;
            // Twilight End function here
            timerText.color = Color.red;
            timeUpDesc.SetText("Twilight has come to a close!\nYou saved <color=green>" + rescueCount + "</color> People!");
            timeUpBox.SetActive(true);

            StartCoroutine(Waiting(5, timeUpDesc));
            
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator Waiting(float delayTime, TextMeshProUGUI timerDesc)
    {
        // Time.timeScale = 0f;
        bool pressedKey = false;
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 1f;
        timeUpDesc.text += ("\n\nPress <color=green>X</color> to proceed.");
        while (!pressedKey)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                pressedKey = true;
            }
            yield return null;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
