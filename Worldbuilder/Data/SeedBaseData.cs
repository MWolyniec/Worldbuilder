using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Worldbuilder.Model;

namespace Worldbuilder.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new WorldbuilderContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WorldbuilderContext>>()))
            {
                if(context.Categories.Any() || context.Brick.Any()) return;

                #region Category Types

                var geography = new CategoryType()
                {
                    Name = "geography"
                };

                var beliefs = new CategoryType()
                {
                    Name = "beliefs, religion, spirituality"
                };

                var cosmology = new CategoryType()
                {
                    Name = "cosmology"
                };

                var biology = new CategoryType()
                {
                    Name = "biology"
                };

                var something = new CategoryType()
                {
                    Name = "something"
                };

                var beings = new CategoryType()
                {
                    Name = "beings"
                };

                #endregion

                var catTypes = new[] { geography, beliefs, cosmology, biology };

                #region Categories
                var ocean = new Model.Category
                {
                    Name = "Ocean",
                    Description = "Vast and a continuous frame of salty water, often taking a large part of planet's surface.",
                    CategoryType = geography
                };
                var sea = new Model.Category
                {
                    Name = "Sea",
                    Description = "Body of salt water, smaller and shallower than ocean.",
                    CategoryType = geography
                };

                var continent = new Model.Category
                {
                    Name = "Continent",
                    Description = "Any of a world's main continuous expanses of land.",
                    CategoryType = geography
                };
                var world = new Model.Category
                {
                    
                    Name = "World",
                    Description = "Place in the universe or the multiverse characterized by connected set of culture or history." +
                         "Usually one planet with the surrounding area.",
                    CategoryType = cosmology
                };
                var river = new Model.Category
                {
                    Name = "River",
                    Description = "A large natural stream of water flowing in a channel to the sea, a lake, or another river.",
                    CategoryType = geography
                };
                var city = new Model.Category
                {
                    Name = "City",
                    Description = "A built-up area with a name, defined boundaries, and local government, that is larger than a town and a village.",
                    CategoryType = geography
                };
                var person = new Model.Category
                {
                    Name = "Person",
                    Description = "A conscious being regarded as an individual.",
                    CategoryType = beings
                };
                var race = new Model.Category
                {
                    Name = "Race",
                    Description = "A group or set of beings or things with a common feature or features.",
                    CategoryType = beings
                };
                var country = new Model.Category
                {
                    Name = "Country",
                    Description = "A nation with its own government, occupying a particular territory.",
                    CategoryType = geography
                };
                #endregion

                var categories = new[]
                {world, ocean, continent, sea, country, city, river, race, person};

                #region Sample bricks

                var sampleWorld = new Model.Brick
                {
                    Name = "Sample world",
                    ShortDesc = "Just a sample world"
                };
                var sampleContinent = new Model.Brick
                {
                    Name = "Sample continent",
                    ShortDesc = "Just a sample continent"
                };
                var secondContinent = new Model.Brick
                {
                    Name = "Second continent",
                    ShortDesc = "Just another sample continent"
                };
                var cityState = new Model.Brick
                {
                    Name = "Sample city-state",
                    ShortDesc = "Just a sample that has more than 1 category assigned"
                };
                #endregion

                var bricks = new[]
                { sampleWorld, sampleContinent, cityState};

               

                var brickCats = new[]
                {
                    new BrickCategory{Brick = sampleWorld, Category = world},
                    new BrickCategory{Brick = sampleContinent, Category = continent},
                    new BrickCategory{Brick = secondContinent, Category = continent},
                    new BrickCategory{Brick = cityState, Category = city},
                    new BrickCategory{Brick = cityState, Category = country}
                };

                var brToBr = new[]
                {
                    new BrickToBrick{Brick = sampleWorld, Child = sampleContinent},
                    new BrickToBrick{Brick = sampleWorld, Child = secondContinent},
                    new BrickToBrick{Brick = sampleContinent, Child = cityState}
                };

                context.CategoryTypes.AddRange(catTypes);
                context.Categories.AddRange(categories);
                context.Brick.AddRange(bricks);
                context.BrickCategories.AddRange(brickCats);
                context.BrickToBrick.AddRange(brToBr);

                   context.SaveChanges(); 
       

               
            }
        }
    }
}
