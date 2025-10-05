using UnityEngine;

public class nextLevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.NextLevel();
        }
    }
}
