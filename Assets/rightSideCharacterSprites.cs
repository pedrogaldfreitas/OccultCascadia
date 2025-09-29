using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightSideCharacterSprites : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Transform headParent = animator.transform.Find("HeadParent");
        headParent.localPosition = new Vector3(-0.0892f, 1.3675f, 0.2f);
        animator.transform.Find("EyesParent").localPosition = new Vector3(0.45754f, 1.2654f, 0.1f);
        animator.transform.Find("NoseParent").localPosition = new Vector3(0.67763f, 0.99766f, 0.1f);
        animator.transform.Find("MouthParent").localPosition = new Vector3(0.53015f, 0.8502f, 0.1f);

        Transform torsoParent = animator.transform.Find("TorsoParent");
        torsoParent.localPosition = new Vector3(-0.00751f, 0.2285f, 0.3f);
        Transform playerArms = animator.transform.Find("Arms");
        playerArms.localPosition = new Vector3(-2.472654f, 4.085711f, 0.3f);
        animator.transform.Find("LegsParent").localPosition = new Vector3(-0.02524f, -0.5232f, 0.4f);

        headParent.transform.Find("HairFrontParent").localPosition = new Vector3(0.279f, 0.41975f, -1.5f);
        headParent.transform.Find("EarsParent").localPosition = new Vector3(-0.35629f, -0.14069f, -1.0f);
        headParent.transform.Find("HairBackParent").localPosition = new Vector3(-0.60135f, 0.16335f, -0.5f);

        torsoParent.transform.Find("TopParent").localPosition = new Vector3(0.00004f, -0.20875f, -0.05f);

        Transform leftArmParent = playerArms.Find("LeftArmParent");
        Transform rightArmParent = playerArms.Find("RightArmParent");
        Transform leftSleeve = leftArmParent.Find("LeftSleeveParent");
        Transform rightSleeve = rightArmParent.Find("RightSleeveParent");

        leftArmParent.localPosition = new Vector3(11.6903f, -21.464f, 0.1f);
        leftArmParent.localScale = new Vector3(2.26892f, 2.26892f, 1);
        rightArmParent.localPosition = new Vector3(11.6903f, -21.464f, -0.1f);
        rightArmParent.localScale = new Vector3(2.26892f, 2.26892f, 1);
        leftSleeve.localPosition = new Vector3(0.04985f, 1.0299f, 0.05f);
        leftSleeve.localScale = new Vector3(1, 1, 1);
        rightSleeve.localPosition = new Vector3(0.04985f, 1.0299f, -0.05f);
        rightSleeve.localScale = new Vector3(1, 1, 1);


    }
}
