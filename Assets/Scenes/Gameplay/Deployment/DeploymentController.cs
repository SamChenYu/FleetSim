using UnityEngine;
using System.Collections;
public class DeploymentController : MonoBehaviour
{


    public bool isDeploymentPhase = true; // Flag to indicate if in deployment phase

    public GameController gameController;
    public GameObject waypointMarkerPrefab; // Prefab for the waypoint marker

    // UI Elements
    public GameObject deployButton;
    public GameObject title;

    // Deployment data
    public Material highlightMaterial; // Material used to highlight the waypoint marker
    public Transform[] deploymentPoints; // Points where ships can be deployed
    public int deploymentArrPtr; // Pointer to the next deployment point
    public GameObject[] waypoints; // Array to hold waypoint markers



    // Source Models
    public GameObject arquitensPrefab;
    public GameObject terminusPrefab;

    public GameObject dreadnoughtPrefab;



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

        deploymentArrPtr++;
        if (deploymentArrPtr == shipCount - 1)
        {
            // Notify that deployment phase can be ended
            Debug.Log("All waypoints placed. Deployment phase can be ended.");
            deployButton.SetActive(true); // Show deploy button
        }

        Destroy(waypoints[deploymentArrPtr]); // Destroy previous waypoint if exists


        if(shipData[deploymentArrPtr] == null || shipData[deploymentArrPtr].shipPrefab == null) {
            Debug.LogError("Ship data or prefab is not assigned for index: " + deploymentArrPtr);
            return;
        }
        // Spawn in the waypoint ship marker
        waypoints[deploymentArrPtr] = Instantiate(shipData[deploymentArrPtr].shipPrefab, position + new Vector3(0.0f, 2.0f, 0.0f), shipData[deploymentArrPtr].shipPrefab.transform.rotation); // Create the new waypoint marker

        // Highlight the marker 
        Renderer[] renderers = waypoints[deploymentArrPtr].GetComponentsInChildren<Renderer>();
        if (renderers.Length != 0)
        {
            foreach (Renderer rend in renderers)
            {
                Material[] highlightMats = new Material[rend.materials.Length];
                for (int i = 0; i < highlightMats.Length; i++)
                {
                    highlightMats[i] = highlightMaterial;
                }
                rend.materials = highlightMats;
            }
        }



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

                case "Capital Ship":
                    spawnPosition.z = -300f;
                    playerShips[i] = Instantiate(arquitensPrefab, spawnPosition, deploymentPoints[i].transform.rotation);
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position + new Vector3(0.0f, 2.0f, 0.0f), 1f + 0.5f)); // Warp
                    break;

                case "Corvette":
                    spawnPosition.z = -300f;
                    playerShips[i] = Instantiate(terminusPrefab, spawnPosition, deploymentPoints[i].transform.rotation);
                    // Start warp-in animation
                    StartCoroutine(WarpIn(playerShips[i], deploymentPoints[i].position, 1.0f + Random.Range(0.0f, 0.5f)));
                    break;

                case "Dreadnought":
                    spawnPosition.z = -300f;
                    playerShips[i] = Instantiate(dreadnoughtPrefab, spawnPosition, deploymentPoints[i].transform.rotation);
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
        isDeploymentPhase = false; // End deployment phase
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
