using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButtonController : MonoBehaviour
{


    public void OnStartClick()
    {   
        SceneManager.LoadScene("Game Scene");
    }

    public void OnShipSelectionClick() 
    {
        SceneManager.LoadScene("Ship Selection");
    }

}
