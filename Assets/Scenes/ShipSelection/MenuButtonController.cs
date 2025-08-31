using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtonController : MonoBehaviour
{


    public void GoToMenu()
    {   
        SceneManager.LoadScene("Main Menu");
    }


}
