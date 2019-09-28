using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyData
{
    public static Dictionary<string, List<PrimitiveSpec>> bodySpec = new Dictionary<string, List<PrimitiveSpec>>
    {
        {
            "Hips",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.3f, Quaternion.identity, Vector3.one * 0.7f)
            }
        },
        {
            "LeftUpLeg",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.3f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.78f, 0.4f))
            }
        },
        {
            "LeftLeg",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.73f, 0.4f))
            }
        },
        {
            "LeftFoot",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Cube, new Vector3(0, -0.1f, 0.26f), Quaternion.Euler(9f, 0, 0), new Vector3(0.3f, 0.2f, 0.6f))
            }
        },
        {
            "RightUpLeg",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.3f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.78f, 0.4f))
            }
        },
        {
            "RightLeg",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.up * -0.79f, Quaternion.identity, new Vector3(0.4f, 0.73f, 0.4f))
            }
        },
        {
            "RightFoot",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Cube, new Vector3(0, -0.1f, 0.26f), Quaternion.Euler(9f, 0, 0), new Vector3(0.3f, 0.2f, 0.6f))
            }
        },
        {
            "Spine",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.8f, Quaternion.identity, Vector3.one * 0.5f)
            }
        },
        {
            "Spine1",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * 0.33f, Quaternion.identity, Vector3.one * 0.8f)
            }
        },
        {
            "Neck",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f)
            }
        },
        {
            "Head",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.up * -0.1f, Quaternion.identity, Vector3.one * 0.8f)
            }
        },
        // left and right reversed
        {
            "LeftShoulder",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.33f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.33f, 0.3f))
            }
        },
        {
            "LeftArm",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.55f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.55f, 0.3f))
            }
        },
        {
            "LeftForeArm",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * 0.43f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.45f, 0.3f))
            }
        },
        {
            "LeftHand",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f)
            }
        },
        {
            "RightShoulder",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.33f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.33f, 0.3f))
            }
        },
        {
            "RightArm",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.55f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.55f, 0.3f))
            }
        },
        {
            "RightForeArm",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f),
                new PrimitiveSpec(PrimitiveType.Capsule, Vector3.right * -0.43f, Quaternion.Euler(0, 0, 90f), new Vector3(0.3f, 0.45f, 0.3f))
            }
        },
        {
            "RightHand",
            new List<PrimitiveSpec>
            {
                new PrimitiveSpec(PrimitiveType.Sphere, Vector3.zero, Quaternion.identity, Vector3.one * 0.2f)
            }
        }
    };
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
