using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button closeButton;
    public void PauseButtonClicked()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
           // pauseButtonText.text = "Resume";
        }
        else
        {
            //Time.timeScale = 1;
            //pauseButtonText.text = "Pause";
        }

        closeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;

        });
    }
}
