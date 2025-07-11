using UnityEngine;


// Has dependencies on DeploymentControlller and PlayerShipController

public class GameController : MonoBehaviour
{

    private DeploymentController deploymentController;
    private DeploymentUI deploymentUI;

    private PlayerShipController playerShipController;



    // Player Ship Data
    public const int shipCount = 3;
    
    public static readonly string[][] playerShips = new string[shipCount][]
        {
        new string[] { "Battleship", "Arquitens" },
        new string[] { "Fighter", "Terminus 1" },
        new string[] { "Fighter", "Terminus 2" }
    };







    void Start()
    {
        deploymentController = Object.FindFirstObjectByType<DeploymentController>();
        if (deploymentController == null) Debug.LogError("DeploymentController component not found.");
        playerShipController = Object.FindFirstObjectByType<PlayerShipController>();
        if (playerShipController == null) Debug.LogError("PlayerShipController component not found.");
    }

    void Update()
    {

    }



}
