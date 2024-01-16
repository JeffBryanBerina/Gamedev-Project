using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScriptButton : MonoBehaviour
{
    [SerializeField]
  public void onClick()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

   
}
