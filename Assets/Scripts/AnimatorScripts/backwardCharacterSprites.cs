using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backwardCharacterSprites : StateMachineBehaviour
{
    Transform rightArmTrans;
    Transform leftArmTrans;
    Transform rightSleeve;
    Transform leftSleeve;
    Transform eyes;
    Transform nose;
    Transform mouth;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        eyes = animator.transform.Find("EyesParent");
        nose = animator.transform.Find("NoseParent");
        mouth = animator.transform.Find("MouthParent");

        Transform headParent = animator.transform.Find("HeadParent");
        headParent.localPosition = new Vector3(0.0269f, 1.374301f, 0.2f);
        eyes.localPosition = new Vector3(0.00416f, 1.26767f, 0.1f);
        nose.localPosition = new Vector3(0.00644f, 1.04531f, 0.1f);
        mouth.localPosition = new Vector3(0.01778f, 0.75943f, 0.1f);

        Transform torsoParent = animator.transform.Find("TorsoParent");
        torsoParent.localPosition = new Vector3(-0.002665f, 0.22623f, 0.3f);
        Transform playerArms = animator.transform.Find("Arms");
        playerArms.localPosition = new Vector3(-2.472654f, 4.085711f, 0.5f);
        animator.transform.Find("LegsParent").localPosition = new Vector3(0.0198f, -0.5325f, 0.3f);

        headParent.transform.Find("HairFrontParent").localPosition = new Vector3(0f, 0f, 0.5f);
        headParent.transform.Find("EarsParent").localPosition = new Vector3(-0.0023f, -0.1701f, 0.5f);
        headParent.transform.Find("HairBackParent").localPosition = new Vector3(0.1336f, 0.13966f, -0.05f);

        torsoParent.transform.Find("TopParent").localPosition = new Vector3(0.00004f, -0.2087495f, -0.05f);

        Transform leftArmParent = playerArms.Find("LeftArmParent");
        Transform rightArmParent = playerArms.Find("RightArmParent");
        Transform leftSleeve = leftArmParent.Find("LeftSleeveParent");
        Transform rightSleeve = rightArmParent.Find("RightSleeveParent");

        leftArmParent.localPosition = new Vector3(10.3184f, -21.0155f, 0.1f);
        leftArmParent.localScale = new Vector3(-2.26892f, 2.26892f, 1);
        rightArmParent.localPosition = new Vector3(14.3796f, -20.97f, 0.1f);
        rightArmParent.localScale = new Vector3(-2.26892f, 2.26892f, 1);
        leftSleeve.localPosition = new Vector3(-0.21999f, 0.96499f, -0.05f);
        leftSleeve.localScale = new Vector3(-1, 1, 1);
        rightSleeve.localPosition = new Vector3(0.21995f, 0.96496f, -0.05f);
        rightSleeve.localScale = new Vector3(1, 1, 1);

        //Make face invisible
        eyes.Find("Eyes").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        nose.Find("Nose").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        mouth.Find("Mouth").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eyes.Find("Eyes").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        nose.Find("Nose").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        mouth.Find("Mouth").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

}
