using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBody : MonoBehaviour
{
    private List<BodyJoint> joints = new List<BodyJoint>();

    private void Start()
    {
        Generate(transform, BodyData.jointSpec);

        GameObject reference = new GameObject();
        reference.name = "BodyRef";
        reference.transform.localPosition = Vector3.zero;
        Generate(reference.transform, BodyData.jointSpec, false);
    }

    private void Update()
    {
        if (Time.time % 2 < 1)
        {
            int frame = (int)(Time.time / WalkData.frameTime) % WalkData.frames;
            for (int i = 0; i < joints.Count; i++)
            {
                if (i == 0)
                {
                    Vector3 p = new Vector3((float)WalkData.data[frame, 0], (float)WalkData.data[frame, 1], (float)WalkData.data[frame, 2]);
                    joints[i].GameObject.transform.localPosition = p / 10f;
                } else
                {
                    joints[i].GameObject.transform.localPosition = joints[i].Offset / 10f;
                }
                Vector3 r = new Vector3((float)WalkData.data[frame, i * 3 + 3], (float)WalkData.data[frame, i * 3 + 4], (float)WalkData.data[frame, i * 3 + 5]);
                // euler -> quaternion in xyz order
                Quaternion q = Quaternion.AngleAxis(r.x, Vector3.right) * Quaternion.AngleAxis(r.y, Vector3.up) * Quaternion.AngleAxis(r.z, Vector3.forward);
                joints[i].GameObject.transform.localRotation = q;

                Rigidbody rb = joints[i].GameObject.GetComponent<Rigidbody>();
                if (frame == 0)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                } else
                {
                    // inaccurate (framerate & animation not synced) - interpolate animation and divide by unity frame time?
                    rb.velocity = (joints[i].GameObject.transform.position - joints[i].LastFramePosition) / WalkData.frameTime;
                    // relative rotation
                    Quaternion vq = Quaternion.Inverse(joints[i].GameObject.transform.rotation) * joints[i].LastFrameRotation;
                    rb.angularVelocity = vq.eulerAngles / WalkData.frameTime;
                }
                if (joints[i].LastFrame != frame)
                {
                    joints[i].LastFrame = frame;
                    joints[i].LastFramePosition = joints[i].GameObject.transform.position;
                    joints[i].LastFrameRotation = joints[i].GameObject.transform.rotation;
                }
            }
        }
    }

    private void Generate(Transform parent, BVHElem bvh, bool addToJoints = true)
    {
        GameObject joint = new GameObject();
        joint.name = bvh.Name;
        joint.transform.parent = parent;
        joint.transform.localPosition = bvh.Offset / 10f;
        if (addToJoints && bvh.IsJoint) { joints.Add(new BodyJoint(joint, bvh.Offset)); }
        joint.AddComponent<Rigidbody>();
        joint.GetComponent<Rigidbody>().useGravity = false;

        /*
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.name = "JointMarker";
        marker.transform.parent = joint.transform;
        marker.transform.localPosition = Vector3.zero;
        marker.transform.localScale = Vector3.one * 0.2f;
        */

        if (BodyData.bodySpec.ContainsKey(bvh.Name))
        {
            foreach (PrimitiveSpec ps in BodyData.bodySpec[bvh.Name])
            {
                GameObject go = GameObject.CreatePrimitive(ps.Type);
                go.transform.parent = joint.transform;
                go.transform.localPosition = ps.Position;
                go.transform.localRotation = ps.Rotation;
                go.transform.localScale = ps.Scale;
                go.layer = 8; // Body
            }
        }

        foreach (var child in bvh.Children)
        {
            Generate(joint.transform, child, addToJoints);
        }
    }
}
