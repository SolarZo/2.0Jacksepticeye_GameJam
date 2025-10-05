using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject StartMenu;
    public void OpenOptions()
    {
        Debug.Log("Open Options");
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
        StartMenu.SetActive(!StartMenu.activeSelf);
    }
}
