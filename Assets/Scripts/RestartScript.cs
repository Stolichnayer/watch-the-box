using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour {

    public GameObject PausePanel;

    public void OnClick()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseButtonScript.instance.button.image.overrideSprite = null;
    }
}
