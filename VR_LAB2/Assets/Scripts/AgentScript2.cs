using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class CubeAgentRays : Agent
{

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        // reset de positie en orientatie als de agent gevallen is
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
            this.transform.localRotation = Quaternion.identity;
        }

        // verplaats de target naar een nieuwe willekeurige locatie 
        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent posities
        // sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

    }


    public float speedMultiplier = 0.5f;
    public float rotationMultiplier = 5f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions (size = 2)
        Vector3 controlSignal = Vector3.zero;

        // Forward/backward movement
        controlSignal.z = actionBuffers.ContinuousActions[0];
        transform.Translate(controlSignal * speedMultiplier);

        // Rotation
        transform.Rotate(
            0.0f,
            rotationMultiplier * actionBuffers.ContinuousActions[1],
            0.0f
        );

        // Rewards
        float distanceToTarget = Vector3.Distance(
            this.transform.localPosition,
            Target.localPosition
        );

        // Target reached
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }


}