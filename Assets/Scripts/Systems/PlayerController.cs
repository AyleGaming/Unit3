using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject blaster; // Assign in Inspector
    public bool isBlasterActive => blaster.activeSelf;

    private void Start()
    {
        if (blaster != null)
            blaster.SetActive(false); // Ensure blaster is initially hidden
    }
}
