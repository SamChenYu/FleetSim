using UnityEngine;

public class GameController : MonoBehaviour
{

    private DeploymentController deploymentController;
    private PlayerShipController playerShipController;

    private bool isDeploymentPhase;

    void Start()
    {
        isDeploymentPhase = true; // Initialize deployment phase
        deploymentController = Object.FindFirstObjectByType<DeploymentController>();
        playerShipController = Object.FindFirstObjectByType<PlayerShipController>();
    }

    void Update()
    {
        
    }
}
