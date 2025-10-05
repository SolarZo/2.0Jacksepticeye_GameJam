using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}
