using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Flows.Models;
using OrchardCore.Media.Settings;

using System.Threading.Tasks;

namespace Carousel.OrchardCore.Migrations
{
    public class CarouselMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public CarouselMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync("Slide", cfg => cfg
                .WithDescription("Contains the fields for the current type")
                .WithField("Caption",
                    fieldBuilder => fieldBuilder
                        .OfType("HtmlField")
                        .WithDisplayName("Caption")
                        .WithEditor("Wysiwyg"))
                .WithField("DisplayCaption",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Display Caption"))
                .WithField("Image",
                    fieldBuilder => fieldBuilder
                        .OfType("MediaField")
                        .WithDisplayName("Image")
                        .WithSettings(new MediaFieldSettings { Required = true, Multiple = false }))
                .WithField("ImageClass",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Image Class"))
                .WithField("ImageAltText",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Image Alt Text"))
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("Slide", type => type
                .WithPart("Slide"));

            await _contentDefinitionManager.AlterPartDefinitionAsync("Carousel", cfg => cfg
                .WithDescription("Contains the fields for the current type")
                .WithField("Interval",
                    fieldBuilder => fieldBuilder
                        .OfType("NumericField")
                        .WithDisplayName("Interval")
                        .WithSettings(new NumericFieldSettings { Required = true, DefaultValue = "5000", Hint = "Delay between slides (ms)" }))
                .WithField("IncludeControls",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Include Controls"))
                .WithField("IncludeIndicators",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Include Indicators"))
                .WithField("Ride",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Autoplay"))
                .WithField("Wrap",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Continuous"))
                .WithField("Keyboard",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("React to keyboard"))
                .WithField("Pause",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Pause on hover/touch"))
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("Carousel", type => type
                 .WithPart("Carousel")
                 .WithPart("Slides", "BagPart", cfg => cfg
                     .WithDisplayName("Slides")
                     .WithDescription("Slides to display in the carousel.")
                     .WithSettings(new BagPartSettings { ContainedContentTypes = new[] { "Slide" }, DisplayType = "Detail" }))
                 .Stereotype("Widget"));

            return 1;
        }
        public async Task<int> UpdateFrom1Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(
                "Carousel",
                cfg => cfg
                .WithDescription("Define extra class for the Carousel")
                .WithField("CarouselClass",
                fieldBuilder => fieldBuilder
                    .OfType("TextField")
                    .WithDisplayName("Carousel Class"))
                );
            return 2;
        }
        public async Task<int> UpdateFrom2Async()
        {
            // Add MobileImage field to Slide part
            await _contentDefinitionManager.AlterPartDefinitionAsync(
                "Slide",
                cfg => cfg
                    .WithField("MobileImage",
                        fieldBuilder => fieldBuilder
                            .OfType("MediaField")
                            .WithDisplayName("Mobile Image")
                            .WithSettings(new MediaFieldSettings { Required = false, Multiple = false }))
            );

            return 3;
        }
    }
}