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
    DeleteDir "src/DynamicConfiguration/bin/debug"
    DeleteDir "src/DynamicConfiguration/bin/release"
    DeleteDir "src/DynamicConfiguration.Tests/bin/debug"
    DeleteDir "src/DynamicConfiguration.Tests/bin/release"
)

Target "Build" (fun _ ->
    build setParams "src/DynamicConfiguration.sln"
)

let testDir = ".src/DynamicConfiguration.Tests/bin/release"
let testOutput = "./src/testResults"

Target "RunTests" (fun _ -> 
    !! (testDir @@ "src/DynamicConfiguration.Tests.dll")
    |> MSpec (fun parameters -> {parameters with HtmlOutputDir = testOutput} )
)

"Clean" 
    ==> "Build"

"Build"
    ==> "RunTests"
    
RunTargetOrDefault "Build"