using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition = Vector3.zero;
    public static Quaternion lastCheckpointRotation = Quaternion.identity;

    public CheckpointManager checkpointManager; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update to the latest checkpoint
            lastCheckpointPosition = transform.position;
            lastCheckpointRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Store horizontal rotation

            Debug.Log($"Checkpoint Triggered: Position {lastCheckpointPosition}, Rotation {lastCheckpointRotation}");

            if (checkpointManager != null)
            {
                checkpointManager.ShowCheckpointMessage();
            }
        }
    }
}