using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonManager : MonoBehaviour
{
    [SerializeField] private string sceneName = "Level1";

    public void Quitter()
    {
        Application.Quit();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
