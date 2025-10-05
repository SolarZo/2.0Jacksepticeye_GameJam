using UnityEngine;

public class Walker : MonoBehaviour
{

    public Transform leftFootTarget;
    public Transform rightFootTarget;
    public AnimationCurve horizontalCurve;

    // Update is called once per frame
    void Update()
    {
        PlayerMechanics pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        
        if (pm.move.x != 0 || pm.move.z !=0)//Moving
        {
            leftFootTarget.transform.localPosition = new Vector3(1 * horizontalCurve.Evaluate(Time.time), -.891f, .229f);
            rightFootTarget.transform.localPosition = new Vector3(-1 * horizontalCurve.Evaluate(Time.time), -.891f, -.229f);
        }
        else//Idle
        {
            leftFootTarget.transform.localPosition = new Vector3(0, -.891f, .229f);
            rightFootTarget.transform.localPosition = new Vector3(0, -.891f, -.229f);
        }
        
    }
}
