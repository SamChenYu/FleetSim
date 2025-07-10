using UnityEngine;

public class GameController : MonoBehaviour
{

    private DeploymentController deploymentController;
    private DeploymentUI deploymentUI;

    private PlayerShipController playerShipController;

    void Start()
    {
        deploymentController = Object.FindFirstObjectByType<DeploymentController>();
        if (deploymentController == null)
        {
            Debug.LogError("DeploymentController component not found.");
        }
        deploymentUI = Object.FindFirstObjectByType<DeploymentUI>();
        if (deploymentUI == null)
        {
            Debug.LogError("DeploymentUI component not found.");
        }
        playerShipController = Object.FindFirstObjectByType<PlayerShipController>();
        if (playerShipController == null)
        {
            Debug.LogError("PlayerShipController component not found.");
        }
    }

    void Update()
    {

    }


    public void EndDeploymentPhase(Transform[] points)
    {
        playerShipController.SetDeployment(points);
    }
}
