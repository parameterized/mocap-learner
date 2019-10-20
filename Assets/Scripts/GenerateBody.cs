using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBody : MonoBehaviour
{
    public string BVHFilePath = "Assets/Mocap/0005_Walking001.bvh";

    private BVHData bvhData;
    private BodyData bodyData;
    private List<BodyJoint> joints = new List<BodyJoint>();

    private void Start()
    {
        LoadBody();
        GenerateElement(transform, bvhData.Skeletons[0]);
    }

    private void LoadBody()
    {
        bvhData = new BVHParser().Parse(BVHFilePath);
        // move hips up
        bvhData.Skeletons[0].Offset = new Vector3(0, 35f, 0);
        bodyData = new BodyData();
    }

    private void Update()
    {
        if (Time.time % 2 < 1)
        {
            int frameA = Mathf.FloorToInt((Time.time / bvhData.FrameTime) % (bvhData.Frames.Count - 1));
            int frameB = Mathf.CeilToInt((Time.time / bvhData.FrameTime) % (bvhData.Frames.Count - 1));
            float frameT = (Time.time / bvhData.FrameTime) % 1;
            int channelIdx = 0;
            foreach (BodyJoint joint in joints)
            {
                Vector3 pA = Vector3.zero;
                Vector3 pB = Vector3.zero;
                bool setPosition = false;
                Quaternion qA = Quaternion.identity;
                Quaternion qB = Quaternion.identity;
                bool setRotation = false;
                foreach (string channel in joint.BVHElement.Channels)
                {
                    float channelValA = bvhData.Frames[frameA][channelIdx];
                    float channelValB = bvhData.Frames[frameB][channelIdx];
                    switch (channel)
                    {
                        case "Xposition":
                            pA.x = channelValA;
                            pB.x = channelValB;
                            setPosition = true;
                            break;
                        case "Yposition":
                            pA.y = channelValA;
                            pB.y = channelValB;
                            setPosition = true;
                            break;
                        case "Zposition":
                            pA.z = channelValA;
                            pB.z = channelValB;
                            setPosition = true;
                            break;
                        case "Xrotation":
                            qA *= Quaternion.AngleAxis(channelValA, Vector3.right);
                            qB *= Quaternion.AngleAxis(channelValB, Vector3.right);
                            setRotation = true;
                            break;
                        case "Yrotation":
                            qA *= Quaternion.AngleAxis(channelValA, Vector3.up);
                            qB *= Quaternion.AngleAxis(channelValB, Vector3.up);
                            setRotation = true;
                            break;
                        case "Zrotation":
                            qA *= Quaternion.AngleAxis(channelValA, Vector3.forward);
                            qB *= Quaternion.AngleAxis(channelValB, Vector3.forward);
                            setRotation = true;
                            break;
                        default:
                            Debug.LogWarning("Channel \"" + channel + "\" not recognized");
                            break;
                    }
                    channelIdx += 1;
                }
                if (setPosition)
                {
                    joint.GameObject.transform.localPosition = Vector3.Lerp(pA, pB, frameT) / 10f;
                }
                else
                {
                    joint.GameObject.transform.localPosition = joint.BVHElement.Offset / 10f;
                }
                if (setRotation)
                {
                    joint.GameObject.transform.localRotation = Quaternion.Lerp(qA, qB, frameT);
                }


                Rigidbody rb = joint.GameObject.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = (joint.GameObject.transform.position - joint.LastFramePosition) / Time.deltaTime;

                    Quaternion deltaRotation = joint.GameObject.transform.rotation * Quaternion.Inverse(joint.LastFrameRotation);
                    deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                    angle *= Mathf.Deg2Rad;
                    Vector3 angularVelocity = axis * angle / Time.deltaTime;
                    if (float.IsInfinity(angularVelocity.x) || float.IsInfinity(angularVelocity.y) || float.IsInfinity(angularVelocity.z))
                    {
                        angularVelocity = Vector3.zero;
                    }
                    rb.angularVelocity = angularVelocity;
                }

                joint.LastFramePosition = joint.GameObject.transform.position;
                joint.LastFrameRotation = joint.GameObject.transform.rotation;
            }
        }
    }

    public void GenerateTestBody()
    {
        LoadBody();
        GameObject testBody = new GameObject();
        testBody.name = "Test Body";
        GenerateElement(testBody.transform, bvhData.Skeletons[0]);
    }

    private void GenerateElement(Transform parent, BVHElement bvh, bool addToJoints = true)
    {
        GameObject joint = new GameObject();
        joint.name = bvh.Name;
        joint.transform.parent = parent;
        joint.transform.localPosition = bvh.Offset / 10f;
        if (addToJoints && bvh.Type != "End Site")
        {
            BodyJoint bodyJoint = new BodyJoint(joint, bvh);
            bodyJoint.LastFramePosition = joint.transform.position;
            bodyJoint.LastFrameRotation = joint.transform.rotation;
            joints.Add(bodyJoint);
        }

        if (bodyData.bodySpec.ContainsKey(bvh.Name))
        {
            joint.AddComponent<Rigidbody>();
            //joint.GetComponent<Rigidbody>().useGravity = false;

            foreach (PrimitiveSpec ps in bodyData.bodySpec[bvh.Name].Shapes)
            {
                GameObject go = GameObject.CreatePrimitive(ps.Type);
                go.transform.parent = joint.transform;
                go.transform.localPosition = ps.Position;
                go.transform.localRotation = ps.Rotation;
                go.transform.localScale = ps.Scale;
                go.layer = 8; // Body
            }

            if (bvh.Name != "Hips")
            {
                joint.AddComponent<ConfigurableJoint>();
                ConfigurableJoint cj = joint.GetComponent<ConfigurableJoint>();
                cj.connectedBody = parent.GetComponent<Rigidbody>();
                cj.xMotion = ConfigurableJointMotion.Locked;
                cj.yMotion = ConfigurableJointMotion.Locked;
                cj.zMotion = ConfigurableJointMotion.Locked;
                cj.anchor = bodyData.bodySpec[bvh.Name].JointParameters.Anchor;

                /*
                cj.angularXMotion = ConfigurableJointMotion.Limited;
                cj.angularYMotion = ConfigurableJointMotion.Limited;
                cj.angularZMotion = ConfigurableJointMotion.Limited;
                cj.lowAngularXLimit = new SoftJointLimit { limit = -20f };
                cj.highAngularXLimit = new SoftJointLimit { limit = 20f };
                cj.angularYLimit = new SoftJointLimit { limit = 20f };
                cj.angularZLimit = new SoftJointLimit { limit = 20f };
                */
            }
        }

        foreach (var child in bvh.Children)
        {
            GenerateElement(joint.transform, child, addToJoints);
        }
    }
}

public class BodyJoint
{
    public GameObject GameObject { get; set; }
    public BVHElement BVHElement { get; set; }

    public Vector3 LastFramePosition { get; set; }
    public Quaternion LastFrameRotation { get; set; }

    public BodyJoint(GameObject gameObject, BVHElement bvhElement)
    {
        GameObject = gameObject;
        BVHElement = bvhElement;
    }
}
