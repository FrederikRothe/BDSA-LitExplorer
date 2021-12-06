namespace LitExplore.Server;

public static class SeedExtensions
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LitExploreContext>();

            await SeedPapersAsync(context);
        }
        return host;
    }

    private static async Task SeedPapersAsync(LitExploreContext context)
    {
        context.Database.Migrate();

        if (!context.Papers.Any())
        {
            var documents = new[] {"One day the goat met a man called Dijkstra. This man was very smart and intelligent. 'Wow', he said to the goat, 'you can speak'. The man was fascinated and extremely surprised. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. Once upon a time there was a goat. This goat was very timid. The goat liked the colour red and was always hungry. One day the goat met a man called Dijkstra. This man was very smart and intelligent. 'Wow', he said to the goat, 'you can speak'. The man was fascinated and extremely surprised. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. ", "This is an epic adventure with an undercurrent about camaraderie and the need to face death. The story is about a chief starship engineer, a champion, and a diabolical prince. It starts in a manufacturing city on a mountainous planet. The story climaxes with a holiday celebration. The future of warfare is a major part of this story.", "This is a comedy/drama with an undercurrent about wandering and how the invention of man can destroy him. The story is about an environmentalist. It takes place in a village on a desert planet. The story begins with someone writing a book.", "The story is about a serious conjurer who is obsessed with a pragmatic doctor. It takes place on a mountainous planet. A new kind of magic is evolving in the story.", "The story is about a gas station attendant. It starts in a large city in North America. The crux of the story involves the taking of a test.", "This is an epic about realism. The story is about a biologist who has a mysterious admirier: a realistic nun. It starts in an outpost in South America. The nature of magic is changing, and that plays an important role in the story.", "This is a crime drama. The story is about a plucky bard, a patriotic jock, an opinionated coachman, and a construction worker who has a crush on a misunderstood businessperson. It takes place in a village. The story climaxes with an eavesdropping. The destruction of a magical artifact plays an important role.", "The story is about a frustrated peasant who is constantly annoying a computer programmer. It takes place in a town on an ocean planet. The story begins with a sport being played and ends with a lecture. Genetic engineering and its side effects is a major part of this story.", "This is a comedy/drama with a focus on impotency. The story is about a virtual reality engineer who is engaged to an extraverted technician. It starts in a city-state on a dying world where magic is part of everyday life. The story ends with a chest being opened. A lost treasure plays an important role.", "This is a tragedy with a focus on people's tenancy to be dishonest. The story is about an astrologer who has a mysterious admirier: a courageous park ranger. It takes place on a desert planet. The destruction of a magical artifact plays an important role.", "The story is about a noble and a planetologist. It takes place at a portal to another solar system. Recovering magical artifacts and knowledge is a major part of the story.", "This is an action comedy with an emphasis on wisdom and the importance of brains over brawn. The story is about seven starship pilots. It takes place in a bank in a large city. The story begins with someone getting a new job and ends with a murder."};
            
            //Papers without author and tags
            Paper Paper1 = new Paper{Authors = new List<Author>{}, Title = "The goat", Date = new DateTime(2020, 10, 5), Tags = new List<Tag>{}, Document = documents[0]};
            Paper Paper2 = new Paper{Authors = new List<Author>{}, Title = "Eluminado", Date = new DateTime(2018, 8, 5), Tags = new List<Tag>{}, Document = documents[1]};
            Paper Paper3 = new Paper{Authors = new List<Author>{}, Title = "El Mechanico", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[2]};
            Paper Paper4 = new Paper{Authors = new List<Author>{}, Title = "McQueen", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[3]}; 
            Paper Paper5 = new Paper{Authors = new List<Author>{}, Title = "Harald Bluetooth", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[4]};
            Paper Paper6 = new Paper{Authors = new List<Author>{}, Title = "I don't like finibs", Date = new DateTime(1873, 7, 22), Tags = new List<Tag>{}, Document = documents[5]};
            Paper Paper7 = new Paper{Authors = new List<Author>{}, Title = "El Mejor", Date = new DateTime(1995, 2, 5), Tags = new List<Tag>{}, Document = documents[6]};
            Paper Paper8 = new Paper{Authors = new List<Author>{}, Title = "El Algorithm", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[7]};
            Paper Paper9 = new Paper{Authors = new List<Author>{}, Title = "Algorithms - Fourth Edition", Date = new DateTime(2011, 4, 3), Tags = new List<Tag>{}, Document = documents[8]};
            Paper Paper10 = new Paper{Authors = new List<Author>{}, Title = "A Relational Model of Data for Large Shared Data Banks", Date = new DateTime(1970, 12, 24), Tags = new List<Tag>{}, Document = documents[9]};
            Paper Paper11 = new Paper{Authors = new List<Author>{}, Title = "Objects First with Java", Date = new DateTime(2003, 8, 7), Tags = new List<Tag>{}, Document = documents[10]};
            Paper Paper12 = new Paper{Authors = new List<Author>{}, Title = "Python Programming", Date = new DateTime(2009, 1, 27), Tags = new List<Tag>{}, Document = documents[11]};
            
            // Authors
            Author Sedgewick = new Author{Name = "Robert Sedgewick",Papers = new List<Paper>{Paper1, Paper2, Paper8}};
            Author Dijkstra = new Author{Name = "Dijkstra", Papers = new List<Paper>{Paper3}};
            Author KameHame = new Author{Name = "Kame Hame", Papers = new List<Paper>{Paper4, Paper5, Paper9}};
            Author Dofensmirtz = new Author{Name = "Doc. Dofensmirtz", Papers = new List<Paper>{Paper6, Paper12}};
            Author Ronaldo = new Author{Name = "Ronaldo", Papers = new List<Paper>{Paper7}};
            Author Major = new Author{Name = "Major Lazor", Papers = new List<Paper>{Paper8}};
            Author Codd = new Author{Name = "Edgar F. Codd", Papers = new List<Paper>{Paper10}};
            Author Barnes = new Author{Name = "David J. Barnes", Papers = new List<Paper>{Paper11}};
            Author Kolling = new Author{Name = "Michael Kölling", Papers = new List<Paper>{Paper11}};
            Author Wayne = new Author{Name = "Kevin Wayne", Papers = new List<Paper>{Paper12}};
            
            // Tags 
            Tag Algorithm = new Tag{Name = "Algorithm", Papers = new List<Paper>{Paper2, Paper3}};
            Tag Physics = new Tag{Name = "Physics", Papers = new List<Paper>{Paper2, Paper3, Paper4}};
            Tag Maths = new Tag{Name = "Maths", Papers = new List<Paper>{Paper4}};
            Tag Drugs = new Tag{Name = "Drugs", Papers = new List<Paper>{Paper5, Paper6}};
            Tag Pathfinding = new Tag{Name = "Pathfinding", Papers = new List<Paper>{Paper5, Paper8}};
            Tag Abuse = new Tag{Name = "Abuse", Papers = new List<Paper>{Paper6}};
            Tag Weapons = new Tag{Name = "Weapons", Papers = new List<Paper>{Paper6, Paper7, Paper12}};
            Tag Football = new Tag{Name = "Football", Papers = new List<Paper>{Paper7}};
            Tag Java = new Tag{Name = "Java", Papers = new List<Paper>{Paper9, Paper10, Paper11}};
            Tag Database = new Tag{Name = "Database", Papers = new List<Paper>{Paper10}};
            Tag Relational = new Tag{Name = "Relational Database", Papers = new List<Paper>{Paper10}};
            Tag CompSci = new Tag{Name = "Computer Science", Papers = new List<Paper>{Paper10}};
            Tag BlueJ = new Tag{Name = "BlueJ", Papers = new List<Paper>{Paper11}};
            Tag Programming = new Tag{Name = "Programming", Papers = new List<Paper>{Paper11, Paper12}};
            Tag OOP = new Tag{Name = "Object Oriented Programming", Papers = new List<Paper>{Paper11}};
            Tag Python = new Tag{Name = "Python", Papers = new List<Paper>{Paper12}};

            // Adding tags and authors to papers
            Paper1.Authors = new List<Author>{Sedgewick};

            Paper2.Authors = new List<Author>{Sedgewick};
            Paper2.Tags = new List<Tag>{Algorithm, Physics};
            
            Paper3.Authors = new List<Author>{Dijkstra};
            Paper3.Tags = new List<Tag>{Algorithm, Physics};

            Paper4.Authors = new List<Author>{KameHame};
            Paper4.Tags = new List<Tag>{Physics, Maths};

            Paper5.Authors = new List<Author>{KameHame};
            Paper5.Tags = new List<Tag>{Drugs, Pathfinding};

            Paper6.Authors = new List<Author>{Dofensmirtz};
            Paper6.Tags = new List<Tag>{Drugs, Abuse, Weapons};

            Paper7.Authors = new List<Author>{Ronaldo};
            Paper7.Tags = new List<Tag>{Weapons, Football};

            Paper8.Authors = new List<Author>{Sedgewick, Major};
            Paper8.Tags = new List<Tag>{Pathfinding};

            Paper9.Authors = new List<Author>{KameHame, Wayne};
            Paper9.Tags = new List<Tag>{Java};

            Paper10.Authors = new List<Author>{Codd};
            Paper10.Tags = new List<Tag>{Database, Relational, CompSci, Java};

            Paper11.Authors = new List<Author>{Barnes, Kolling};
            Paper11.Tags = new List<Tag>{Java, BlueJ, Programming, OOP};

            Paper12.Authors = new List<Author>{Wayne, Dofensmirtz};
            Paper12.Tags = new List<Tag>{Python, Programming, Weapons};

            // Add to context.
            context.Papers.AddRange(Paper1, Paper2, Paper3, Paper4, Paper5, Paper6, Paper7, Paper8, Paper9, Paper10, Paper11, Paper12);
            await context.SaveChangesAsync();
        }
    }
}