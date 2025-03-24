using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance { get; private set; }
    
    // Stores colors set when targets are hit
    private HashSet<Colors> activeColors = new();
    // track changeable gameobjects status by color
    private Dictionary<Colors, List<IStatusChangeable>> statusChangeables = new();
    public Action OnColorsChange;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ActivateColor(Colors color)
    {
        if (activeColors.Add(color)) // Only adds if not already in the set
        {
            SoundType colorSoundType = AudioManager.Instance.GetSoundTypeByColor(color);
            AudioManager.Instance.PlaySoundsSequentially(colorSoundType, SoundType.WallsAndTraps, SoundType.Active);
            SetAllStatuses(color, true);
            // Set secondary colors only if called with primary color 
            if (color == Colors.Yellow || color == Colors.Red || color == Colors.Blue)
            {
                SetSecondaryColors();
            }
            OnColorsChange?.Invoke();
        }
    }

    public void DeactivateColor(Colors color)
    {
        if (activeColors.Remove(color))
        {
            SoundType colorSoundType = AudioManager.Instance.GetSoundTypeByColor(color);
            AudioManager.Instance.PlaySoundsSequentially(colorSoundType, SoundType.Deactivated);
            SetAllStatuses(color, false);
            if (color == Colors.Yellow || color == Colors.Red || color == Colors.Blue)
            {
                UnsetSecondaryColors(color);
            }
            OnColorsChange?.Invoke();
        }
       
    }

    private void SetSecondaryColors()
    {
        bool isYellowActive = IsColorActive(Colors.Yellow);
        bool isBlueActive = IsColorActive(Colors.Blue);
        bool isRedActive = IsColorActive(Colors.Red);

        if (isRedActive && isYellowActive)
        {
            // Set Orange
            ActivateColor(Colors.Orange);
        } 
        
        if (isBlueActive && isYellowActive)
        {
            // Set Green
            ActivateColor(Colors.Green);
        }
        
        if (isBlueActive && isRedActive)
        {
            // Set Purple
            ActivateColor(Colors.Purple);
        }
        
        if (isBlueActive && isRedActive && isYellowActive)
        {
            // Set Brown
            //ActivateColor(Colors.Brown);
        }
    }

    private void UnsetSecondaryColors(Colors color)
    {
        //DeactivateColor(Colors.Brown);
        switch (color) 
        {
            case Colors.Yellow:
                // unSet Orange
                DeactivateColor(Colors.Orange);
                // unSet Green
                DeactivateColor(Colors.Green);
                break;
            case Colors.Red:
                // unSet Orange
                DeactivateColor(Colors.Orange);
                // unSet Purple
                DeactivateColor(Colors.Purple);
                break;
            case Colors.Blue:
                // unSet Green
                DeactivateColor(Colors.Green);
                // unSet Purple
                DeactivateColor(Colors.Purple);
                break;
            default:
                break;
        }
    }

    // Register all changeables to the dictionary
    public void RegisterStatusChangeable(IStatusChangeable statusChangeable, Colors color)
    {
        if (!statusChangeables.ContainsKey(color))
        {
            statusChangeables[color] = new List<IStatusChangeable>();
        }
        statusChangeables[color].Add(statusChangeable);
    }

    private void SetAllStatuses(Colors color, bool status)
    {
        if (statusChangeables.TryGetValue(color, out List<IStatusChangeable> changeAbles))
        {
            foreach (var statusChangeable in changeAbles)
            {
                // if status does not match update target
                if(statusChangeable.GetStatus() != status)
                {
                    statusChangeable.SetStatus(status);
                }
            }
        }
    }

    public bool IsColorActive(Colors color) => activeColors.Contains(color);

}
