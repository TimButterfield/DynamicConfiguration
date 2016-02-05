#r "../packages/FAKE/tools/FakeLib.dll"

open Fake

Target "Build" (fun _ -> 
 trace "Foo"
)

RunTargetOrDefault "Build"