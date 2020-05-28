using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    void Start()
    {
        startScreen.SetActive(true);

    }

    public void OnStart()
    {
        startScreen.SetActive(false);
    }

    public void OnLose()
    {
        loseScreen.SetActive(true);

    }

    public void OnWin()
    {
        winScreen.SetActive(true);

    }
}
