namespace FSharpScripting.Balloon

open Godot
open System
open FSharpScripting.Utilities

type Balloon() =
    inherit AnimatedSprite2D()

    let speed =
        (randRange 100.0 200.0) |> float32

    let mutable dead = false

    override this._Ready() =
        let animation =
            this.GetNode<AnimatedSprite2D>(new NodePath("AnimatedSprite"))

        animation.Animation <- "default"

    override this._Process(delta) =
        this.Position <- Vector2(this.Position.x, this.Position.y + speed * (delta |> float32))

    member this.Pop() =
        let popSound =
            this.GetNode<AudioStreamPlayer2D>(new NodePath("pop_sound"))

        let animation =
            this.GetNode<AnimatedSprite2D>(new NodePath("AnimatedSprite"))

        popSound.Play()
        animation.Animation <- "death"
        this.QueueFree()

    member private this._on_area_2d_input_event((viewport: obj), (event: InputEvent), (shape_idx: int)) =
        match event with
        | x when (x :? InputEventMouseButton) ->
            match x with
            | y when (y :?> InputEventMouseButton).ButtonIndex = MouseButton.Left ->
                match y with
                | z when z.IsPressed() ->
                    dead <- true
                    this.Pop()
                | _ -> ()
            | _ -> ()
        | _ -> ()
