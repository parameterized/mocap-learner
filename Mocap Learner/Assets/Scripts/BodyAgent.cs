using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MocapLearner;

public class BodyAgent : Agent
{
    public string bvhFilePath = "Assets/Mocap/0005_Walking001.bvh";
    public Material simMaterial;
    public Material refMaterial;
    public bool randomStateInitialization = true;
    public bool earlyStopping = true;

    private BVHData bvhData;
    private SimBody simBody;
    private RefBody refBody;
    private float timer;
    private Vector3 velocity = Vector3.zero;

    private bool isNewDecisionStep;
    private int currentDecisionStep;

    void Start()
    {
        bvhData = new BVHParser().Parse(bvhFilePath);
        // move hips up
        bvhData.Skeletons[0].Offset = new Vector3(0, 35f, 0);
        BodyData bodyData = new BodyData();


        simBody = new SimBody(bvhData, bodyData);

        GameObject simBodyGO = new GameObject { name = "Sim Body" };
        simBodyGO.transform.parent = transform;
        simBodyGO.transform.localPosition = Vector3.right * 5f;

        GameObject simOriginSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        simOriginSphere.transform.parent = simBodyGO.transform;
        simOriginSphere.transform.localPosition = Vector3.zero;
        simOriginSphere.transform.localScale = Vector3.one * 0.5f;
        if (simMaterial) {
            simOriginSphere.GetComponent<Renderer>().material = simMaterial;
        }
        simOriginSphere.GetComponent<Collider>().enabled = false;

        simBody.Material = simMaterial;
        simBody.Generate(simBodyGO.transform);


        refBody = new RefBody(bvhData, bodyData);

        GameObject refBodyGO = new GameObject { name = "Ref Body" };
        refBodyGO.transform.parent = transform;
        refBodyGO.transform.localPosition = Vector3.left * 5f;

        GameObject refOriginSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        refOriginSphere.transform.parent = refBodyGO.transform;
        refOriginSphere.transform.localPosition = Vector3.zero;
        refOriginSphere.transform.localScale = Vector3.one * 0.5f;
        if (refMaterial) {
            refOriginSphere.GetComponent<Renderer>().material = refMaterial;
        }
        refOriginSphere.GetComponent<Collider>().enabled = false;

        refBody.Material = refMaterial;
        refBody.Generate(refBodyGO.transform);
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > (bvhData.Frames.Count - 1) * bvhData.FrameTime)
        {
            Done();
        }

        refBody.SetBodyFromAnimation(timer);
        //simBody.BodyParts[0].GameObject.GetComponent<Rigidbody>().velocity = velocity;

        Vector3 p = simBody.BodyParts[0].GameObject.transform.localPosition;
        //simBody.BodyParts[0].GameObject.transform.localPosition = new Vector3(p.x, refBody.BodyParts[0].GameObject.transform.localPosition.y, p.z);
    }

    public override void AgentReset()
    {
        // random state initialization
        if (randomStateInitialization)
            timer = Random.Range(Time.fixedDeltaTime, bvhData.Frames.Count * bvhData.FrameTime);
        else
            timer = Time.fixedDeltaTime;

        // correct velocity
        refBody.SetBodyFromAnimation(timer - Time.fixedDeltaTime);
        refBody.SetBodyFromAnimation(timer);
        simBody.SetBodyFromRef(refBody);

        isNewDecisionStep = true;
        currentDecisionStep = 1;
    }

    public override void CollectObservations()
    {
        Vector3 refPos = refBody.BodyParts[0].GameObject.transform.localPosition;
        Vector3 simPos = simBody.BodyParts[0].GameObject.transform.localPosition;
        AddVectorObs(refPos - simPos);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (isNewDecisionStep)
        {
            velocity = new Vector3(vectorAction[0], vectorAction[1], vectorAction[2]) * 10f;
        }

        IncrementDecisionTimer();

        float dist = Vector3.Distance(
            simBody.BodyParts[0].GameObject.transform.localPosition,
            refBody.BodyParts[0].GameObject.transform.localPosition
        );
        AddReward(1 - dist / 2f);
        if (earlyStopping && dist > 2f)
        {
            Done();
        }
    }

    private void IncrementDecisionTimer()
    {
        if (currentDecisionStep == agentParameters.numberOfActionsBetweenDecisions ||
            agentParameters.numberOfActionsBetweenDecisions == 1)
        {
            currentDecisionStep = 1;
            isNewDecisionStep = true;
        }
        else
        {
            currentDecisionStep++;
            isNewDecisionStep = false;
        }
    }
}
