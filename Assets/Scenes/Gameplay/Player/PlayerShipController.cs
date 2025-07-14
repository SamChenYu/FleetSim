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
    public GameObject[] shipStatsLabels;
    private Vector2 currentLabelPosition = new Vector2(0, 160);
    public Material highlightMaterial;


    public GameObject[] AdjustPowerButtons;
    public GameObject AddPowerButton;
    public GameObject RemovePowerButton;


    void Awake()
    {
        // Harcoded the ship selection for now
        shipData = new ShipData[shipCount];
        shipData[0] = ScriptableObject.CreateInstance<ShipData>();
        shipData[0].Initialize("Fighter", "Terminus 1");
        shipData[1] = ScriptableObject.CreateInstance<ShipData>();
        shipData[1].Initialize("Fighter", "Terminus 2");
        shipData[2] = ScriptableObject.CreateInstance<ShipData>();
        shipData[2].Initialize("Battleship", "Arquitens");
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
                Renderer[] previousRenderers = playerShips[currentShipSelected].GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in previousRenderers)
                {
                    if (originalMaterials.ContainsKey(rend))
                    {
                        rend.materials = originalMaterials[rend];
                    }
                }
            }
            // Clear the highlighted materials dictionary
            originalMaterials.Clear();
            // Clear the ship stats labels
            for (int i = 0; i < shipStatsLabels.Length; i++)
            {
                if (shipStatsLabels[i] != null) Destroy(shipStatsLabels[i]);
            }

            if (currentShipSelected == shipIndex)
            {
                currentShipSelected = -1; // Deselect the ship if it is already selected
                return;
            }
            // Clear the previous buttons
            for (int i = 0; i < AdjustPowerButtons.Length; i++)
            {
                if (AdjustPowerButtons[i] != null) Destroy(AdjustPowerButtons[i]);
            }




            // Highlight the selected ship label
            currentShipSelected = shipIndex;
            shipLabels[shipIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow; // Highlight selected ship label

            // Highlight the selected ship in the game world
            Renderer[] renderers = playerShips[currentShipSelected].GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
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


        // Display the UI for the selected ship's stats

        // Health
        // Speed
        // Shield
        // Shield Regen Rate
        // Shield Regen Delay
        // Fire Rate
        // Range
        // Damage


        // Power
        // Allocated Engine Power
        // Allocated Shield Power
        // Allocated Weapon Power

        shipStatsLabels[0] = createLabel("Health: " + shipData[currentShipSelected].health.ToString("F1"), currentLabelPosition);
        shipStatsLabels[1] = createLabel("Speed: " + shipData[currentShipSelected].baseSpeed.ToString("F1"), currentLabelPosition);
        currentLabelPosition += new Vector2(0, -50); // Move label down for next stat
        shipStatsLabels[2] = createLabel("Shield: " + shipData[currentShipSelected].shield.ToString("F1"), currentLabelPosition);
        shipStatsLabels[3] = createLabel("Shield Regen Rate: " + shipData[currentShipSelected].shieldRegenRate.ToString("F1"), currentLabelPosition);
        shipStatsLabels[4] = createLabel("Shield Regen Delay: " + shipData[currentShipSelected].shieldRegenDelay.ToString("F1"), currentLabelPosition);
        currentLabelPosition += new Vector2(0, -50); // Move label down for next stat
        shipStatsLabels[5] = createLabel("Fire Rate: " + shipData[currentShipSelected].fireRate.ToString("F1"), currentLabelPosition);
        shipStatsLabels[6] = createLabel("Range: " + shipData[currentShipSelected].range.ToString("F1"), currentLabelPosition);
        shipStatsLabels[7] = createLabel("Damage: " + shipData[currentShipSelected].damage.ToString("F1"), currentLabelPosition);
        currentLabelPosition += new Vector2(0, -50); // Move label down for next stat
        shipStatsLabels[8] = createLabel("Power: " + shipData[currentShipSelected].power, currentLabelPosition);


        // Add engine buttons
        GameObject[] temp = createAddPowerButton(currentLabelPosition);
        AdjustPowerButtons[0] = temp[0]; // Add Engine Power Button
        AdjustPowerButtons[1] = temp[1]; // Remove Engine Power Button
        shipStatsLabels[9] = createLabel("Allocated Engine Power: " + shipData[currentShipSelected].allocatedEnginePower, currentLabelPosition);

        temp = createAddPowerButton(currentLabelPosition);
        AdjustPowerButtons[2] = temp[0]; // Add Shield Power Button
        AdjustPowerButtons[3] = temp[1]; // Remove Shield Power Button
        shipStatsLabels[10] = createLabel("Allocated Shield Power: " + shipData[currentShipSelected].allocatedShieldPower, currentLabelPosition);

        temp = createAddPowerButton(currentLabelPosition);
        AdjustPowerButtons[4] = temp[0]; // Add Weapon Power Button
        AdjustPowerButtons[5] = temp[1]; // Remove Weapon Power Button
        shipStatsLabels[11] = createLabel("Allocated Weapon Power: " + shipData[currentShipSelected].allocatedWeaponPower, currentLabelPosition);
        currentLabelPosition += new Vector2(0, 50 * 15); // Move label up to original position for next loop
    }

    void Start()
    {
        shipLabels = new GameObject[shipCount];
        gameplayUI.SetActive(false);

        shipStatsLabels = new GameObject[12];
        AdjustPowerButtons = new GameObject[6];
    }

    void Update()
    {
    }

    public void ReceiveShipsFromDeployment(GameObject[] deploymentShips)
    {
        playerShips = deploymentShips;


        // Initialize ship labels in the UI
        gameplayUI.SetActive(true);

        for (int i = 0; i < shipData.Length; i++)
        {
            shipLabels[i] = createLabel(shipData[i].name + " (" + shipData[i].type + ")", currentLabelPosition);
        }
        currentLabelPosition += new Vector2(0, -100); // Adjust for the individual ship stats display

    }

    private GameObject createLabel(string labelText, Vector2 position)
    {
        GameObject label = Instantiate(shipLabelPrefab);
        label.transform.SetParent(gameplayUI.transform, false);
        RectTransform rect = label.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        TextMeshProUGUI text = label.GetComponentInChildren<TextMeshProUGUI>();
        text.text = labelText;
        currentLabelPosition += new Vector2(0, -50); // Move label down
        return label;
    }

    private GameObject[] createAddPowerButton(Vector2 position)
    {
        GameObject[] buttons = new GameObject[2];

        GameObject addPowerButton = Instantiate(AddPowerButton);
        addPowerButton.transform.SetParent(gameplayUI.transform, false);
        RectTransform rect = addPowerButton.GetComponent<RectTransform>();
        rect.anchoredPosition = position + new Vector2(300, 0); // Position below the ship stats

        buttons[0] = addPowerButton;

        GameObject removePowerButton = Instantiate(RemovePowerButton);
        removePowerButton.transform.SetParent(gameplayUI.transform, false);
        rect = removePowerButton.GetComponent<RectTransform>();
        rect.anchoredPosition = position + new Vector2(350, 0); // Position below the add button
        buttons[1] = removePowerButton;

        return buttons;
    }

    
}