#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Build");

Task("Default")
  .IsDependentOn("xUnit");

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

RunTarget(target);