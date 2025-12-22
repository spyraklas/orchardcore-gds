using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "DfE SignIn Components",
    Author = "Spyros Ioannidis",
    Website = "https://orchardcore.net",
    Version = "1.0.0",
    Description = "Provides widgets and activities to implement DfE SignIn components.",
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCore.DSI",
    Name = "DfE SignIn Components",
    Description = "Provides widgets and activities to implement DfE SignIn components.",
    Dependencies = new[] { "GovUK.DSI" },
    Category = "Content Management"
)]
