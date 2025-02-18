using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, IPuzzlePiece
{
    public UnityEvent TargetHit = new UnityEvent();
    private bool targetActive = false;

    public Color whiteColor = Color.white;
    public Color blackColor = Color.black;

    public Color yellowColor = Color.yellow;
    public Color blueColor = Color.blue;
    public Color redColor = Color.red;

    [SerializeField] private Color borderColor; 

    public bool IsCorrect()
    {
        return targetActive;
    }

    public void LinkToPuzzle(Puzzle p)
    {
    }

    private void Start()
    {
        SetTargetColor();
    }

    private void SetTargetColor()
    {
        Transform border = transform.GetChild(0); // First child is border which will have a color
        Renderer borderRenderer = border.GetComponent<Renderer>();

       



        if (borderRenderer != null)
        {
            borderColor = borderRenderer.material.color;
            Debug.Log(borderColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!!");
        // make sure npcs dont pick up key
        if (other.CompareTag("Bullet"))
        {
            ToggleTarget();
            TargetHit.Invoke();
            Destroy(other.gameObject);
            Debug.Log("Target hit!");
        }
    }

    public void ToggleTarget()
    {
        if (targetActive == false)
        {
            targetActive = true;
        }
        else
        {
            targetActive = false;
        }
        ChangeRingColors();
    }

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
                    renderer.material.color = GetSwapColorFromBorder();
                }
            }
        }
    }

    private Color GetSwapColorFromBorder()
    {
        if (IsColorMatch(borderColor, yellowColor))
            return yellowColor;
        if (IsColorMatch(borderColor, blueColor))
            return blueColor;
        if (IsColorMatch(borderColor, redColor))
            return redColor;

        return borderColor;
    }

    private bool IsColorMatch(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
    }
}
