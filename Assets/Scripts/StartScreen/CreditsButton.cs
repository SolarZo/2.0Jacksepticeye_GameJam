using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour
{
    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }
}
