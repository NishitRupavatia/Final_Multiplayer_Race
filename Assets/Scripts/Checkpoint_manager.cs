using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public TextMeshProUGUI checkpointMessage; // Assign this in the Inspector

    private void Start()
    {
        checkpointMessage.gameObject.SetActive(false); // Hide the message initially
    }

    public void ShowCheckpointMessage()
    {
        Debug.Log("Displaying Checkpoint Message");
        if (checkpointMessage != null)
        {
            checkpointMessage.gameObject.SetActive(true);
            Invoke("HideCheckpointMessage", 2f); // Hide message after 2 seconds
        }
    }

    private void HideCheckpointMessage()
    {
        Debug.Log("Hiding Checkpoint Message");
        if (checkpointMessage != null)
        {
            checkpointMessage.gameObject.SetActive(false);
        }
    }
}