using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{


    public void StartGame()
    {
        //Production VVVVV
        GameManager.Instance.NextLevel();
        //Testing VVVVV
        //SceneManager.LoadScene("TestScene");
    }

}