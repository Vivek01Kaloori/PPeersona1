using UnityEngine;
using System.Collections.Generic;

public class ArcMovement : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject Cube1;
    public GameObject Cube2;
    public int numberOfSpheres = 100;
    public float sphereSpeed = 1.0f;
    public float cylinderRadius = 2.0f;
    public float arcRadius = 5.0f; // Variable for arc radius

    private List<GameObject> spheres = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private Vector3 direction;
    private float arcLength;

    void Start()
    {
        Vector3 cube1Pos = Cube1.transform.position;
        Vector3 cube2Pos = Cube2.transform.position;

        // Calculate the direction and length of the arc
        direction = (cube2Pos - cube1Pos).normalized;
        arcLength = Vector3.Distance(cube1Pos, cube2Pos);

        // Generate initial positions for the spheres
        GenerateInitialPositions();

        // Generate spheres at initial positions
        GenerateSpheres();
    }

    void GenerateInitialPositions()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            float t = (float)i / numberOfSpheres; // evenly distribute along the arc
            float randomTheta = Random.Range(0f, 2f * Mathf.PI);
            float randomRadius = Random.Range(0f, cylinderRadius);
            float randomLength = t * arcLength;

            Vector3 randomOffset = new Vector3(
                randomRadius * Mathf.Cos(randomTheta),
                randomRadius * Mathf.Sin(randomTheta),
                randomLength
            );

            Vector3 pointOnArc = Cube1.transform.position + direction * randomLength;
            Vector3 arcOffset = Quaternion.FromToRotation(Vector3.forward, direction) * new Vector3(randomOffset.x, randomOffset.y, 0);
            Vector3 initialPosition = pointOnArc + arcOffset * arcRadius; // Apply arc radius

            initialPositions.Add(initialPosition);
        }
    }

    void GenerateSpheres()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
            sphere.transform.position = initialPositions[i];
            spheres.Add(sphere);
        }
    }

    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            // Calculate new position along the arc
            float moveDistance = sphereSpeed * Time.deltaTime;
            Vector3 currentPos = spheres[i].transform.position;

            Vector3 currentPosOnLine = ProjectPointOnLine(Cube1.transform.position, direction, currentPos);
            float currentLength = Vector3.Distance(Cube1.transform.position, currentPosOnLine);
            float newLength = (currentLength + moveDistance) % arcLength;

            float randomTheta = Random.Range(0f, 2f * Mathf.PI);
            float randomRadius = Random.Range(0f, cylinderRadius);

            Vector3 randomOffset = new Vector3(
                randomRadius * Mathf.Cos(randomTheta),
                randomRadius * Mathf.Sin(randomTheta),
                newLength
            );

            Vector3 pointOnArc = Cube1.transform.position + direction * newLength;
            Vector3 arcOffset = Quaternion.FromToRotation(Vector3.forward, direction) * randomOffset;
            Vector3 newPosition = pointOnArc + arcOffset * arcRadius; // Apply arc radius

            // Move the sphere towards its new position
            spheres[i].transform.position = newPosition;
        }
    }

    Vector3 ProjectPointOnLine(Vector3 lineStart, Vector3 lineDirection, Vector3 point)
    {
        return lineStart + Vector3.Project(point - lineStart, lineDirection);
    }
}
