using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{


    public void StartGame()
    {
        //Production VVVVV
        //SceneManager.LoadScene("SampleScene");
        //Testing VVVVV
        SceneManager.LoadScene("TestScene");
    }

}
