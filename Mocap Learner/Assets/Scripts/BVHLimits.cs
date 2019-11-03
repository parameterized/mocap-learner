using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BVHLimits
{
    public Dictionary<string, BVHElementLimit> Limits { get; set; }

    private BVHData bvhData;
    private List<BVHElement> bvhElements;

    public BVHLimits(BVHData bvhData)
    {
        this.bvhData = bvhData;
        Limits = new Dictionary<string, BVHElementLimit>();

        // default limits
        // (left and right reversed)
        Limits["Hips"] = new BVHElementLimit
        {
            AngularXLow = -180f,
            AngularXHigh = 180f,
            AngularYLow = -180f,
            AngularYHigh = 180f,
            AngularZLow = -180f,
            AngularZHigh = 180f
        };
        Limits["LeftUpLeg"] = new BVHElementLimit
        {
            AngularXLow = -65f,
            AngularXHigh = 30f,
            AngularYLow = -45f,
            AngularYHigh = 48f,
            AngularZLow = -15f,
            AngularZHigh = 45f
        };
        Limits["LeftLeg"] = new BVHElementLimit
        {
            AngularXLow = -3f,
            AngularXHigh = 150f,
            AngularYLow = -15f,
            AngularYHigh = 15f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["LeftFoot"] = new BVHElementLimit
        {
            AngularXLow = -25f,
            AngularXHigh = 35f,
            AngularYLow = -19f,
            AngularYHigh = 15f,
            AngularZLow = -22f,
            AngularZHigh = 16f
        };
        Limits["RightUpLeg"] = new BVHElementLimit
        {
            AngularXLow = -65f,
            AngularXHigh = 30f,
            AngularYLow = -48f,
            AngularYHigh = 45f,
            AngularZLow = -45f,
            AngularZHigh = 15f
        };
        Limits["RightLeg"] = new BVHElementLimit
        {
            AngularXLow = -3f,
            AngularXHigh = 150f,
            AngularYLow = -15f,
            AngularYHigh = 15f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["RightFoot"] = new BVHElementLimit
        {
            AngularXLow = -25f,
            AngularXHigh = 35f,
            AngularYLow = -15f,
            AngularYHigh = 19f,
            AngularZLow = -16f,
            AngularZHigh = 22f
        };
        Limits["Spine"] = new BVHElementLimit
        {
            AngularXLow = -25f,
            AngularXHigh = 45f,
            AngularYLow = -25f,
            AngularYHigh = 25f,
            AngularZLow = -20f,
            AngularZHigh = 20f
        };
        Limits["Spine1"] = new BVHElementLimit
        {
            AngularXLow = -27f,
            AngularXHigh = 40f,
            AngularYLow = -20f,
            AngularYHigh = 20f,
            AngularZLow = -20f,
            AngularZHigh = 20f
        };
        Limits["Neck"] = new BVHElementLimit
        {
            AngularXLow = -20f,
            AngularXHigh = 20f,
            AngularYLow = -25f,
            AngularYHigh = 25f,
            AngularZLow = -10f,
            AngularZHigh = 10f
        };
        Limits["Head"] = new BVHElementLimit
        {
            AngularXLow = -35f,
            AngularXHigh = 17f,
            AngularYLow = -12f,
            AngularYHigh = 12f,
            AngularZLow = -15f,
            AngularZHigh = 15f
        };
        Limits["LeftShoulder"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = -30f,
            AngularYHigh = 30f,
            AngularZLow = -12f,
            AngularZHigh = 31f
        };
        Limits["LeftArm"] = new BVHElementLimit
        {
            AngularXLow = -30f,
            AngularXHigh = 35f,
            AngularYLow = -45f,
            AngularYHigh = 35f,
            AngularZLow = -85f,
            AngularZHigh = 45f
        };
        // angularY 15 -> 3
        Limits["LeftForeArm"] = new BVHElementLimit
        {
            AngularXLow = -85f,
            AngularXHigh = 45f,
            AngularYLow = -130f,
            AngularYHigh = 3f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["LeftHand"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = -20f,
            AngularYHigh = 20f,
            AngularZLow = -80f,
            AngularZHigh = 70f
        };
        Limits["RightShoulder"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = -30f,
            AngularYHigh = 30f,
            AngularZLow = -31f,
            AngularZHigh = 12f
        };
        Limits["RightArm"] = new BVHElementLimit
        {
            AngularXLow = -30f,
            AngularXHigh = 35f,
            AngularYLow = -35f,
            AngularYHigh = 45f,
            AngularZLow = -45f,
            AngularZHigh = 85f
        };
        // angularY 15 -> 3
        Limits["RightForeArm"] = new BVHElementLimit
        {
            AngularXLow = -85f,
            AngularXHigh = 45f,
            AngularYLow = -3f,
            AngularYHigh = 130f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["RightHand"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = -20f,
            AngularYHigh = 20f,
            AngularZLow = -70f,
            AngularZHigh = 80f
        };

        // from data
        Limits["LeftToeBase"] = new BVHElementLimit
        {
            AngularXLow = -28f,
            AngularXHigh = 32f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["RightToeBase"] = new BVHElementLimit
        {
            AngularXLow = -28f,
            AngularXHigh = 32f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["LeftHandThumb"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["L_Wrist_End"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["RightHandThumb"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["R_Wrist_End"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
        Limits["End Site"] = new BVHElementLimit
        {
            AngularXLow = 0f,
            AngularXHigh = 0f,
            AngularYLow = 0f,
            AngularYHigh = 0f,
            AngularZLow = 0f,
            AngularZHigh = 0f
        };
    }

    public void FitAnimation()
    {
        bvhElements = new List<BVHElement>();
        AddElements(bvhData.Skeletons[0]);
        Limits = new Dictionary<string, BVHElementLimit>();
        for (int frameIdx = 0; frameIdx < bvhData.Frames.Count; frameIdx++)
        {
            int channelIdx = 0;
            foreach (BVHElement bvh in bvhElements)
            {
                if (frameIdx == 0)
                {
                    Limits[bvh.Name] = new BVHElementLimit();
                }
                foreach (string channel in bvh.Channels)
                {
                    float channelVal = bvhData.Frames[frameIdx][channelIdx];
                    switch (channel)
                    {
                        case "Xrotation":
                            if (frameIdx == 0)
                            {
                                Limits[bvh.Name].AngularXLow = channelVal;
                                Limits[bvh.Name].AngularXHigh = channelVal;
                            }
                            else
                            {
                                Limits[bvh.Name].AngularXLow = Mathf.Min(Limits[bvh.Name].AngularXLow, channelVal);
                                Limits[bvh.Name].AngularXHigh = Mathf.Max(Limits[bvh.Name].AngularXHigh, channelVal);
                            }
                            break;
                        case "Yrotation":
                            if (frameIdx == 0)
                            {
                                Limits[bvh.Name].AngularYLow = channelVal;
                                Limits[bvh.Name].AngularYHigh = channelVal;
                            }
                            else
                            {
                                Limits[bvh.Name].AngularYLow = Mathf.Min(Limits[bvh.Name].AngularYLow, channelVal);
                                Limits[bvh.Name].AngularYHigh = Mathf.Max(Limits[bvh.Name].AngularYHigh, channelVal);
                            }
                            break;
                        case "Zrotation":
                            if (frameIdx == 0)
                            {
                                Limits[bvh.Name].AngularZLow = channelVal;
                                Limits[bvh.Name].AngularZHigh = channelVal;
                            }
                            else
                            {
                                Limits[bvh.Name].AngularZLow = Mathf.Min(Limits[bvh.Name].AngularZLow, channelVal);
                                Limits[bvh.Name].AngularZHigh = Mathf.Max(Limits[bvh.Name].AngularZHigh, channelVal);
                            }
                            break;
                    }
                    channelIdx += 1;
                }
            }
        }
    }

    public void ExpandToAnimation(bool logExpansion = true)
    {
        var oldLimits = Limits;
        var newLimits = new Dictionary<string, BVHElementLimit>();
        FitAnimation();
        foreach (string name in Limits.Keys)
        {
            if (oldLimits.ContainsKey(name))
            {
                newLimits[name] = new BVHElementLimit();

                if (Limits[name].AngularXLow < oldLimits[name].AngularXLow)
                {
                    newLimits[name].AngularXLow = Limits[name].AngularXLow;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularXLow limit from {oldLimits[name].AngularXLow} to {newLimits[name].AngularXLow}");
                }
                else
                {
                    newLimits[name].AngularXLow = oldLimits[name].AngularXLow;
                }
                if (Limits[name].AngularXHigh > oldLimits[name].AngularXHigh)
                {
                    newLimits[name].AngularXHigh = Limits[name].AngularXHigh;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularXHigh limit from {oldLimits[name].AngularXHigh} to {newLimits[name].AngularXHigh}");
                }
                else
                {
                    newLimits[name].AngularXHigh = oldLimits[name].AngularXHigh;
                }


                if (Limits[name].AngularYLow < oldLimits[name].AngularYLow)
                {
                    newLimits[name].AngularYLow = Limits[name].AngularYLow;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularYLow limit from {oldLimits[name].AngularYLow} to {newLimits[name].AngularYLow}");
                }
                else
                {
                    newLimits[name].AngularYLow = oldLimits[name].AngularYLow;
                }
                if (Limits[name].AngularYHigh > oldLimits[name].AngularYHigh)
                {
                    newLimits[name].AngularYHigh = Limits[name].AngularYHigh;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularYHigh limit from {oldLimits[name].AngularYHigh} to {newLimits[name].AngularYHigh}");
                }
                else
                {
                    newLimits[name].AngularYHigh = oldLimits[name].AngularYHigh;
                }


                if (Limits[name].AngularZLow < oldLimits[name].AngularZLow)
                {
                    newLimits[name].AngularZLow = Limits[name].AngularZLow;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularZLow limit from {oldLimits[name].AngularZLow} to {newLimits[name].AngularZLow}");
                }
                else
                {
                    newLimits[name].AngularZLow = oldLimits[name].AngularZLow;
                }
                if (Limits[name].AngularZHigh > oldLimits[name].AngularZHigh)
                {
                    newLimits[name].AngularZHigh = Limits[name].AngularZHigh;
                    if (logExpansion)
                        Debug.Log($"Expanded {name} AngularZHigh limit from {oldLimits[name].AngularZHigh} to {newLimits[name].AngularZHigh}");
                }
                else
                {
                    newLimits[name].AngularZHigh = oldLimits[name].AngularZHigh;
                }
            }
            else
            {
                Debug.LogWarning($"\"{name}\" not in original limits");
                newLimits[name] = Limits[name];
            }
        }
        Limits = newLimits;
    }

    private void AddElements(BVHElement bvhElement)
    {
        bvhElements.Add(bvhElement);
        foreach (BVHElement child in bvhElement.Children)
        {
            AddElements(child);
        }
    }
}

public class BVHElementLimit
{
    public float AngularXLow { get; set; }
    public float AngularXHigh { get; set; }
    public float AngularYLow { get; set; }
    public float AngularYHigh { get; set; }
    public float AngularZLow { get; set; }
    public float AngularZHigh { get; set; }
}
