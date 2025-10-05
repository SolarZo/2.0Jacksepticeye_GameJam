using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowButton : MonoBehaviour
{
    int[] WindowModes = { 0, 1};
    int WindowSetting = 0;

    public TextMeshProUGUI buttonText;

    private void Start()
    {
        DisplayWindowMode();
    }


    public void CycleLabel()
    {   
        if(WindowSetting != 1)
        {
            WindowSetting += 1;
        }
        else
        {
            WindowSetting = 0;
        }
        Debug.Log("Cycle window mode");
        DisplayWindowMode();
    }
    public void DisplayWindowMode()
    {
        switch (WindowSetting)
        {
            case 0:
                buttonText.text = "Fullscreen";
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                buttonText.text = "Windowed";
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }




}
