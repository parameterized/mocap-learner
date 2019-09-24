using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyData : MonoBehaviour
{
    // hips moved up
    public static BVHElem jointSpec = new BVHElem("Hips", new Vector3(0, 35f, 0), new List<BVHElem>
    {
        new BVHElem("LeftUpLeg", new Vector3(3.64953f, 0, 0), new List<BVHElem>
        {
            new BVHElem("LeftLeg", new Vector3(0, -15.70580f, 0), new List<BVHElem>
            {
                new BVHElem("LeftFoot", new Vector3(0, -15.41867f, 0), new List<BVHElem>
                {
                    new BVHElem("LeftToeBase", new Vector3(0, -1.53543f, 5.73033f), new List<BVHElem>
                    {
                        new BVHElem("EndSite", new Vector3(0, 0, 2.95275f), new List<BVHElem>(), false)
                    })
                })
            })
        }),
        new BVHElem("RightUpLeg", new Vector3(-3.64953f, 0, 0), new List<BVHElem>
        {
            new BVHElem("RightLeg", new Vector3(0, -15.70580f, 0), new List<BVHElem>
            {
                new BVHElem("RightFoot", new Vector3(0, -15.41867f, 0), new List<BVHElem>
                {
                    new BVHElem("RightToeBase", new Vector3(0, -1.53543f, 5.73033f), new List<BVHElem>
                    {
                        new BVHElem("EndSite", new Vector3(0, 0, 2.95275f), new List<BVHElem>(), false)
                    })
                })
            })
        }),
        new BVHElem("Spine", new Vector3(0, 0.03937f, 0), new List<BVHElem>
        {
            new BVHElem("Spine1", new Vector3(0, 10.24829f, 0), new List<BVHElem>
            {
                new BVHElem("Neck", new Vector3(0, 7.82687f, 0), new List<BVHElem>
                {
                    new BVHElem("Head", new Vector3(0, 6.90715f, 0), new List<BVHElem>
                    {
                        new BVHElem("EndSite", new Vector3(0, 4.52755f, 0), new List<BVHElem>(), false)
                    })
                }),
                new BVHElem("LeftShoulder", new Vector3(0, 7.82687f, 0), new List<BVHElem>
                {
                    new BVHElem("LeftArm", new Vector3(6.71018f, -0.00002f, 0), new List<BVHElem>
                    {
                        new BVHElem("LeftForeArm", new Vector3(10.94419f, -0.00004f, 0), new List<BVHElem>
                        {
                            new BVHElem("LeftHand", new Vector3(8.52010f, -0.00003f, 0), new List<BVHElem>
                            {
                                new BVHElem("LeftHandThumb", new Vector3(0, 0, 0), new List<BVHElem>
                                {
                                    new BVHElem("EndSite", new Vector3(0, 0, 3.93700f), new List<BVHElem>(), false)
                                }),
                                new BVHElem("L_Wrist_End", new Vector3(3.93700f, -0.00001f, 0), new List<BVHElem>())
                            })
                        })
                    })
                }),
                new BVHElem("RightShoulder", new Vector3(0, 7.82687f, 0), new List<BVHElem>
                {
                    new BVHElem("RightArm", new Vector3(-6.71018f, -0.00002f, 0), new List<BVHElem>
                    {
                        new BVHElem("RightForeArm", new Vector3(-10.94419f, -0.00004f, 0), new List<BVHElem>
                        {
                            new BVHElem("RightHand", new Vector3(-8.52010f, -0.00003f, 0), new List<BVHElem>
                            {
                                new BVHElem("RightHandThumb", new Vector3(0, 0, 0), new List<BVHElem>
                                {
                                    new BVHElem("EndSite", new Vector3(0, 0, 3.93700f), new List<BVHElem>(), false)
                                }),
                                new BVHElem("R_Wrist_End", new Vector3(-5.39369f, -0.00002f, 0), new List<BVHElem>())
                            })
                        })
                    })
                })
            })
        })
    });

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

public class BVHElem
{
    public string Name { get; set; }
    public Vector3 Offset { get; set; }
    public List<BVHElem> Children { get; set; }
    public bool IsJoint { get; set; }

    public BVHElem(string name, Vector3 offset, List<BVHElem> children, bool isJoint = true)
    {
        Name = name;
        Offset = offset;
        Children = children;
        IsJoint = isJoint;
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

public class BodyJoint
{
    public GameObject GameObject { get; set; }
    public Vector3 Offset { get; set; }

    public int LastFrame { get; set; }
    public Vector3 LastFramePosition { get; set; }
    public Quaternion LastFrameRotation { get; set; }

    public BodyJoint(GameObject gameObject, Vector3 offset)
    {
        GameObject = gameObject;
        Offset = offset;
    }
}
