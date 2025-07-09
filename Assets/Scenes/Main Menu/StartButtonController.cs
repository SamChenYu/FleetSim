using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButtonController : MonoBehaviour
{


    public void OnStartClick()
    {   
        SceneManager.LoadScene("Game Scene");
    }

}
