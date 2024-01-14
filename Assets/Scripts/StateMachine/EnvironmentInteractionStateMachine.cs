using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Assertions;

public class EnvironmentInteractionStateMachine : StateManager<EnvironmentInteractionStateMachine.EEnvironmentInteractionState>
{
    public enum EEnvironmentInteractionState
    {
        Search,
        Approach,
        Rise,
        Touch,
        Reset,

    }

    [SerializeField] private TwoBoneIKConstraint _leftIkConstraint;
    [SerializeField] private TwoBoneIKConstraint _rightIkConstraint;
    [SerializeField] private MultiRotationConstraint _leftMultiRotationConstraint;
    [SerializeField] private MultiRotationConstraint _rightMultiRotationConstraint;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _rootCollider;

    void Awake()
    {
        ValidateConstraints();
    }

    private void ValidateConstraints()
    {
        Assert.IsNotNull(_leftIkConstraint, "Left IK constraints is not assigned.");
        Assert.IsNotNull(_rightIkConstraint, "Right IK constraints is not assigned.");
        Assert.IsNotNull(_leftMultiRotationConstraint, "left multi-rotation constraint is not assigned.");
        Assert.IsNotNull(_rightMultiRotationConstraint, "right multi-rotation constraint is not assigned.");
        Assert.IsNotNull(_rigidbody, "Rigidbody used to control character is not assigned.");
        Assert.IsNotNull(_rootCollider, "RootCollider attached to character is not assigned.");
    }
}
