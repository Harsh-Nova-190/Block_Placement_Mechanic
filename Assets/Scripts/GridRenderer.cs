using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1f;
    public Material lineMaterial;

    private void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        for (int x = 0; x <= gridSize; x++)
        {
            CreateLine(
                new Vector3(x * cellSize, 0, 0),
                new Vector3(x * cellSize, 0, gridSize * cellSize)
            );
        }

        for (int z = 0; z <= gridSize; z++)
        {
            CreateLine(
                new Vector3(0, 0, z * cellSize),
                new Vector3(gridSize * cellSize, 0, z * cellSize)
            );
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine");
        lineObj.transform.parent = transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.positionCount = 2;
        start += transform.position;
        end += transform.position;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}