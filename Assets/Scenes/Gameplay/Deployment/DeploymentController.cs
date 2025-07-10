using UnityEngine;

public class DeploymentController : MonoBehaviour
{
    
    public bool isDeploymentPhase = true;
    public GameObject waypointMarkerPrefab; // Prefab for the waypoint marker


    public GameObject deployButton;

    // Deployment data
    public const int shipCount = 3;
    public Transform[] deploymentPoints; // Points where ships can be deployed
    public int deploymentArrPtr = 0; // Pointer to the next deployment point
    public GameObject[] waypoints; // Array to hold waypoint markers

    void Start()
    {
        deploymentPoints = new Transform[shipCount];
        waypoints = new GameObject[shipCount];
        deployButton = GameObject.Find("DeployButton");
        if (deployButton == null)
        {
            Debug.LogError("DeployButton not found in the scene.");
        }
        deployButton.SetActive(false); // Hide deploy button initially
    }

    void Update()
    {
        if (!isDeploymentPhase)
        {
            return;
        }

    }


    public void PlaceWaypointMarker(Vector3 position)
    {
        // Logic to place a waypoint marker at the specified position
        if (waypointMarkerPrefab == null)
        {
            Debug.LogError("Waypoint marker prefab is not assigned.");
            return;
        }

        deploymentArrPtr++;

        if (deploymentArrPtr == shipCount)
        {
            // Notify that deployment phase can be ended
            Debug.Log("All waypoints placed. Deployment phase can be ended.");
            deployButton.SetActive(true); // Show deploy button
        }


        if (deploymentArrPtr >= shipCount)
        {
            deploymentArrPtr = 0; // Reset pointer
        }
        Destroy(waypoints[deploymentArrPtr]); // Destroy previous waypoint if exists
        waypoints[deploymentArrPtr] = Instantiate(waypointMarkerPrefab, position, Quaternion.identity);


    }

}
