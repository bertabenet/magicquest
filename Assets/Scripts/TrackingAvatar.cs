using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using extOSC;
using System.Linq;

public class TrackingAvatar : MonoBehaviour
{
    public GameObject nose;
    public GameObject wristR;
    public GameObject wristL;
    public GameObject ankleR;
    public GameObject ankleL;

    public int mean_size;

    //OSC Variables
    private OSCReceiver _receiver;
    private const string _oscAddress = "/pose/0";

    //Dictionary to store pose data
    public Dictionary<string, Vector3> pose = new Dictionary<string, Vector3>();

    private Dictionary<string, List<Vector3>> mean_pose = new Dictionary<string, List<Vector3>>();

    private List<string> bodyParts = new List<string>();
    private List<GameObject> bodyParts_object = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    { 

        bodyParts.Add("nose");
        bodyParts.Add("leftWrist");
        bodyParts.Add("rightWrist");
        bodyParts.Add("leftAnkle");
        bodyParts.Add("rightAnkle");

        bodyParts_object.Add(nose);
        bodyParts_object.Add(wristL);
        bodyParts_object.Add(wristR);
        bodyParts_object.Add(ankleL);
        bodyParts_object.Add(ankleR);


        //Set up OSC receiver
        StartOSCReceiver();

        //Initialize pose
        StartPose();
    }

    void StartOSCReceiver()
    {
        // Creating a receiver.
        _receiver = gameObject.AddComponent<OSCReceiver>();

        // Set local port.
        _receiver.LocalPort = 9876;

        // Bind "MessageReceived" method to special address.
        _receiver.Bind(_oscAddress, MessageReceived);
    }

    void StartPose()
    {
        int count = 0;
        foreach(string part in bodyParts)
        {
            pose.Add(part, bodyParts_object[count].transform.position);
            mean_pose.Add(part, new List<Vector3>());
            for(int i = 0; i < mean_size; i++)
            {
                mean_pose[part].Add(Vector3.zero);
            }
            count += 1;
        }

    }


    // Update is called once per frame
    void Update()
    {
        // add the new pose in this frame in the vector of means
        foreach(string part in bodyParts)
        {
            mean_pose[part][0] = pose[part];
        }

        // if the frame is not one of the first ones do the following:
        if (Time.frameCount > mean_size)
        {
            // counter to keep track of the body part object we are working on
            int count = 0;
            // do the same piece of code for all the body parts
            foreach (string part in bodyParts)
            {
                // compute the mean position with the current one and the ones before
                Vector3 total_pose = new Vector3();
                float ponderations = 0.0f;
                for (int i = 0; i < mean_size; i++)
                {
                    // compute ponderations
                    float pond = (1 / Mathf.Pow(2.0f, (float)(i + 1)));
                    if (i == mean_size - 1)
                    {
                        pond = 1 - ponderations;
                    }
                    ponderations += pond;
                    total_pose += mean_pose[part][i] * pond;
                }

                // transform the object's position according to the mean
                bodyParts_object[count].transform.position = total_pose;
                count += 1;
            }
        }
        else
        {
            int count = 0;
            foreach(string part in bodyParts)
            {
                bodyParts_object[count].transform.position = pose[part];
                count += 1;
            }
        }

        // update the mean vector
        for (int i = 1; i < mean_size; i++)
        {
            foreach(string part in bodyParts)
            {
                mean_pose[part][i] = mean_pose[part][i - 1];
            }
        }

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
}
