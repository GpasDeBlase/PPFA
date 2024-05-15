using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameSystem : MonoBehaviour
{
    public GameObject UIperdu;
    public GameObject UIgagne;
    [SerializeField] private string sceneName = "Level1";
    [SerializeField] private string sceneName2 = "EcranTitre";
    public int checkpoints;
    private int checkNum;

    void Start()
    {
        Time.timeScale = 1;
        UIperdu.SetActive(false);
        UIgagne.SetActive(false);
        checkNum = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneName2);
        }
    }

    public void addCheckpoint()
    {
        checkNum++;
        Debug.Log(checkNum);
    }

    public void FinishRace()
    {
        if (checkNum == checkpoints)
        {
            Time.timeScale = 0;
            UIgagne.SetActive(true);

        }
    }


    public void perdu()
    {
        Time.timeScale = 0;
        UIperdu.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quitter()
    {
        SceneManager.LoadScene(sceneName2);
    }
}
