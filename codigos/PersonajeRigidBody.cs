using Godot;
using System;

public class PersonajeRigidBody : RigidBody
{
    [Export]
    public float MoveSpeed = 6f; //velocidad a la que se mueve
    [Export]
    public float MovementMultiplier = 10;

    [Export]
    bool puedoMover = true;

    private float horizontalMovement;
    private float VerticalMovement;

    Vector3 MoveDirection;

    public override void _Input(InputEvent @event)
    {
        horizontalMovement =  Input.GetActionStrength("d") - Input.GetActionStrength("a");
        VerticalMovement = Input.GetActionStrength("s") - Input.GetActionStrength("w");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

     // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //mueve hacia el vector forward
        MoveDirection = GlobalTransform.basis.z * VerticalMovement + GlobalTransform.basis.x * horizontalMovement;
    }

    // public override void _IntegrateForces(PhysicsDirectBodyState state)
    // {
    //     // AddForce( MoveDirection.Normalized , Vector3.Up);
    //     ApplyImpulse( Vector3(0,0,0) ,MoveDirection);
    // }

    public override void _PhysicsProcess(float delta)
    {
        if(puedoMover)
        {
            AddForce( MoveDirection * MoveSpeed * MovementMultiplier * delta, Vector3.Forward);// agrego la fuerza de movimiento
        }
    }

}
