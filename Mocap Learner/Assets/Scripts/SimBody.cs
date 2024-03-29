﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MocapLearner
{
    public class SimBody
    {
        public BVHData BVHData { get; set; }
        public BodyData BodyData { get; set; }
        public List<BodyPart> BodyParts { get; set; } = new List<BodyPart>();
        public Material Material { get; set; }

        private BVHLimits bvhLimits;

        public SimBody(BVHData bvhData, BodyData bodyData)
        {
            BVHData = bvhData;
            BodyData = bodyData;
            bvhLimits = new BVHLimits(bvhData);
            //bvhLimits.FitAnimation();
            bvhLimits.ExpandToAnimation(false);
        }

        public void Generate(Transform parent)
        {
            GenerateElements(parent, BVHData.Skeletons[0]);
            Unparent();
        }

        private void GenerateElements(Transform parent, BVHElement bvh)
        {
            GameObject bodyPartGO = new GameObject() { name = bvh.Name };
            bodyPartGO.transform.parent = parent;
            bodyPartGO.transform.localPosition = bvh.Offset / 10f;

            BVHElementLimit bvhLimit = bvhLimits.Limits[bvh.Name];
            float meanXLimit = (bvhLimit.AngularXLow + bvhLimit.AngularXHigh) / 2f;
            float meanYLimit = (bvhLimit.AngularYLow + bvhLimit.AngularYHigh) / 2f;
            float meanZLimit = (bvhLimit.AngularZLow + bvhLimit.AngularZHigh) / 2f;
            bodyPartGO.transform.localRotation = Quaternion.identity;
            bodyPartGO.transform.localRotation *= Quaternion.AngleAxis(meanXLimit, Vector3.right);
            bodyPartGO.transform.localRotation *= Quaternion.AngleAxis(meanYLimit, Vector3.up);
            bodyPartGO.transform.localRotation *= Quaternion.AngleAxis(meanZLimit, Vector3.forward);

            bodyPartGO.AddComponent<Rigidbody>();
            //bodyPartGO.GetComponent<Rigidbody>().useGravity = false;
            BodyPart bodyPart = new BodyPart(bodyPartGO, bvh);
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

                if (bvh.Name != "Hips")
                {
                    bodyPartGO.AddComponent<ConfigurableJoint>();
                    ConfigurableJoint cj = bodyPartGO.GetComponent<ConfigurableJoint>();
                    cj.connectedBody = parent.GetComponent<Rigidbody>();
                    cj.xMotion = ConfigurableJointMotion.Locked;
                    cj.yMotion = ConfigurableJointMotion.Locked;
                    cj.zMotion = ConfigurableJointMotion.Locked;
                    cj.anchor = BodyData.bodySpec[bvh.Name].JointParameters.Anchor;

                    cj.angularXMotion = ConfigurableJointMotion.Limited;
                    cj.angularYMotion = ConfigurableJointMotion.Limited;
                    cj.angularZMotion = ConfigurableJointMotion.Limited;
                    cj.lowAngularXLimit = new SoftJointLimit { limit = bvhLimit.AngularXLow - meanXLimit };
                    cj.highAngularXLimit = new SoftJointLimit { limit = bvhLimit.AngularXHigh - meanXLimit };
                    cj.angularYLimit = new SoftJointLimit { limit = bvhLimit.AngularYHigh - meanYLimit };
                    cj.angularZLimit = new SoftJointLimit { limit = bvhLimit.AngularZHigh - meanZLimit };
                }
            }

            foreach (BVHElement child in bvh.Children)
            {
                GenerateElements(bodyPart.GameObject.transform, child);
            }
        }

        private void Unparent()
        {
            foreach (BodyPart bodyPart in BodyParts.Skip(1))
            {
                bodyPart.GameObject.transform.parent = BodyParts[0].GameObject.transform.parent;
            }
        }

        public void SetBodyFromRef(RefBody refBody)
        {
            Debug.Assert(BodyParts.Count == refBody.BodyParts.Count);
            Transform refParent = refBody.BodyParts[0].GameObject.transform.parent;
            for (int i = 0; i < BodyParts.Count; i++)
            {
                Transform simBodyPartTransform = BodyParts[i].GameObject.transform;
                Transform refBodyPartTransform = refBody.BodyParts[i].GameObject.transform;
                Transform refBodyPartParent = refBodyPartTransform.parent;
                refBodyPartTransform.parent = refParent;
                simBodyPartTransform.localPosition = refBodyPartTransform.localPosition;
                simBodyPartTransform.localRotation = refBodyPartTransform.localRotation;
                refBodyPartTransform.parent = refBodyPartParent;

                Rigidbody rb = BodyParts[i].GameObject.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = (refBodyPartTransform.position - refBody.BodyParts[i].LastFramePosition) / Time.fixedDeltaTime;
                    
                    Quaternion deltaRotation = refBodyPartTransform.rotation * Quaternion.Inverse(refBody.BodyParts[i].LastFrameRotation);
                    deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                    angle *= Mathf.Deg2Rad;
                    Vector3 angularVelocity = axis * angle / Time.fixedDeltaTime;
                    if (float.IsInfinity(angularVelocity.x) || float.IsInfinity(angularVelocity.y) || float.IsInfinity(angularVelocity.z))
                    {
                        angularVelocity = Vector3.zero;
                    }
                    rb.angularVelocity = angularVelocity;
                }
            }
        }
    }
}
