using UnityEngine;

namespace Medfable.Controller
{
    public class PatrolController : MonoBehaviour
    {
        [SerializeField]
        private float gizmoRadius = 0.4f;

        // Used to visualise all the checkpoints and paths on screen for the AI to use
        private void OnDrawGizmos()
        {
            for (int currentCPIndex = 0; currentCPIndex < transform.childCount; currentCPIndex++)
            {
                int nextCPIndex = GetNextCPIndex(currentCPIndex);
                DrawCheckpoint(currentCPIndex);
                DrawCheckpointLines(currentCPIndex, nextCPIndex);
            }
        }

        // Draw the paths between every checkpoint
        private void DrawCheckpointLines(int currentCPIndex, int nextCPIndex)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GetCheckpoint(currentCPIndex), GetCheckpoint(nextCPIndex));
        }

        // Draw the checkpoint in which the AI moves to
        private void DrawCheckpoint(int checkpointIndex)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetCheckpoint(checkpointIndex), gizmoRadius);
        }

        // Retrieve the next checkpoint index that is conntected to current checkpoint
        public int GetNextCPIndex(int currentCPIndex)
        {
            return (currentCPIndex + 1) % transform.childCount;
        }

        // Retrieve a checkpoint by getting their position in the scene
        public Vector3 GetCheckpoint(int checkpointIndex)
        {
            return transform.GetChild(checkpointIndex).position;
        }

    }
}
