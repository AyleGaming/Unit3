using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnFinalPuzzleCompleted = new UnityEvent();

    [SerializeField] private Puzzle finalPuzzle;

    private Puzzle currentPuzzle;

    [SerializeField] private bool yellowActive;
    [SerializeField] private bool redActive;
    [SerializeField] private bool blueActive;

    [SerializeField] private bool greenActive;
    [SerializeField] private bool purpleActive;
    [SerializeField] private bool orangeActive;

    private void Start()
    {
//        finalPuzzle.OnPuzzleCompleted.AddListener(GameCompleted);
    }

    public void StartGame()
    {

    }

    public void GameCompleted()
    {
        OnFinalPuzzleCompleted.Invoke();
    }

}
