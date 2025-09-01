using UnityEngine;

public class CameraPOV : MonoBehaviour
{

    public GameObject camera;
    public GameObject commandShip;
    public DeploymentController deploymentController;

    void Update() 
    {


        if(deploymentController.isDeploymentPhase) return;

        if (commandShip == null)
        {
            Debug.LogWarning("Command ship not assigned in Camera script.");

            commandShip = GameObject.Find("arquitens(Clone)");
            return;
        }

        // Follow the command ship's position
        camera.transform.position = new Vector3(commandShip.transform.position.x, commandShip.transform.position.y + 2f, commandShip.transform.position.z);
    }
}