using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour {

    public GameObject pausePanel;
    public void onClick()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        PauseButtonScript.instance.button.image.overrideSprite = null;
        PauseButtonScript.instance.active = false;
    }
}
