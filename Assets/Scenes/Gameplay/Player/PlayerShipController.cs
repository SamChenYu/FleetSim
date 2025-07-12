using UnityEngine;
using TMPro;

public class PlayerShipController : MonoBehaviour
{


    // Ship data
    public GameObject[] playerShips;
    public ShipData[] shipData;
    public const int shipCount = 3;
    public int currentShipSelected = -1;


    // UI elements
    public GameObject gameplayUI;
    public GameObject shipLabelPrefab;
    public Vector2 currentLabelPosition = new Vector2(0, 160); // Start more centered
    public GameObject[] shipLabels;


    public void SelectShip(int shipIndex)
    {
        // Used by PlayerControls to select a ship
        if (shipIndex < 0 || shipIndex >= shipCount)
        {
            Debug.LogError("Invalid ship index selected: " + shipIndex);
            return;
        }
        else
        {
            currentShipSelected = shipIndex;
            Debug.Log("Selected ship: " + currentShipSelected);
            // Clear previous selections
            for (int i = 0; i < shipLabels.Length; i++)
            {
                if (i != currentShipSelected)
                {
                    shipLabels[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white; // Reset other labels
                }
            }
            shipLabels[shipIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow; // Highlight selected ship label

        }
    }

    void Start()
    {
        // Harcoded the ship selection for now
        shipData = new ShipData[shipCount];
        shipData[0] = ScriptableObject.CreateInstance<ShipData>();
        shipData[0].Initialize("Battleship", "Arquitens");
        shipData[1] = ScriptableObject.CreateInstance<ShipData>();
        shipData[1].Initialize("Fighter", "Terminus 1");
        shipData[2] = ScriptableObject.CreateInstance<ShipData>();
        shipData[2].Initialize("Fighter", "Terminus 2");

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