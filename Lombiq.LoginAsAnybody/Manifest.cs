﻿using OrchardCore.Modules.Manifest;
using static Lombiq.LoginAsAnybody.FeatureIds;

[assembly: Module(
    Name = "Lombiq Login as Anybody",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Description = "Module that allows administrators to log in as any user.",
    Website = "https://github.com/Lombiq/Orchard-Login-as-Anybody"
)]

[assembly: Feature(
    Id = Default,
    Name = "Lombiq Login as Anybody",
    Category = "Security",
    Description = "Module that allows administrators to log in as any user.",
    Dependencies = new[]
    {
        "OrchardCore.Users",
    }
)]