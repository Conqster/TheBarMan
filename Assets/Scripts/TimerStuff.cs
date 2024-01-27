using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerStuff : MonoBehaviour
{
    public float time = 300;
    public TextMeshProUGUI timerText;
    private bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            gameStarted = true;
            Debug.Log("game begun");
        }

        timerText.text = time.ToString("00");

            if (gameStarted)
            {
                time -= Time.deltaTime;
            }

            if (time <= 0)
            {
                Debug.Log("next scene");
                SceneManager.LoadScene("Club");
            }
    }
}
