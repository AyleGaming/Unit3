using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    Command currentCommand;
    [SerializeField] private Queue<Command> commandQueue = new Queue<Command>();

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Debug.Log("Commands in Q: " + commandQueue.Count);

        if (currentCommand != null && !currentCommand.IsCommandComplete()) return;
        if (commandQueue.Count == 0) return;

        currentCommand = commandQueue.Dequeue();
        currentCommand.Execute();

    }

    public void FinishCommand()
    {
        commandQueue.Dequeue();
    }

    public void GiveCommand(Command newCommand)
    {
        newCommand.SetCompanionController(this);
        commandQueue.Enqueue(newCommand);
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return agent;
    }

}
