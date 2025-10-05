using UnityEngine;

public class HideDoor : MonoBehaviour
{
    public GameObject[] DoorObject;
    void Start()
    {
        for(int i = 0; i < DoorObject.Length; i++)
        {
            DoorObject[i].SetActive(false);
        }
    }

}
