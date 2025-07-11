using UnityEngine;
using System.Collections;

public class DeploymentController : MonoBehaviour
{

    public GameController gameController;
    public GameObject waypointMarkerPrefab; // Prefab for the waypoint marker
    private bool isDeploymentPhase = true; 

    // UI Elements
    public GameObject deployButton;
    public GameObject title;

    // Deployment data
    public Transform[] deploymentPoints; // Points where ships can be deployed
    public int deploymentArrPtr = 0; // Pointer to the next deployment point
    public GameObject[] waypoints; // Array to hold waypoint markers



    // Source Models
    public GameObject arquitensPrefab;
    public GameObject terminusPrefab;
    


    void Start()
    {   
        // Find UI elements
        deployButton = GameObject.Find("DeployButton");
        if (deployButton == null) Debug.LogError("DeployButton not found in the scene.");
        title = GameObject.Find("Title");
        if (title == null) Debug.LogError("Title not found in the scene.");
        // Find GameController
        gameController = Object.FindFirstObjectByType<GameController>();
        if (gameController == null) Debug.LogError("GameController component not found in the scene.");


        // Initialization       
        deploymentPoints = new Transform[GameController.shipCount];
        waypoints = new GameObject[GameController.shipCount];


        deployButton.SetActive(false); // Hide deploy button initially
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

        if (deploymentArrPtr == GameController.shipCount)
        {
            // Notify that deployment phase can be ended
            Debug.Log("All waypoints placed. Deployment phase can be ended.");
            deployButton.SetActive(true); // Show deploy button
        }


        if (deploymentArrPtr >= GameController.shipCount)
        {
            deploymentArrPtr = 0; // Reset pointer
        }
        Destroy(waypoints[deploymentArrPtr]); // Destroy previous waypoint if exists
        waypoints[deploymentArrPtr] = Instantiate(waypointMarkerPrefab, position, Quaternion.identity);
        deploymentPoints[deploymentArrPtr] = waypoints[deploymentArrPtr].transform; // Store the deployment point
        Debug.Log("Waypoint marker placed at: " + position + ". Deployment point index: " + deploymentArrPtr);


    }


    public void EndDeploymentPhase()
    {


        // Spawn the ships at the deployment points
        GameObject[] playerShips = new GameObject[GameController.shipCount];
        for (int i = 0; i < GameController.shipCount; i++)
        {
            if (deploymentPoints[i] == null)
            {
                Debug.LogError("Deployment point " + i + " is null. Cannot spawn ship.");
                continue;
            }
            Vector3 spawnPosition = deploymentPoints[i].position;
            switch (GameController.playerShips[i][0])
            {
                case "Battleship":
                    spawnPosition.z = 300f;       
                    playerShips[i] = Instantiate(arquitensPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position, 1f+0.5f)); // Warp
                    break;
                case "Fighter":
                    
                    spawnPosition.x -= 2.45f; // terminus model has some slight offset
                    spawnPosition.z = 300f;
                    playerShips[i] = Instantiate(terminusPrefab, spawnPosition, Quaternion.Euler(-90, 0, 180));
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position, 1.0f));

                    break;
                default:
                    Debug.LogError("Unknown ship type: " + GameController.playerShips[i][0]);
                    break;
            }
        }
        // Destroy the waypoint markers

        for(int i = 0; i < GameController.shipCount; i++)
        {
            if (waypoints[i] != null)
            {
                Destroy(waypoints[i]);
            }
        }

        // Hide UI
        deployButton.SetActive(false);
        title.SetActive(false);
    }


    private IEnumerator WarpIn(GameObject ship, Vector3 targetPos, float duration)
    {
        Vector3 startPos = ship.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            ship.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        ship.transform.position = targetPos;
    }




    void Update()
    {
        if (isDeploymentPhase)
        {
            return;
        }



    }

}
