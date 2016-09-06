#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=OctopusTools"

var target = Argument("target", "Build");

Task("Default")
  .IsDependentOn("xUnit")
  .IsDependentOn("Pack")
  .IsDependentOn("OctoPush")
  .IsDependentOn("OctoRelease");

Task("Build")
  .Does(() =>
{
  MSBuild("./src/CakeDemo.sln");
});

Task("xUnit")
  .IsDependentOn("Build")
    .Does(() =>
{
  XUnit2("./src/CakeDemo.Tests/bin/Debug/CakeDemo.Tests.dll");
});

Task("Pack")
  .IsDependentOn("Build")
  .Does(() => {
    var nuGetPackSettings   = new NuGetPackSettings {
                                    Id                      = "CakeDemo",
                                    Version                 = "0.0.0.1",
                                    Title                   = "Cake Demo",
                                    Authors                 = new[] {"Derek Comartin"},
                                    Description             = "Demo of creating cake.build scripts.",
                                    Summary                 = "Excellent summary of what the Cake (C# Make) build tool does.",
                                    ProjectUrl              = new Uri("https://github.com/dcomartin/Cake.Demo"),
                                    Files                   = new [] {
                                                                        new NuSpecContent {Source = "CakeDemo.exe", Target = "bin"},
                                                                      },
                                    BasePath                = "./src/CakeDemo/bin/Debug",
                                    OutputDirectory         = "./nuget"
                                };

    NuGetPack(nuGetPackSettings);
  });

Task("OctoPush")
  .IsDependentOn("Pack")
  .Does(() => {
    OctoPush("http://your.octopusdeploy.server/", "YOUR_API_KEY", new FilePath("./nuget/CakeDemo.0.0.0.1.nupkg"),
      new OctopusPushSettings {
        ReplaceExisting = true
      });
  });

Task("OctoRelease")
  .IsDependentOn("OctoPush")
  .Does(() => {
    OctoCreateRelease("CakeDemo", new CreateReleaseSettings {
        Server = "http://your.octopusdeploy.server/",
        ApiKey = "YOUR_API_KEY",
        ReleaseNumber = "0.0.0.1"
      });
  });

RunTarget(target);