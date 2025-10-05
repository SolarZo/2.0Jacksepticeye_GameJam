using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class Walker : MonoBehaviour
{

    public Transform leftFootTarget;
    public Transform rightFootTarget;
    public Transform leftArmTarget;
    public Transform rightArmTarget;
    public AnimationCurve horizontalCurve;
    public AnimationCurve throwCurve;
    public bool isThrowing = false;


    public float startTime = 0f;
    public float duration = .3f;

    private float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float curveTime = Mathf.Max(0, elapsedTime - startTime);
        //Debug.Log($"curve time {curveTime} > {duration}");
        if (curveTime > duration)
        {
            curveTime = duration;
            isThrowing = false;
        }
        float curveValue = throwCurve.Evaluate(curveTime);


        PlayerMechanics pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();

        if (pm.move.x != 0 || pm.move.z != 0)//Moving
        {
            leftFootTarget.transform.localPosition = new Vector3(1 * horizontalCurve.Evaluate(Time.time), -.891f, .229f);
            rightFootTarget.transform.localPosition = new Vector3(-1 * horizontalCurve.Evaluate(Time.time), -.891f, -.229f);
            if (!isThrowing)
            {
                leftArmTarget.transform.localPosition = new Vector3(-1 * horizontalCurve.Evaluate(Time.time), -.5f, .6f);
                rightArmTarget.transform.localPosition = new Vector3(1 * horizontalCurve.Evaluate(Time.time), -.5f, -.6f);
            }
            else//Throwing
            {
                if (pm.lastDirection.x <= 0)//last looking left
                {
                    leftArmTarget.transform.localPosition = new Vector3(curveValue, -.1f, .6f);
                    rightArmTarget.transform.localPosition = new Vector3(-curveValue, -.1f, -.6f);
                }
                else
                {
                    leftArmTarget.transform.localPosition = new Vector3(-curveValue, -.1f, .6f);
                    rightArmTarget.transform.localPosition = new Vector3(curveValue, -.1f, -.6f);
                }
            }
        }
        else//Idle
        {
            leftFootTarget.transform.localPosition = new Vector3(0, -.891f, .229f);
            rightFootTarget.transform.localPosition = new Vector3(0, -.891f, -.229f);
            if (!isThrowing)
            {
                leftArmTarget.transform.localPosition = new Vector3(0, -.5f, .6f);
                rightArmTarget.transform.localPosition = new Vector3(0, -.5f, -.6f);
            }
            else//Throwing
            {
                if (pm.lastDirection.x <= 0)//last looking left
                {
                    leftArmTarget.transform.localPosition = new Vector3(curveValue, -.1f, .6f);
                    rightArmTarget.transform.localPosition = new Vector3(-curveValue, -.1f, -.6f);
                }
                else
                {
                    leftArmTarget.transform.localPosition = new Vector3(-curveValue, -.1f, .6f);
                    rightArmTarget.transform.localPosition = new Vector3(curveValue, -.1f, -.6f);
                }
            }
        }



    }
    public void StartThrowAnimation()
    {
        elapsedTime = 0;
        isThrowing = true;
    }
}
