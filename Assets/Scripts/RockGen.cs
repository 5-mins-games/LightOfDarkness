using UnityEngine;

public class RockGen : MonoBehaviour
{
    public Mesh rockMesh;
    public GameObject pointPrefab;
    [Range(10, 1000)]
    public int pointCount = 1000;
    [Range(1, 100)]
    public int distance = 10;

    public bool animate = true;

    GameObject[] points;
    Vector3[] pointPos;

    private void Start()
    {
        if (rockMesh == null || pointPrefab == null)
            return;

        CreatePoints();
        CreatePointCloud();
    }

    private void Update()
    {
        if (animate)
        {
            AnimatePoints();
        }
        else
        {
            DisablePoints();
        }
    }

    void CreatePoints()
    {
        int interval = rockMesh.vertexCount / pointCount;
        int vertexCount = rockMesh.vertexCount;

        pointPos = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            pointPos[i] = rockMesh.vertices[(i * interval) % vertexCount] * distance;
        }

        Vector3 center = transform.parent.position;
        for (int i = 0; i < pointCount; i++)
        {
            pointPos[i] += center;
        }
    }

    void CreatePointCloud()
    {
        points = new GameObject[pointCount];

        int i = 0;
        foreach (Vector3 pos in pointPos)
        {
            GameObject go = Instantiate(pointPrefab, transform, false);
            go.transform.position = pos;

            points[i] = go;
            i++;
        }
    }

    void AnimatePoints()
    {
        for (int i = 0; i < pointCount; i++)
        {
            points[i].SetActive(true);

            Vector3 pos = pointPos[i];
            pos.x = pointPos[i].x + Mathf.Sin(Time.time * .5f + i) * .5f;
            pos.z = pointPos[i].z + Mathf.Cos(Time.time * .5f + i) * .5f;

            points[i].transform.position = pos;
        }
    }

    void DisablePoints()
    {
        for (int i = 0; i < pointCount; i++)
        {
            points[i].SetActive(false);
        }
    }
}
