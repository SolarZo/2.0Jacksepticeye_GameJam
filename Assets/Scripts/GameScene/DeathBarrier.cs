using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public GameObject RespawnAnchor;
    

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger entered by: " + col.gameObject.name);
        //GameManager.Instance.RespawnPlayer();

        
        if(col.gameObject.tag == "Player")
        {
            GameManager.Instance.RespawnPlayer();
        }
        Destroy(col.gameObject);


    }

}
