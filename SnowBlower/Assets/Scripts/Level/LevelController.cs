using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController inst;

    [SerializeField] CarController player;

    public List<Follower> followers; 

    [SerializeField] [SerializeReference] List<Worker> movables;

    public float speed;

    bool _playing;

    bool waitForRestart;

    [SerializeField] UnityEvent gameStartEvent;
    [SerializeField] UnityEvent gameFinishEvent;
    [SerializeField] UnityEvent gameLoseEvent;

    void Awake()
    {
        if(inst == null)
            inst = this;
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if(!_playing)
        {
            if(Input.GetMouseButtonDown(0))
            {
                StartMoving();
            }
        }

        if(waitForRestart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    public void StartMoving()
    {
        _playing = true;
        foreach (var item in movables)
            item.IsWorking = true;

        foreach (var item in followers)
            item.IsWorking = true;

        player.IsWorking = true;

        gameStartEvent.Invoke();
    }

    public void StopMoving()
    {
        foreach (var item in movables)
            item.IsWorking = false;

        waitForRestart = true;
        gameLoseEvent.Invoke();
    }

    public void Finish()
    {
        waitForRestart = true;
        gameFinishEvent.Invoke();
    }
}
