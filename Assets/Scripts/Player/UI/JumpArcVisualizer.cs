using UnityEngine;

[ExecuteAlways]
public class JumpArcVisualizer : MonoBehaviour
{
    public PlayerStats Stats;

    private void OnDrawGizmos()
    {
        if (Stats == null) return;

        Stats.CalculateValues(); // Make sure values are up to date

        Vector3 startPos = transform.position;
        int resolution = Stats.ArcResolution;
        float timeStep = Stats.TimeTillJumpApex * 2f / resolution;

        // Draw Max Jump Arc
        if (Stats.ShowWalkJumpArc)
        {
            DrawJumpArc(startPos, Stats.InitialJumpVelocity, Stats.Gravity, resolution, Stats.MaxJumpArcColor);
        }

        // Draw Min Jump Arc
        if (Stats.ShowMinJumpArc)
        {
            float minVelocity = Mathf.Sqrt(2f * Mathf.Abs(Stats.Gravity) * Stats.MinJumpHeight);
            DrawJumpArc(startPos, minVelocity, Stats.Gravity, resolution, Stats.MinJumpArcColor);
        }
    }

    private void DrawJumpArc(Vector3 startPos, float velocity, float gravity, int steps, Color color)
    {
        Gizmos.color = color;
        Vector3 prevPoint = startPos;

        for (int i = 1; i <= steps; i++)
        {
            float t = i * (Stats.TimeTillJumpApex * 2f / steps);
            float x = t * Stats.MaxWalkSpeed * 0.5f; // for visual spacing
            float y = velocity * t + 0.5f * gravity * t * t;

            Vector3 nextPoint = startPos + new Vector3(x, y, 0f);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
