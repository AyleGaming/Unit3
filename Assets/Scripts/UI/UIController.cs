using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    [SerializeField] private Slider healthSlider;  // Assign the prefab in Inspector
    [SerializeField] private GameObject colorTextPrefab;  // Assign the prefab in Inspector
    [SerializeField] private Transform colorListParent;   // Assign the UI panel where texts will go

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
        healthSlider.value = healthToDisplay;
        healthText.text = healthToDisplay.ToString() + " / 100";
    }

    private void PopulateColorList()
    {
        // Clear any previous UI elements
        foreach (Transform child in colorListParent)
        {
            if (!child.CompareTag("TargetUI"))
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Colors color in Enum.GetValues(typeof(Colors)))
        {
            if (statusManager.IsColorActive(color))
            {
                // Instantiate a new UI Text object
                GameObject colorTextObj = Instantiate(colorTextPrefab, colorListParent);

                // Get the Text or TMP component
                TextMeshProUGUI textComponent = colorTextObj.GetComponent<TextMeshProUGUI>();

                // Set the text and color
                textComponent.text = color.ToString();
                textComponent.color = GetColorFromEnum(color);
            }
        }
    }

    void UpdateColors()
    {
        yellowImage.color = statusManager.IsColorActive(Colors.Yellow) ? Color.yellow : Color.gray;
        redImage.color = statusManager.IsColorActive(Colors.Red) ? Color.red : Color.gray;
        blueImage.color = statusManager.IsColorActive(Colors.Blue) ? Color.blue : Color.gray;

        orangeImage.color = statusManager.IsColorActive(Colors.Orange) ? GetColorFromEnum(Colors.Orange) : Color.gray;
        greenImage.color = statusManager.IsColorActive(Colors.Green) ? GetColorFromEnum(Colors.Green) : Color.gray;
        purpleImage.color = statusManager.IsColorActive(Colors.Purple) ? GetColorFromEnum(Colors.Purple) : Color.gray;

        PopulateColorList();
    }

    Color GetColorFromEnum(Colors color)
    {
        switch (color)
        {
            case Colors.Red: return Color.red;
            case Colors.Yellow: return Color.yellow;
            case Colors.Blue: return Color.blue;
            case Colors.Green: return Color.green;
            case Colors.Orange: return new Color(1.0f, 0.5f, 0.0f); // RGB for Orange
            case Colors.Purple: return new Color(0.5f, 0.0f, 0.5f); // RGB for Purple
            default: return Color.white;
        }
    }
}
