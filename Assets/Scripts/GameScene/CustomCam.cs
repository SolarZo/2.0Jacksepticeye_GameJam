using UnityEngine;

public class CustomCam : MonoBehaviour
{
    public Camera CinemaCam;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CinemaCam.depth = 2;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CinemaCam.depth = -1;
        }
    }

    public void deactivateCam()
    {
        CinemaCam.depth = -1;
    }
}
