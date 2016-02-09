#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.MSpecHelper

let buildMode = "Release"

let setParams defaults =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", buildMode
                ]

         }

Target "Clean" (fun _ -> 
    DeleteDir "DynamicConfiguration/bin/debug"
    DeleteDir "DynamicConfiguration/bin/release"
    DeleteDir "DynamicConfiguration.Tests/bin/debug"
    DeleteDir "DynamicConfiguration.Tests/bin/release"
)

Target "Build" (fun _ ->
    build setParams "DynamicConfiguration.sln"
)

let testDir = "./DynamicConfiguration.Tests/bin/release"
let testOutput = "./testResults"

Target "RunTests" (fun _ -> 
    !! (testDir @@ "DynamicConfiguration.Tests.dll")
    |> MSpec (fun parameters -> {parameters with HtmlOutputDir = testOutput} )
)

"Clean" 
    ==> "Build"

"Build"
    ==> "RunTests"
    
RunTargetOrDefault "Build"