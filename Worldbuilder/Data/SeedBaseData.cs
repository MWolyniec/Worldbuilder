using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Worldbuilder.Model;

namespace Worldbuilder.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, bool development)
        {
            using (var context = new WorldbuilderContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WorldbuilderContext>>()))
            {
                if (context.Categories.Any() || context.Bricks.Any()) return;

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
                var itemsOfCommonUse = new CategoryType()
                {
                    Name = "items of common use"
                };
                var technology = new CategoryType()
                {
                    Name = "technology"
                };
                var roleInSociety = new CategoryType()
                {
                    Name = "role in society"
                };
                var astronomy = new CategoryType()
                {
                    Name = "astronomy"
                };

                #endregion

                var catTypes = new[] { geography, beliefs, cosmology, biology, something, beings, itemsOfCommonUse, technology, roleInSociety, astronomy };

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
                var capital = new Category
                {
                    Name = "Capital",
                    Description = "The municipality exercising primary status in a country, state, province, or other administrative region, usually as its seat of government.",
                    CategoryType = geography
                };
                var animal = new Model.Category
                {
                    Name = "Animal",
                    Description = "Multicellular eukaryotic organisms that form the biological kingdom Animalia.",
                    CategoryType = beings
                };
                var furniture = new Model.Category
                {
                    Name = "Furniture",
                    Description = "Furniture refers to movable objects intended to support various human activities such as seating (e.g., chairs, stools, and sofas), eating (tables), and sleeping (e.g., beds).",
                    CategoryType = itemsOfCommonUse
                };
                var transport = new Model.Category
                {
                    Name = "Transport",
                    Description = "Transport or transportation is the movement of humans, animals and goods from one location to another. In other words, the action of transport is defined as a particular movement of an organism or thing from a point A to a Point B.",
                    CategoryType = itemsOfCommonUse
                };
                var machine = new Model.Category
                {
                    Name = "Machine",
                    Description = "A machine (or mechanical device) is a mechanical structure that uses power to apply forces and control movement to perform an intended action.",
                    CategoryType = technology
                };
                var inventor = new Model.Category
                {
                    Name = "Inventor",
                    Description = "An inventor is a person who creates or discovers a new method, form, device or other useful means that becomes known as an invention.",
                    CategoryType = roleInSociety
                };
                var scientist = new Model.Category
                {
                    Name = "Scientist",
                    Description = "A scientist is someone who conducts scientific research to advance knowledge in an area of interest.",
                    CategoryType = roleInSociety
                };
                var planet = new Model.Category
                {
                    Name = "Planet",
                    Description = "A planet is an astronomical body orbiting a star or stellar remnant that is massive enough to be rounded by its own gravity, is not massive enough to cause thermonuclear fusion, and has cleared its neighbouring region of planetesimals.",
                    CategoryType = astronomy
                };
                #endregion

                var categories = new[]
                {world, ocean, continent, sea, country, city, river, race, person, capital, animal, furniture, transport, machine, inventor, scientist};

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

                var london = new Model.Brick
                {
                    Name = "London",
                    ShortDesc = "Capital of the United Kingdom.",
                    LongDesc = @"London is the capital and largest city of England and of the United Kingdom.Standing on the River Thames in the south-east of England, at the head of its 50-mile (80 km) estuary leading to the North Sea, London has been a major settlement for two millennia. Londinium was founded by the Romans.[9] The City of London, London's ancient core − an area of just 1.12 square miles (2.9 km2) and colloquially known as the Square Mile − retains boundaries that closely follow its medieval limits. The City of Westminster is also an Inner London borough holding city status. Greater London is governed by the Mayor of London and the London Assembly.

  London is considered to be one of the world's most important global cities and has been termed the world's most powerful, most desirable, most influential, most visited, most expensive, innovative, sustainable, most investment friendly, and most popular for work[29] city. London exerts a considerable impact upon the arts, commerce, education, entertainment, fashion, finance, healthcare, media, professional services, research and development, tourism and transportation.[30][31] London ranks 26th out of 300 major cities for economic performance.[32] It is one of the largest financial centres[33] and has either the fifth or the sixth largest metropolitan area GDP.[note 3][34][35][36][37][38] It is the most-visited city as measured by international arrivals[39] and has the busiest city airport system as measured by passenger traffic.[40] It is the leading investment destination,[41][42][43][44] hosting more international retailers[45][46] and ultra high-net-worth individuals[47][48] than any other city. London's universities form the largest concentration of higher education institutes in Europe,[49] and London is home to highly ranked institutions such as Imperial College London in natural and applied sciences, the London School of Economics in social sciences, and the comprehensive University College London.[50][51][52] In 2012, London became the first city to have hosted three modern Summer Olympic Games.[53]

  London has a diverse range of people and cultures, and more than 300 languages are spoken in the region.[54] Its estimated mid-2018 municipal population (corresponding to Greater London) was 8,908,081,[4] the third most populous of any city in Europe[55] and accounts for 13.4% of the UK population.[56] London's urban area is the third most populous in Europe, after Paris and Istanbul, with 9,787,426 inhabitants at the 2011 census.[57] The population within the London commuter belt is the most populous in Europe with 14,040,163 inhabitants in 2016.[note 4][3][58] London was the world's most populous city from c. 1831 to 1925.[59]

  London contains four World Heritage Sites: the Tower of London; Kew Gardens; the site comprising the Palace of Westminster, Westminster Abbey, and St Margaret's Church; and the historic settlement in Greenwich where the Royal Observatory, Greenwich defines the Prime Meridian (0° longitude) and Greenwich Mean Time.[60] Other landmarks include Buckingham Palace, the London Eye, Piccadilly Circus, St Paul's Cathedral, Tower Bridge, Trafalgar Square and The Shard. London has numerous museums, galleries, libraries and sporting events. These include the British Museum, National Gallery, Natural History Museum, Tate Modern, British Library and West End theatres.[61] The London Underground is the oldest underground railway network in the world."

                };

                var newYork = new Model.Brick
                {
                    Name = "New York City",
                    ShortDesc = "The most populous city in the United States",
                    LongDesc = @"New York City (NYC), also known as the City of New York or simply New York (NY), is the most populous city in the United States. With an estimated 2018 population of 8,398,748 distributed over a land area of about 302.6 square miles (784 km2), New York is also the most densely populated major city in the United States.[10] Located at the southern tip of the state of New York, the city is the center of the New York metropolitan area, the largest metropolitan area in the world by urban landmass[11] and one of the world's most populous megacities,[12][13] with an estimated 19,979,477 people in its 2018 Metropolitan Statistical Area and 22,679,948 residents in its Combined Statistical Area.[3][4] A global power city,[14] New York City has been described as the cultural,[15][16][17][18][19] financial,[20][21][22] and media capital of the world,[23][24] and exerts a significant impact upon commerce,[22] entertainment, research, technology, education, politics, tourism, art, fashion, and sports. The city's fast pace[25][26][27] has inspired the term New York minute.[28] Home to the headquarters of the United Nations,[29] New York is an important center for international diplomacy.[30][31]

Situated on one of the world's largest natural harbors,[32][33] New York City consists of five boroughs, each of which is a separate county of the State of New York.[34] The five boroughs – Brooklyn, Queens, Manhattan, The Bronx, and Staten Island – were consolidated into a single city in 1898.[35] The city and its metropolitan area constitute the premier gateway for legal immigration to the United States.[36] As many as 800 languages are spoken in New York,[37][38][39][40] making it the most linguistically diverse city in the world.[39][41][42] New York City is home to 3.2 million residents born outside the United States,[43] the largest foreign born population of any city in the world as of 2016.[44] As of 2019, the New York metropolitan area is estimated to produce a gross metropolitan product (GMP) of US$1.9 trillion.[8] If greater New York City were a sovereign state, it would have the 12th highest GDP in the world.[45] New York is home to the highest number of billionaires of any city in the world.[46]

New York City traces its origins to a trading post founded by colonists from the Dutch Republic in 1624 on Lower Manhattan; the post was named New Amsterdam in 1626.[47] The city and its surroundings came under English control in 1664[47] and were renamed New York after King Charles II of England granted the lands to his brother, the Duke of York.[48] New York was the capital of the United States from 1785 until 1790,[49] and has been the largest US city since 1790.[50] The Statue of Liberty greeted millions of immigrants as they came to the U.S. by ship in the late 19th and early 20th centuries[51] and is an international symbol of the U.S. and its ideals of liberty and peace.[52] In the 21st century, New York has emerged as a global node of creativity and entrepreneurship,[53] social tolerance,[54] and environmental sustainability,[55][56] and as a symbol of freedom and cultural diversity.[57] In 2019, New York was voted the greatest city in the world per a survey of over 30,000 people from 48 cities worldwide, citing its cultural diversity.[15]

Many districts and landmarks in New York City are well known, including three of the world's ten most visited tourist attractions in 2013;[58] a record 62.8 million tourists visited in 2017.[59] Several sources have ranked New York the most photographed city in the world.[60][61] Times Square, iconic as the world's 'heart'[62] and 'crossroads',[63] is the brightly illuminated hub of the Broadway Theater District,[64] one of the world's busiest pedestrian intersections,[65][66] and a major center of the world's entertainment industry.[67] The names of many of the city's landmarks, skyscrapers,[68] and parks are known internationally. Manhattan's real estate market is among the most expensive in the world.[69][70] New York is home to the largest ethnic Chinese population outside of Asia,[71][72] with multiple distinct Chinatowns across the city.[73][74][75] Providing continuous 24/7 service,[76] the New York City Subway is the largest single-operator rapid transit system worldwide, with 472 rail stations.[77][78][79] The city has over 120 colleges and universities, including Columbia University, New York University, and Rockefeller University, ranked among the top universities in the world.[80][81] Anchored by Wall Street in the Financial District of Lower Manhattan, New York has been called both the most economically powerful city and world's leading financial center,[22][82][83][84] and is home to the world's two largest stock exchanges by total market capitalization, the New York Stock Exchange and NASDAQ.[85][86]"

                };
                var dog = new Model.Brick
                {
                    Name = "Dog",
                    ShortDesc = "Domestic animal, a best friend for some, food for others.",
                    LongDesc = @"The domestic dog (Canis lupus familiaris when considered a subspecies of the wolf or Canis familiaris when considered a distinct species)[5] is a member of the genus Canis (canines), which forms part of the wolf-like canids,[6] and is the most widely abundant terrestrial carnivore.[7][8][9][10][11] The dog and the extant gray wolf are sister taxa[12][13][14] as modern wolves are not closely related to the wolves that were first domesticated,[13][14] which implies that the direct ancestor of the dog is extinct.[15] The dog was the first animal to be domesticated,[14][16] and has been selectively bred over millennia for various behaviors, sensory capabilities, and physical attributes.[17]

      Their long association with humans has led dogs to be uniquely attuned to human behavior[18] and they are able to thrive on a starch-rich diet that would be inadequate for other canids.[19] Dogs vary widely in shape, size and colors.[20] They perform many roles for humans, such as hunting, herding, pulling loads, protection, assisting police and military, companionship and, more recently, aiding disabled people and therapeutic roles. This influence on human society has given them the sobriquet of 'man's best friend'."

                };
                var train = new Model.Brick
                {
                    Name = "Train",
                    ShortDesc = "A form of rail transport consisting of a series of connected vehicles"
                };
                var table = new Model.Brick
                {
                    Name = "Table",
                    ShortDesc = "An item of furniture with a flat top and one or more legs"
                };
                var tesla = new Model.Brick
                {
                    Name = "Nikola Tesla",
                    ShortDesc = "a Serbian-American inventor"
                };
                var einstein = new Model.Brick
                {
                    Name = "Albert Einstein",
                    ShortDesc = "A German-born theoretical physicist"
                };
                var uk = new Model.Brick
                {
                    Name = "United Kingdom",
                    ShortDesc = "The United Kingdom of Great Britain and Northern Ireland, commonly known as the United Kingdom (abbreviated to UK or U.K.) or Britain, is a sovereign country located off the north­western coast of the European mainland."
                };
                var europe = new Model.Brick
                {
                    Name = "Europe",
                    ShortDesc = "Europe is a continent located entirely in the Northern Hemisphere and mostly in the Eastern Hemisphere."
                };
                var australia = new Model.Brick
                {
                    Name = "Australia",
                    ShortDesc = "Australia, officially the Commonwealth of Australia, is a sovereign country comprising the mainland of the Australian continent, the island of Tasmania, and numerous smaller islands."
                };
                var earth = new Model.Brick
                {
                    Name = "Earth",
                    ShortDesc = "Earth is the third planet from the Sun and the only astronomical object known to harbor life."
                };
                #endregion

                var bricks = new[]
                { sampleWorld, sampleContinent, secondContinent, cityState, london, newYork, dog, train, table, tesla, einstein, uk, europe, australia, earth};



                var brickCats = new[]
                {
                    new BrickCategory{Brick = sampleWorld, Category = world},
                    new BrickCategory{Brick = sampleContinent, Category = continent},
                    new BrickCategory{Brick = secondContinent, Category = continent},
                    new BrickCategory{Brick = cityState, Category = city},
                    new BrickCategory{Brick = cityState, Category = country},
                    new BrickCategory{Brick = london, Category = city},
                    new BrickCategory{Brick = london, Category = capital},

                    new BrickCategory{Brick = uk, Category = country},
                    new BrickCategory{Brick = europe, Category = continent},

                    new BrickCategory{Brick = australia, Category = continent},

                    new BrickCategory{Brick = australia, Category = country},
                    new BrickCategory{Brick = newYork, Category = city},
                    new BrickCategory{Brick = dog, Category = animal},
                    new BrickCategory{Brick = table, Category = furniture},
                    new BrickCategory{Brick = train, Category = transport},
                    new BrickCategory{Brick = train, Category = machine},
                    new BrickCategory{Brick = tesla, Category = person},
                    new BrickCategory{Brick = tesla, Category = inventor},
                    new BrickCategory{Brick = einstein, Category = person},
                    new BrickCategory{Brick = einstein, Category = scientist},

                    new BrickCategory{Brick = earth, Category = planet}

                };

                var brToBr = new[]
                {
                    new BrickToBrick{Brick = sampleWorld, Child = sampleContinent},
                    new BrickToBrick{Brick = sampleWorld, Child = secondContinent},

                    new BrickToBrick{Brick = europe, Child = uk},
                    new BrickToBrick{Brick = uk, Child = london},
                    new BrickToBrick{Brick = sampleContinent, Child = cityState},

                    new BrickToBrick{Brick = earth, Child = europe},

                    new BrickToBrick{Brick = earth, Child = australia},

                };



                if (development)
                {
                    foreach (var item in catTypes)
                    {
                        context.CategoryTypes.Add(item);
                        context.SaveChanges();
                    }
                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                        context.SaveChanges();
                    }
                    foreach (var item in bricks)
                    {
                        context.Bricks.Add(item);
                        context.SaveChanges();
                    }
                    foreach (var item in brickCats)
                    {
                        context.BrickCategories.Add(item);
                        context.SaveChanges();
                    }
                    foreach (var item in brToBr)
                    {
                        context.BrickToBrick.Add(item);
                        context.SaveChanges();
                    }

                }
                else
                {
                    context.CategoryTypes.AddRange(catTypes);
                    context.Categories.AddRange(categories);
                    context.Bricks.AddRange(bricks);
                    context.BrickCategories.AddRange(brickCats);
                    context.BrickToBrick.AddRange(brToBr);

                    context.SaveChanges();

                }



            }
        }
    }
}
