using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScriptScore : MonoBehaviour {
    public GameObject ob;

    public void OnClick()
    {
        Time.timeScale = 1;
        ob.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
