using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBackButton : MonoBehaviour
{
    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScreen");
    }

}
