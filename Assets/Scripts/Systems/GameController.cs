using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnFinalPuzzleCompleted = new UnityEvent();

    [SerializeField] private Puzzle finalPuzzle;

    [SerializeField] private bool yellowActive;
    [SerializeField] private bool redActive;
    [SerializeField] private bool blueActive;

    [SerializeField] private bool greenActive;
    [SerializeField] private bool purpleActive;
    [SerializeField] private bool orangeActive;

    private void Start()
    {
        //        finalPuzzle.OnPuzzleCompleted.AddListener(GameCompleted);
        AudioManager.Instance.PlayMusic(SoundType.StartMuzak);
    }

    public void RestartGame()
    {
        // Reload the current scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
