using UnityEngine;

public class LookatPlayer : MonoBehaviour
{
    private void Update()
    {
        this.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
}
