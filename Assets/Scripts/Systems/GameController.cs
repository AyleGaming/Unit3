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

    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        //        finalPuzzle.OnPuzzleCompleted.AddListener(GameCompleted);
        if (backgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(backgroundMusic, 0.5f);
        }
    }

    public void RestartGame()
    {
        // Reload the current scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
