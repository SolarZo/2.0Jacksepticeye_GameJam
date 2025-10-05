using UnityEngine;

public class opentrapdoor : MonoBehaviour
{
    float buttonBottom = 1.6f;
    bool pressed = false;
    float buttonSpeed = .02f;
    GameObject trapDoor;
    float trapdoorbottom = -170f;
    private void Awake()
    {
        trapDoor = GameObject.Find("TrapDoor");
        Debug.Log("Trapdoor name: "+trapDoor.name);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Button turned on by player");
            pressed = true;

            //make it move
            trapDoor.transform.rotation = Quaternion.Euler(trapdoorbottom, 270, 0);
        }
    }
    private void Update()
    {
        if (pressed && this.transform.localPosition.y > buttonBottom)
        {
            this.transform.localPosition -= new Vector3(0,buttonSpeed);
        }
        
    }
}
