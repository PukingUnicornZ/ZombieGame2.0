using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static int gameType;
    public void loadSceneByIndex(int i)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
    }
    public void loadSceneByName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    public void setGameType(int i)
    {
        gameType = i;
    }
}
