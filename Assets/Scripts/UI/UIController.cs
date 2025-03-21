using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private HealthSystem playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private TreeOfLife treeOfLife;
    private StatusManager statusManager;
    [SerializeField] private Image yellowImage;
    [SerializeField] private Image redImage;
    [SerializeField] private Image blueImage;
    [SerializeField] private Image orangeImage;
    [SerializeField] private Image greenImage;
    [SerializeField] private Image purpleImage;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject liveScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI endScreenText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = PlayerInput.Instance.GetComponent<HealthSystem>();
        playerHealth.OnLifeChange += UpdateHealthText;
        playerHealth.OnDead += DisplayDeathScreen;

        statusManager = StatusManager.Instance;
        statusManager.OnColorsChange += UpdateColors;

        treeOfLife = TreeOfLife.Instance;
        treeOfLife.OnGameOver += UpdateGameOverScreen;
   }

    void DisplayDeathScreen()
    {
        liveScreen.SetActive(false);
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UpdateGameOverScreen()
    {
        liveScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UpdateHealthText(float healthToDisplay)
    {
        healthText.text = "Health: " + healthToDisplay.ToString();
    }

    void UpdateColors()
    {
        yellowImage.color = statusManager.IsColorActive(Colors.Yellow) ? Color.yellow : Color.gray;
        redImage.color = statusManager.IsColorActive(Colors.Red) ? Color.red : Color.gray;
        blueImage.color = statusManager.IsColorActive(Colors.Blue) ? Color.blue : Color.gray;


        if (statusManager.IsColorActive(Colors.Orange))
        {
            orangeImage.gameObject.SetActive(true);
        } 
        else
        {
            orangeImage.gameObject.SetActive(false);
        }

        if (statusManager.IsColorActive(Colors.Green))
        {
            greenImage.gameObject.SetActive(true);
        }
        else
        {
            greenImage.gameObject.SetActive(false);
        }

        if (statusManager.IsColorActive(Colors.Purple))
        {
            purpleImage.gameObject.SetActive(true);
        }
        else
        {
            purpleImage.gameObject.SetActive(false);
        }
    }
}
