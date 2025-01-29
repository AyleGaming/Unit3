using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    private Vector3 target;

    public override void Cancel()
    {

    }

    public override void Execute()
    {
        companionController.GetNavMeshAgent().SetDestination(target);
    }

    public override bool IsCommandComplete()
    {
//        float distance = Vector3.Distance(target, companionController.transform.position);

        return Vector3.Distance(target, companionController.transform.position) < 0.5f;
    }

    public MoveCommand(Vector3 position)
    {
        target = position;
    }

}
