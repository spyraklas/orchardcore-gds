using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "GDS Components",
    Author = "Spyros Ioannidis",
    Website = "",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = "OrchardCore.GDS.Components",
    Name = "GDS Components",
    Description = "Provides widgets and activities to implement GDS components.",
    Dependencies = new[] { "OrchardCore.Forms" },
    Category = "Content"
)]
