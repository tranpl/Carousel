using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Orchard Core Carousel",
    Author = "Michael Tran",
    Website = "https://github.com/tranpl",
    Version = "1.0.0",
    Description = "Adds a Bootstrap 5 Carousel Widget.",
    Dependencies = new[] { "OrchardCore.ContentFields", "OrchardCore.Flows", "OrchardCore.Media", "OrchardCore.Widgets" },
    Category = "Widget"
)]