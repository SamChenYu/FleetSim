using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public int size = 500;
    public int divisions = 10;
    public float lineWidth = 1.0f;
    public Material lineMaterial;

    void Start()
    {
        float step = (float)size / divisions;
        float halfSize = size / 2f;

        for (int i = 0; i <= divisions; i++)
        {
            float offset = -halfSize + i * step;

            // Vertical lines (Z axis)
            DrawLine(new Vector3(offset, 1, -halfSize), new Vector3(offset, 1, halfSize));

            // Horizontal lines (X axis)
            DrawLine(new Vector3(-halfSize, 1, offset), new Vector3(halfSize, 1, offset));
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject("GridLine");
        line.transform.parent = transform;

        var lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { start, end });
        lr.startWidth = lr.endWidth = lineWidth;
        lr.material = lineMaterial;
        lr.useWorldSpace = true;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
    }
}
