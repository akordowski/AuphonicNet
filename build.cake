#load nuget:https://www.myget.org/F/arkord/api/v2?package=Cake.Baker

var twitterMessage = new StringBuilder()
    .AppendLine("Version {0} of {1} has just been released, https://www.nuget.org/packages/{1}/.")
    .Append("#auphonic #nuget #csharp #dotnet")
    .ToString();

Build
    .SetParameters(
        "AuphonicNet",
        "akordowski",
        postTwitterMessage: twitterMessage)
    .Run();