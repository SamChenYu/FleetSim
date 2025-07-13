using UnityEngine;
using System.Collections;

public class DeploymentController : MonoBehaviour
{

    public GameController gameController;
    public GameObject waypointMarkerPrefab; // Prefab for the waypoint marker

    // UI Elements
    public GameObject deployButton;
    public GameObject title;

    // Deployment data
    public Transform[] deploymentPoints; // Points where ships can be deployed
    public int deploymentArrPtr; // Pointer to the next deployment point
    public GameObject[] waypoints; // Array to hold waypoint markers



    // Source Models
    public GameObject arquitensPrefab;
    public GameObject terminusPrefab;



    // Ship data
    public int shipCount;
    public ShipData[] shipData;



    void Start() { 
        // Find UI elements
        deployButton = GameObject.Find("DeployButton");
        if (deployButton == null) Debug.LogError("DeployButton not found in the scene.");
        title = GameObject.Find("Title");
        if (title == null) Debug.LogError("Title not found in the scene.");
        // Find GameController
        gameController = Object.FindFirstObjectByType<GameController>();
        if (gameController == null) Debug.LogError("GameController component not found in the scene.");
        // Initialization       
        shipCount = gameController.playerShipController.shipCount;
        shipData = gameController.playerShipController.shipData;
        if(shipData == null || shipData.Length < shipCount) {
            Debug.LogError("Ship data is not properly initialized.");
            return;
        }
        deploymentArrPtr = -1;
        deploymentPoints = new Transform[shipCount];
        waypoints = new GameObject[shipCount];
        

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
        if (deploymentArrPtr == shipCount - 1)
        {
            // Notify that deployment phase can be ended
            Debug.Log("All waypoints placed. Deployment phase can be ended.");
            deployButton.SetActive(true); // Show deploy button

        }


        Destroy(waypoints[deploymentArrPtr]); // Destroy previous waypoint if exists
        waypoints[deploymentArrPtr] = Instantiate(waypointMarkerPrefab, position, Quaternion.identity); // Create the new waypoint marker
        deploymentPoints[deploymentArrPtr] = waypoints[deploymentArrPtr].transform; // Store the deployment point

        Debug.Log("Waypoint marker placed at: " + position + ". Deployment point index: " + deploymentArrPtr);

        if (deploymentArrPtr == shipCount - 1)
        {
            deploymentArrPtr = -1; // Reset pointer for next deployment phase
        }
    }
    


    public void EndDeploymentPhase()
    {
        // Spawn the ships at the deployment points
        GameObject[] playerShips = new GameObject[shipCount];
        for (int i = 0; i < shipCount; i++)
        {
            if (deploymentPoints[i] == null)
            {
                Debug.LogError("Deployment point " + i + " is null. Cannot spawn ship.");
                continue;
            }
            Vector3 spawnPosition = deploymentPoints[i].position;
            Debug.Log(shipData[i]);
            switch (shipData[i].type)
            {

                case "Battleship":
                    spawnPosition.z = 300f;
                    deploymentPoints[i].position += new Vector3(0.0f, 2.0f, 0.0f);
                    playerShips[i] = Instantiate(arquitensPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position, 1f + 0.5f)); // Warp
                    break;

                case "Fighter":
                    spawnPosition.z = 300f;
                    spawnPosition.x -= 4.90f; // terminus model has some slight offset -> this is at 2x size
                    // Adjust the deploymentPoint because the terminus model is not centered
                    deploymentPoints[i].position += new Vector3(-4.90f, 0.0f, 0.0f);
                    playerShips[i] = Instantiate(terminusPrefab, spawnPosition, Quaternion.Euler(-90, 0, 180));
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position, 1.0f + Random.Range(0.0f, 0.5f)));
                    break;

                default:
                    Debug.LogError("<DeploymentController> Unknown ship type: " + shipData[i].type);
                    break;
            }
        }
        // Destroy the waypoint markers=
        for (int i = 0; i < shipCount; i++)
        {
            if (waypoints[i] != null)
            {
                Destroy(waypoints[i]);
            }
        }
        // Hide UI
        deployButton.SetActive(false);
        title.SetActive(false);
        // Pass the ship data to the PlayerShipController
        gameController.playerShipController.ReceiveShipsFromDeployment(playerShips);
        Debug.Log("Deployment phase ended. Ships deployed: " + playerShips.Length);
    }


    private IEnumerator WarpIn(GameObject ship, Vector3 targetPos, float duration) {
        // Coroutine to animate the ship's warp-in effect
        Vector3 startPos = ship.transform.position;
        float elapsed = 0f;
        while (elapsed < duration) {
            ship.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ship.transform.position = targetPos;
    }
}
