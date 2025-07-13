using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerShipController : MonoBehaviour
{


    // Ship data
    public GameObject[] playerShips;
    public ShipData[] shipData;
    public int shipCount = 3;
    public int currentShipSelected = -1;
private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();



    // UI elements
    public GameObject gameplayUI;
    public GameObject shipLabelPrefab;
    public GameObject[] shipLabels;
    public Vector2 currentLabelPosition = new Vector2(0, 160);
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
            // Reset the materials of the previously selected ship
            if (currentShipSelected != -1)
            {
                Debug.Log("Resetting materials for previously selected ship.");
                Renderer[] previousRenderers = playerShips[currentShipSelected].GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in previousRenderers)
                {
                    if (originalMaterials.ContainsKey(rend))
                    {
                        rend.materials = originalMaterials[rend];
                    }
                }
            }

            // Highlight the selected ship label
            currentShipSelected = shipIndex;
            Debug.Log("Selected ship: " + currentShipSelected);
            Debug.Log(currentShipSelected);
            shipLabels[shipIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow; // Highlight selected ship label

            originalMaterials.Clear();
            // Highlight the selected ship in the game world
            Renderer[] renderers = playerShips[currentShipSelected].GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.LogWarning("No renderers found on selected ship.");
                return;
            }
            foreach (Renderer rend in renderers)
            {
                originalMaterials[rend] = rend.materials; // Save original materials
                Material[] highlightMats = new Material[rend.materials.Length];
                for (int i = 0; i < highlightMats.Length; i++)
                {
                    highlightMats[i] = highlightMaterial;
                }
                rend.materials = highlightMats;
            }
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