/* using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameObject player; // Assign your player/car GameObject in the Inspector

    private void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartFromCheckpoint();
        }
    }

    public void RestartFromCheckpoint()
    {
        Debug.Log("Restarting from checkpoint...");
        if (Checkpoint.lastCheckpointPosition != Vector3.zero)
        {
            Debug.Log($"Restarting at {Checkpoint.lastCheckpointPosition}");
            player.transform.position = Checkpoint.lastCheckpointPosition;
            player.transform.rotation = Quaternion.Euler(0, Checkpoint.lastCheckpointRotation.eulerAngles.y, 0); // Keep horizontal

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            Debug.LogWarning("No checkpoint position set.");
        }
    }
} */


using UnityEngine;
using System.Collections.Generic;

public class RestartButton : MonoBehaviour
{
    public List<GameObject> players; // Assign your player/car GameObjects in the Inspector

    private void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartFromCheckpoint();
        }
    }

    public void RestartFromCheckpoint()
    {
        Debug.Log("Restarting from checkpoint...");

        foreach (var player in players)
        {
            if (Checkpoint.lastCheckpointPosition != Vector3.zero)
            {
                Debug.Log($"Restarting {player.name} at {Checkpoint.lastCheckpointPosition}");
                player.transform.position = Checkpoint.lastCheckpointPosition;
                player.transform.rotation = Quaternion.Euler(0, Checkpoint.lastCheckpointRotation.eulerAngles.y, 0); // Keep horizontal

                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                Debug.LogWarning("No checkpoint position set.");
            }
        }
    }
}
