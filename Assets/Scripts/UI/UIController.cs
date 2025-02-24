using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private HealthSystem playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private StatusManager statusManager;
    [SerializeField] private Image yellowImage;
    [SerializeField] private Image redImage;
    [SerializeField] private Image blueImage;
    [SerializeField] private Image orangeImage;
    [SerializeField] private Image greenImage;
    [SerializeField] private Image purpleImage;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = PlayerInput.Instance.GetComponent<HealthSystem>();
        playerHealth.OnLifeChange += UpdateHealthText;
        playerHealth.OnDead += DisplayDeathScreen;

        statusManager = StatusManager.Instance;
        statusManager.OnColorsChange += UpdateColors;
   }

    void DisplayDeathScreen()
    {

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
