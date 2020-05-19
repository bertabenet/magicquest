using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using System.Linq;
using System.Diagnostics;

public class TrackingReceiver : MonoBehaviour
{
    public Animator animator;
    public List<Transform> BoneList = new List<Transform>();
    Quaternion[] DefaultBoneRot = new Quaternion[17];
    Quaternion[] DefaultBoneLocalRot = new Quaternion[17];
    Vector3[] DefaultXAxis = new Vector3[17];
    Vector3[] DefaultYAxis = new Vector3[17];
    Vector3[] DefaultZAxis = new Vector3[17];
    Vector3[] DefaultNormalizeBone = new Vector3[12];
    int[,] BoneJoint = new int[,]
    { { 0, 2 }, { 2, 3 }, { 0, 5 }, { 5, 6 }, { 0, 7 }, { 7, 8 }, { 8, 9 }, { 9, 10 }, { 9, 12 }, { 12, 13 }, { 9, 15 }, { 15, 16 }
    };

    //GameObjects to be controlled with Posenet
    //public GameObject nose;
    //public GameObject wristR;

    //OSC Variables
    private OSCReceiver _receiver;
    private const string _oscAddress = "/pose/0";

    //Dictionary to store pose data
    public Dictionary<string, Vector3> pose = new Dictionary<string, Vector3>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Set up OSC receiver
        StartOSCReceiver();

        GetBones();

        //Initialize pose
        StartPose();
    }

    void StartOSCReceiver() {
        // Creating a receiver.
        _receiver = gameObject.AddComponent<OSCReceiver>();

        // Set local port.
        _receiver.LocalPort = 9876;

        // Bind "MessageReceived" method to special address.
        _receiver.Bind(_oscAddress, MessageReceived);
    }

    void StartPose() {
        pose.Add("nose", Vector3.zero);
        /*
        pose.Add("leftEye", Vector3.zero);
        pose.Add("rightEye", Vector3.zero);
        pose.Add("leftEar", Vector3.zero);
        pose.Add("rightEar", Vector3.zero);
        */
        pose.Add("leftShoulder", Vector3.zero);
        pose.Add("rightShoulder", Vector3.zero);
        pose.Add("leftElbow", Vector3.zero);
        pose.Add("rightElbow", Vector3.zero);
        pose.Add("leftWrist", Vector3.zero);
        pose.Add("rightWrist", Vector3.zero);
        pose.Add("leftHip", Vector3.zero);
        pose.Add("rightHip", Vector3.zero);
        pose.Add("leftKnee", Vector3.zero);
        pose.Add("rightKnee", Vector3.zero);
        pose.Add("leftAnkle", Vector3.zero);
        pose.Add("rightAnkle", Vector3.zero);
    }
    

    // Update is called once per frame
    void Update()
    {
        //nose.transform.position = pose["nose"];
        //wristR.transform.position = pose["rightWrist"];
        //BoneList[16].transform.position = pose["rightWrist"];

        //UnityEngine.Debug.Log(BoneList[16].ToString());


        BoneList[0].transform.position = pose["nose"];
        //BoneList[1].transform.position = pose["leftEye"];
        //BoneList[2].transform.position = pose["rightEye"];
        //BoneList[3].transform.position = pose["leftEar"];
        //BoneList[4].transform.position = pose["rightEar"];
        
        BoneList[5].transform.position = pose["leftShoulder"];
        BoneList[6].transform.position = pose["rightShoulder"];
        BoneList[7].transform.position = pose["leftElbow"];
        BoneList[8].transform.position = pose["rightElbow"];
        BoneList[9].transform.position = pose["leftWrist"];
        BoneList[10].transform.position = pose["rightWrist"];
        BoneList[11].transform.position = pose["leftHip"];
        BoneList[12].transform.position = pose["rightHip"];
        BoneList[13].transform.position = pose["leftKnee"];
        BoneList[14].transform.position = pose["rightKnee"];
        BoneList[15].transform.position = pose["leftAnkle"];
        BoneList[16].transform.position = pose["rightAnkle"];
        
    }

    protected void MessageReceived(OSCMessage message)
    {
        List<OSCValue> list = message.Values;
        UnityEngine.Debug.Log(list.Count);

        for (int i = 0; i < list.Count; i += 3)
        {
            string key = "";
            Vector2 position = Vector3.zero;

            OSCValue val0 = list.ElementAt(i);
            if (val0.Type == OSCValueType.String) key = val0.StringValue;
            OSCValue val1 = list.ElementAt(i + 1);
            if (val1.Type == OSCValueType.Float) position.x = val1.FloatValue - 250;
            OSCValue val2 = list.ElementAt(i + 2);
            if (val2.Type == OSCValueType.Float) position.y = -(val2.FloatValue - 250);


            if (pose.ContainsKey(key))
            {
                pose[key] = position;
            }
        }

    }

    void GetBones()
    {
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.Head));

        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.Head));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.Head));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.Head));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.Head));
        
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftShoulder));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightShoulder));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightLowerArm));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftHand));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightHand));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.LeftFoot));
        BoneList.Add(animator.GetBoneTransform(HumanBodyBones.RightFoot));

        /*
        for (int i = 0; i < BoneList.Count; i++)
        {
            var rootT = animator.GetBoneTransform(HumanBodyBones.Hips).root;
            DefaultBoneRot[i] = BoneList[i].rotation;
            DefaultBoneLocalRot[i] = BoneList[i].localRotation;
            DefaultXAxis[i] = new Vector3(
                Vector3.Dot(BoneList[i].right, rootT.right),
                Vector3.Dot(BoneList[i].up, rootT.right),
                Vector3.Dot(BoneList[i].forward, rootT.right)
            );
            DefaultYAxis[i] = new Vector3(
                Vector3.Dot(BoneList[i].right, rootT.up),
                Vector3.Dot(BoneList[i].up, rootT.up),
                Vector3.Dot(BoneList[i].forward, rootT.up)
            );
            DefaultZAxis[i] = new Vector3(
                Vector3.Dot(BoneList[i].right, rootT.forward),
                Vector3.Dot(BoneList[i].up, rootT.forward),
                Vector3.Dot(BoneList[i].forward, rootT.forward)
            );
        }
        for (int i = 0; i < 12; i++)
        {
            DefaultNormalizeBone[i] = (BoneList[BoneJoint[i, 1]].position - BoneList[BoneJoint[i, 0]].position).normalized;
        }
        */

    }
    
}