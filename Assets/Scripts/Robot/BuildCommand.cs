using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    private NavMeshAgent _agent;
    private Builder _builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        _agent = agent;
        _builder = builder;
    }

    public override bool IsComplete => BuildComplete();

    public override void Execute()
    {
        _agent.SetDestination(_builder.transform.position);
    }

    private bool BuildComplete()
    {
        if (_agent.remainingDistance > 0.1f)
            return false;

        if (_builder != null)
            _builder.Build();

        return true;
    }
}
