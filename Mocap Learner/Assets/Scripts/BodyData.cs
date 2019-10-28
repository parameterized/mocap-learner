using System.Collections.Generic;
using UnityEngine;

public class BodyData
{
    public Dictionary<string, BodyPartInfo> bodySpec;

    public BodyData()
    {
        bodySpec = new Dictionary<string, BodyPartInfo>();
        string[] bodyPartNames = { "Hips", "LeftUpLeg", "LeftLeg", "LeftFoot", "RightUpLeg", "RightLeg", "RightFoot",
            "Spine", "Spine1", "Neck", "Head", "LeftShoulder", "LeftArm", "LeftForeArm", "LeftHand", "RightShoulder", "RightArm", "RightForeArm", "RightHand" };
        foreach (string name in bodyPartNames)
        {
            bodySpec[name] = new BodyPartInfo(new List<PrimitiveSpec>(), new JointParameters());
        }

        bodySpec["Hips"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.3f, Quaternion.identity, Vector3.one * 0.7f));
        bodySpec["LeftUpLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.3f));
        bodySpec["LeftUpLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.78f, 0.4f)));
        bodySpec["LeftLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["LeftLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.73f, 0.4f)));
        bodySpec["LeftFoot"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["LeftFoot"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Cube, new Vector3(0, -0.1f, 0.26f), Quaternion.Euler(9f, 0, 0), new Vector3(0.3f, 0.2f, 0.6f)));
        bodySpec["RightUpLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.3f));
        bodySpec["RightUpLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.78f, 0.4f)));
        bodySpec["RightLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["RightLeg"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.73f, 0.4f)));
        bodySpec["RightFoot"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["RightFoot"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Cube, new Vector3(0, -0.1f, 0.26f), Quaternion.Euler(9f, 0, 0), new Vector3(0.3f, 0.2f, 0.6f)));
        bodySpec["Spine"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.8f, Quaternion.identity, Vector3.one * 0.5f));
        bodySpec["Spine1"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.33f, Quaternion.identity, Vector3.one * 0.8f));
        bodySpec["Neck"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["Head"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * -0.1f, Quaternion.identity, Vector3.one * 0.8f));
        // left and right reversed
        bodySpec["LeftShoulder"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.33f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.33f, 0.3f)));
        bodySpec["LeftArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["LeftArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.55f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.55f, 0.3f)));
        bodySpec["LeftForeArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["LeftForeArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.43f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.45f, 0.3f)));
        bodySpec["LeftHand"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["RightShoulder"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.33f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.33f, 0.3f)));
        bodySpec["RightArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["RightArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.55f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.55f, 0.3f)));
        bodySpec["RightForeArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));
        bodySpec["RightForeArm"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.43f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.45f, 0.3f)));
        bodySpec["RightHand"].Shapes.Add(new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f));

        bodySpec["Spine"].JointParameters = new JointParameters(new Vector3(0, 0.6f, 0));
        bodySpec["Head"].JointParameters = new JointParameters(new Vector3(0, -0.52f, 0));
    }
}

public class PrimitiveSpec
{
    public PrimitiveType Type { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    public PrimitiveSpec(PrimitiveType type, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Type = type;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}

public class JointParameters
{
    public Vector3 Anchor { get; set; }

    public JointParameters()
    {
        Anchor = Vector3.zero;
    }

    public JointParameters(Vector3 anchor)
    {
        Anchor = anchor;
    }
}

public class BodyPartInfo
{
    public List<PrimitiveSpec> Shapes { get; set; }
    public JointParameters JointParameters { get; set; }

    public BodyPartInfo(List<PrimitiveSpec> shapes, JointParameters jointParameters)
    {
        Shapes = shapes;
        JointParameters = jointParameters;
    }
}
