using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void buttonChangeScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
    /*
    public void loadMenu ()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadMenu2()
    {
        SceneManager.LoadScene("Menu2");
    }

    public void setActiveScene(string sceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }*/

}
