using UnityEngine;
using TMPro;

public class PlayerShipController : MonoBehaviour
{


    // Ship data
    public GameObject[] playerShips;
    public ShipData[] shipData;
    public int shipCount = 3;
    public int currentShipSelected = -1;


    // UI elements
    public GameObject gameplayUI;
    public GameObject shipLabelPrefab;
    public GameObject[] shipLabels;
    public Vector2 currentLabelPosition = new Vector2(0, 160); // Start more centered
    public Material highlightMaterial;


    void Awake()
    {
        // Harcoded the ship selection for now
        shipData = new ShipData[shipCount];
        shipData[2] = ScriptableObject.CreateInstance<ShipData>();
        shipData[2].Initialize("Fighter", "Terminus 1");
        shipData[1] = ScriptableObject.CreateInstance<ShipData>();
        shipData[1].Initialize("Fighter", "Terminus 2");
        shipData[0] = ScriptableObject.CreateInstance<ShipData>();
        shipData[0].Initialize("Battleship", "Arquitens");
    }

    public void SelectShip(int shipIndex)
    {


        // Used by PlayerControls to select a ship
        if (shipIndex < 0 || shipIndex >= shipCount || playerShips == null || playerShips.Length == 0)
        {
            Debug.LogError("Invalid ship index selected: " + shipIndex);
            return;
        }
        else
        {
            // Reset the previously selected ship's color
            if (currentShipSelected != -1) shipLabels[currentShipSelected].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            // Highlight the selected ship label
            currentShipSelected = shipIndex;
            Debug.Log("Selected ship: " + currentShipSelected);
            Debug.Log(currentShipSelected);
            shipLabels[shipIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow; // Highlight selected ship label
            // Highlight the selected ship in the game world
            Renderer renderer = playerShips[currentShipSelected].GetComponentInChildren<Renderer>();

            if (renderer == null)
            {
                Debug.LogWarning("No renderer found on selected ship.");
                return;
            }

            renderer.material = highlightMaterial;
        }
    }

    void Start()
    {
        shipLabels = new GameObject[shipCount];
        gameplayUI.SetActive(false);
    }

    void Update()
    {
    }


    public void ReceiveShipsFromDeployment(GameObject[] deploymentShips)
    {
        playerShips = deploymentShips;
        Debug.Log("Received ships from deployment. Total ships: " + playerShips.Length);


        // Initialize ship labels in the UI
        gameplayUI.SetActive(true);

        for (int i = 0; i < shipData.Length; i++)
        {
            GameObject shipLabel = Instantiate(shipLabelPrefab);
            shipLabel.transform.SetParent(gameplayUI.transform, false);
            Debug.Log("Coordinates of ship label: " + currentLabelPosition);
            RectTransform rect = shipLabel.GetComponent<RectTransform>();
            rect.anchoredPosition = currentLabelPosition;

            TextMeshProUGUI labelText = shipLabel.GetComponentInChildren<TextMeshProUGUI>();
            labelText.text = shipData[i].name + " (" + shipData[i].baseSpeed + " speed)";
            shipLabels[i] = shipLabel;
            currentLabelPosition += new Vector2(0, -50); // Move label down
        }



    }
}