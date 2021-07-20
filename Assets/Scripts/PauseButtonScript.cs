using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PauseButtonScript : MonoBehaviour {

    public static PauseButtonScript instance;

    public GameObject pausePanel;    
    public Sprite newsprite;
    public Button button;

    public bool active = false;

    void Start()
    {
        instance = this;
    }

    public void OnClick()
    {       
        if (active)
        {
            pausePanel.SetActive(false);
            active = false;
            Time.timeScale = 1;
            button.image.overrideSprite = null;
        }
        else
        {
            pausePanel.SetActive(true);            
            active = true;
            Time.timeScale = 0;
            button.image.overrideSprite = newsprite;
        }

    }
}
