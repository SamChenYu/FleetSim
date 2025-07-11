using UnityEngine;

public class PlayerShipController : MonoBehaviour
{


    public bool isDeploymentPhase = false;

    public GameObject[] playerShips;
    public ShipData[] shipData;
    public const int shipCount = 3;


    public int currentShipSelected = -1;


    public Transform[] deploymentPoints; // Points where ships are deployed

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

    }

    void Update()
    {

        if (isDeploymentPhase)
        {
            return;
        }

    }

}
