namespace Player

open Godot

type StaticBody2d () =
    inherit StaticBody2D()
    
    override this._Ready() =
        ()
        
    override this._Process(delta) =
        printfn $"%A{delta}"
        ()