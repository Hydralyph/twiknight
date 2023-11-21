using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public GameObject endBox;

    public void BossHasDied()
    {
        Debug.Log("Boss has died");
        StartCoroutine(ReturnToTitle());
    }

    IEnumerator ReturnToTitle()
    {
        endBox.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Ending", LoadSceneMode.Single);
    }
}
