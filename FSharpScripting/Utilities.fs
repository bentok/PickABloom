module FSharpScripting.Utilities

open System

let randRange min max =
    Random().NextDouble() * (max - min) + min