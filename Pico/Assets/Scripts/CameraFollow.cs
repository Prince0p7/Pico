using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> Players;
    [SerializeField] Transform CamPivot;
    private void Awake()
    {
        CamPivot.position = calculateCentroid(Players);
    }
    private void Update()
    {
        CamPivot.position = calculateCentroid(Players);
    }
    Vector3 calculateCentroid(List<Transform> centerPoints)
    {
        Vector3 centroid = new Vector3(0, 0, 0);
        float numPoints = centerPoints.Count;

        foreach(Transform point in centerPoints)
        {
            centroid += point.position;
        }

        centroid /= numPoints;

        return centroid;
    }
}