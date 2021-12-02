namespace LitExplore.ApplicationLogic;

public static class SeedExtensions
{
    public static IHost Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LitExploreContext>();

            SeedPapers(context);
        }
        return host;
    }

    private static void SeedPapers(LitExploreContext context)
    {
        context.Database.Migrate();

        if (!context.Papers.Any())
        {
            var documents = new[] {"Once upon a time there was a goat. This goat was very timid. The goat liked the colour red and was always hungry. One day the goat met a man called Dijkstra. This man was very smart and intelligent. 'Wow', he said to the goat, 'you can speak'. The man was fascinated and extremely surprised. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. Once upon a time there was a goat. This goat was very timid. The goat liked the colour red and was always hungry. One day the goat met a man called Dijkstra. This man was very smart and intelligent. 'Wow', he said to the goat, 'you can speak'. The man was fascinated and extremely surprised. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. ", "This is an epic adventure with an undercurrent about camaraderie and the need to face death. The story is about a chief starship engineer, a champion, and a diabolical prince. It starts in a manufacturing city on a mountainous planet. The story climaxes with a holiday celebration. The future of warfare is a major part of this story.", "This is a comedy/drama with an undercurrent about wandering and how the invention of man can destroy him. The story is about an environmentalist. It takes place in a village on a desert planet. The story begins with someone writing a book.", "The story is about a serious conjurer who is obsessed with a pragmatic doctor. It takes place on a mountainous planet. A new kind of magic is evolving in the story.", "The story is about a gas station attendant. It starts in a large city in North America. The crux of the story involves the taking of a test.", "This is an epic about realism. The story is about a biologist who has a mysterious admirier: a realistic nun. It starts in an outpost in South America. The nature of magic is changing, and that plays an important role in the story.", "This is a crime drama. The story is about a plucky bard, a patriotic jock, an opinionated coachman, and a construction worker who has a crush on a misunderstood businessperson. It takes place in a village. The story climaxes with an eavesdropping. The destruction of a magical artifact plays an important role.", "The story is about a frustrated peasant who is constantly annoying a computer programmer. It takes place in a town on an ocean planet. The story begins with a sport being played and ends with a lecture. Genetic engineering and its side effects is a major part of this story.", "This is a comedy/drama with a focus on impotency. The story is about a virtual reality engineer who is engaged to an extraverted technician. It starts in a city-state on a dying world where magic is part of everyday life. The story ends with a chest being opened. A lost treasure plays an important role.", "This is a tragedy with a focus on people's tenancy to be dishonest. The story is about an astrologer who has a mysterious admirier: a courageous park ranger. It takes place on a desert planet. The destruction of a magical artifact plays an important role.", "The story is about a noble and a planetologist. It takes place at a portal to another solar system. Recovering magical artifacts and knowledge is a major part of the story.", "This is an action comedy with an emphasis on wisdom and the importance of brains over brawn. The story is about seven starship pilots. It takes place in a bank in a large city. The story begins with someone getting a new job and ends with a murder."};
            
            context.Papers.AddRange( 
            new Paper(documents[0], new List<Author> {new Author("Robert Sedgewick")}, "The goat", 2020, 10, 5, null),
            new Paper(documents[1], new List<Author> {new Author("Robert Sedgewick")}, "Eluminado", 2018, 8, 5, new List<Tag> {new Tag("Algorithm"), new Tag("Physics")}),
            new Paper(documents[2], new List<Author> {new Author("Dijkstra")}, "El Mechanico", 1994, 7, 22, new List<Tag> {new Tag("Algorithm"), new Tag("Physics")}),
            new Paper(documents[3], new List<Author> {new Author("Kame hame")}, "McQueen", 1994, 7, 22, new List<Tag> {new Tag("Physics"), new Tag("Maths")}),
            new Paper(documents[4], new List<Author> {new Author("Kame hame")}, "Harald Bluetooth", 1994, 7, 22, new List<Tag> {new Tag("Drugs"), new Tag("Pathfinding")}),
            new Paper(documents[5], new List<Author> {new Author("Doc. Dofensmirtz")}, "I don't like finibs", 1873, 7, 22, new List<Tag> {new Tag("Drugs"), new Tag("Abuse"), new Tag("Weapons")}),
            new Paper(documents[6], new List<Author> {new Author("Ronaldo")}, "El Mejor", 1995, 2, 5, new List<Tag> {new Tag("Weapons"), new Tag("Football")}),
            new Paper(documents[7], new List<Author> {new Author("Robert Sedgewick"), new Author("Major Lazor")}, "El Mechanico", 1994, 7, 22, new List<Tag> {new Tag("Pathfinding")}),
            new Paper(documents[8], new List<Author> {new Author("Kame hame"), new Author("Kevin Wayne")}, "Algorithms - Fourth Edition", 2011, 4, 3, new List<Tag> {new Tag("Java")}),
            new Paper(documents[9], new List<Author> {new Author("Edgar F. Codd")}, "A Relational Model of Data for Large Shared Data Banks", 1970, 12, 24, new List<Tag> {new Tag("Database"), new Tag("Relational Database"), new Tag("Computer Science"), new Tag("Java")}),
            new Paper(documents[10], new List<Author> {new Author("David J. Barnes"), new Author("Michael Kölling")}, "Objects First with Java", 2003, 8, 7, new List<Tag> {new Tag("Java"), new Tag("BlueJ"), new Tag("Programming"), new Tag("Object Oriented Programming")}),
            new Paper(documents[11], new List<Author> {new Author("Kevin Wayne"), new Author("Doc. Dofensmirtz")}, "Python Programming", 2009, 1, 27, new List<Tag> {new Tag("Python"), new Tag("Programming"), new Tag("Weapons")})
            );
            context.SaveChanges();
        }
    }
}