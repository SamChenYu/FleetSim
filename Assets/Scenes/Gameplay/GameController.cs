using UnityEngine;


// Has dependencies on DeploymentControlller and PlayerShipController

public class GameController : MonoBehaviour
{

    public DeploymentController deploymentController;
    public DeploymentUI deploymentUI;

    public PlayerShipController playerShipController;

    void Awake()
    {
        playerShipController = FindFirstObjectByType<PlayerShipController>();
        if (playerShipController == null) Debug.LogError("PlayerShipController component not found.");
        deploymentController = FindFirstObjectByType<DeploymentController>();
        if (deploymentController == null) Debug.LogError("DeploymentController component not found.");
    }

    void Start()
    {
        
    }

    void Update()
    {

    }



}
