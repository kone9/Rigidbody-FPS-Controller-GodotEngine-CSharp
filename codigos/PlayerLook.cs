using Godot;
using System;

public class PlayerLook : Node
{
    [Export]
    float senX;
    
    [Export]
    float senY;
    
    [Export]
    float multiplier = 0.01f;

    [Export]
    bool puedoRotar = true;

    [Export]
    bool capturarMouse = true;

    float mouseX;
    float mouseY;


    float xRotation;
    float yRotation;



    RigidBody personaje;
    Camera camara;

    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        personaje = GetParent() as RigidBody;
        camara = GetTree().GetNodesInGroup("CameraPersonaje")[0] as Camera;
        if(capturarMouse)
        {
            Input.SetMouseMode( Input.MouseMode.Captured );
        }
        else
        {
            Input.SetMouseMode( Input.MouseMode.Hidden );
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        yRotation -= mouseX - senX - multiplier * delta;
        xRotation -= mouseY - senY - multiplier * delta;
        xRotation = Mathf.Clamp(xRotation,-90,90);//clampea la variable entre esos dos valores para que la camara no rota más de 90 grados
        
        if(puedoRotar)
        {
            rotarCamara();
            rotarPersonaje();
        }
    }

    private void rotarCamara()
    {
        camara.RotationDegrees = new Vector3(
            xRotation,
            0,
            0
        );
    }

    private void rotarPersonaje()
    {
        personaje.RotationDegrees = new Vector3(
            0,
            yRotation,
            0
        );
    }




    public override void _Input(InputEvent @event)
    {
       if(@event is InputEventMouseMotion)//si el evento es mover el mouse
       {
           mouseX = (@event as InputEventMouseMotion).Relative.x;
           mouseY = (@event as InputEventMouseMotion).Relative.y;
           GD.Print("posicion mouse X", mouseX);
           GD.Print("posicion mouse Y", mouseY);
       }

    }


}
