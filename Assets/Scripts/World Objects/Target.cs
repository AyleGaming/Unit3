using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, IPuzzlePiece, IStatusChangeable
{
    public UnityEvent TargetHit = new UnityEvent();
    [SerializeField] private bool isActiveStatus = false;

    private Color blackColor = Color.black;
    private Color yellowColor = Color.yellow;
    private Color blueColor = Color.blue;
    private Color redColor = Color.red;

    [SerializeField] private Color borderColor;
    [SerializeField] private Colors targetColor;
    [SerializeField] private AudioClip targetHitSound;


    public bool IsCorrect()
    {
        return isActiveStatus;
    }

    public void LinkToPuzzle(Puzzle p)
    {
    }

    private void Start()
    {
        SetTargetColor();
        StatusManager.Instance?.RegisterStatusChangeable(this, targetColor);
    }

    private void SetTargetColor()
    {
        Transform border = transform.GetChild(0); // First child is border which will have a color
        Renderer borderRenderer = border.GetComponent<Renderer>();

        if (borderRenderer != null)
        {
            borderColor = borderRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Only allow bullet to trigger target activation
        if (collider.CompareTag("Bullet"))
        {
            if (targetHitSound != null)
            {
                AudioManager.Instance.PlaySound(targetHitSound);
            }
            ToggleTarget(!isActiveStatus);
            TargetHit.Invoke();
            Destroy(collider.gameObject); // remove bullet so it doesn't hit other targets on riccochet
        }
    }

    public void ToggleTarget(bool status)
    {
        SetStatus(status);

        // set global color value
        if (status)
        {
            StatusManager.Instance.ActivateColor(targetColor);
        }
        else
        {
            StatusManager.Instance.DeactivateColor(targetColor);
        }
    }

    // Update color visual on target
    private void ChangeRingColors()
    {
        foreach (Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color currentColor = renderer.material.color;

                if (IsColorMatch(currentColor, borderColor))
                {
                    renderer.material.color = blackColor;
                }
                else if (IsColorMatch(currentColor, blackColor))
                {
                    renderer.material.color = SwapTargetColors();
                }
            }
        }
    }

    private Color SwapTargetColors()
    {
        if (IsColorMatch(borderColor, yellowColor))
            return yellowColor;
        if (IsColorMatch(borderColor, blueColor))
            return blueColor;
        if (IsColorMatch(borderColor, redColor))
            return redColor;

        return borderColor;
    }

    // Checks colors and determines what color it is
    private bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
    }

    public void SetStatus(bool status)
    {
        isActiveStatus = status;
        ChangeRingColors();
    }

    public bool GetStatus()
    {
        return isActiveStatus;
    }
}
