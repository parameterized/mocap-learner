using System.Collections.Generic;
using UnityEngine;

namespace MocapLearner
{
    public class BodyPart
    {
        public GameObject GameObject { get; set; }
        public BVHElement BVHElement { get; set; }
        public Vector3 LastFramePosition { get; set; }
        public Quaternion LastFrameRotation { get; set; }

        public BodyPart(GameObject gameObject, BVHElement bvhElement)
        {
            GameObject = gameObject;
            BVHElement = bvhElement;
        }

        public void SetLastFrame()
        {
            LastFramePosition = GameObject.transform.position;
            LastFrameRotation = GameObject.transform.rotation;
        }
    }
}
