using System.Collections.Generic;
using UnityEngine;

namespace MocapLearner
{
    public class RefBody
    {
        public BVHData BVHData { get; set; }
        public BodyData BodyData { get; set; }

        public Material Material { get; set; }

        public List<BodyPart> BodyParts { get; set; } = new List<BodyPart>();

        public RefBody(BVHData bvhData, BodyData bodyData)
        {
            BVHData = bvhData;
            BodyData = bodyData;
        }

        public void Generate(Transform parent)
        {
            GenerateElement(parent, BVHData.Skeletons[0]);
        }

        private void GenerateElement(Transform parent, BVHElement bvh)
        {
            GameObject bodyPartGO = new GameObject() { name = bvh.Name };
            bodyPartGO.transform.parent = parent;
            bodyPartGO.transform.localPosition = bvh.Offset / 10f;
            BodyPart bodyPart = new BodyPart(bodyPartGO, bvh);
            bodyPart.SetLastFrame();
            BodyParts.Add(bodyPart);

            if (BodyData.bodySpec.ContainsKey(bvh.Name))
            {
                foreach (PrimitiveSpec ps in BodyData.bodySpec[bvh.Name].Shapes)
                {
                    GameObject go = GameObject.CreatePrimitive(ps.Type);
                    go.transform.parent = bodyPart.GameObject.transform;
                    go.transform.localPosition = ps.Position;
                    go.transform.localRotation = ps.Rotation;
                    go.transform.localScale = ps.Scale;
                    go.layer = 8; // Body
                    if (Material)
                    {
                        go.GetComponent<Renderer>().material = Material;
                    }
                }
            }

            foreach (var child in bvh.Children)
            {
                GenerateElement(bodyPart.GameObject.transform, child);
            }
        }

        public void SetBodyFromAnimation(float time)
        {
            SetLastFrame();
            int frameA = Mathf.FloorToInt(Mathf.Clamp(time / BVHData.FrameTime, 0, BVHData.Frames.Count - 1));
            int frameB = Mathf.CeilToInt(Mathf.Clamp(time / BVHData.FrameTime, 0, BVHData.Frames.Count - 1));
            float frameT = (time / BVHData.FrameTime) % 1;
            int channelIdx = 0;
            foreach (BodyPart bodyPart in BodyParts)
            {
                Vector3 pA = Vector3.zero;
                Vector3 pB = Vector3.zero;
                bool setPosition = false;
                Quaternion qA = Quaternion.identity;
                Quaternion qB = Quaternion.identity;
                bool setRotation = false;
                foreach (string channel in bodyPart.BVHElement.Channels)
                {
                    float channelValA = BVHData.Frames[frameA][channelIdx];
                    float channelValB = BVHData.Frames[frameB][channelIdx];
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
                    bodyPart.GameObject.transform.localPosition = Vector3.Lerp(pA, pB, frameT) / 10f;
                else
                    bodyPart.GameObject.transform.localPosition = bodyPart.BVHElement.Offset / 10f;

                if (setRotation)
                {
                    bodyPart.GameObject.transform.localRotation = Quaternion.Lerp(qA, qB, frameT);
                }
            }
        }

        public void SetLastFrame()
        {
            foreach (BodyPart bodyPart in BodyParts)
            {
                bodyPart.SetLastFrame();
            }
        }
    }
}
