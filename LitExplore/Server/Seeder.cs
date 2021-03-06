namespace LitExplore.Server;

public static class Seeder
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LitExploreContext>();

            await SeedPapersAsync(context);
            await SeedTeamsAsync(context);
        }
        return host;
    }

    private static async Task SeedPapersAsync(LitExploreContext context)
    {
        context.Database.Migrate();

        if (!context.Papers.Any())
        {
            var documents = new[] {"One day the goat met a man called Dijkstra. 'Wow', he said to the goat, 'you can speak'. The man was fascinated and extremely surprised. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. One day the goat met a man called Dijkstra. This man was very smart and intelligent. 'Wow', he said to the goat, 'you can speak'. The next day he went on to create four new algorithms for pathfinding. 'CRAAZY!' thought the goat to itself. ", "This is an epic adventure with an undercurrent about camaraderie and the need to face death. The story is about a chief starship engineer, a champion, and a diabolical prince. It starts in a manufacturing city on a mountainous planet. The story climaxes with a holiday celebration. The future of warfare is a major part of this story.", "This is a comedy/drama with an undercurrent about wandering and how the invention of man can destroy him. The story is about an environmentalist. It takes place in a village on a desert planet. The story begins with someone writing a book.", "The story is about a serious conjurer who is obsessed with a pragmatic doctor. It takes place on a mountainous planet. A new kind of magic is evolving in the story.", "The story is about a gas station attendant. It starts in a large city in North America. The crux of the story involves the taking of a test.", "This is an epic about realism. The story is about a biologist who has a mysterious admirier: a realistic nun. It starts in an outpost in South America. The nature of magic is changing, and that plays an important role in the story.", "This is a crime drama. The story is about a plucky bard, a patriotic jock, an opinionated coachman, and a construction worker who has a crush on a misunderstood businessperson. It takes place in a village. The story climaxes with an eavesdropping. The destruction of a magical artifact plays an important role.", "The story is about a frustrated peasant who is constantly annoying a computer programmer. It takes place in a town on an ocean planet. The story begins with a sport being played and ends with a lecture. Genetic engineering and its side effects is a major part of this story.", "This is a comedy/drama with a focus on impotency. The story is about a virtual reality engineer who is engaged to an extraverted technician. It starts in a city-state on a dying world where magic is part of everyday life. The story ends with a chest being opened. A lost treasure plays an important role.", "This is a tragedy with a focus on people's tenancy to be dishonest. The story is about an astrologer who has a mysterious admirier: a courageous park ranger. It takes place on a desert planet. The destruction of a magical artifact plays an important role.", "The story is about a noble and a planetologist. It takes place at a portal to another solar system. Recovering magical artifacts and knowledge is a major part of the story.", "This is an action comedy with an emphasis on wisdom and the importance of brains over brawn. The story is about seven starship pilots. It takes place in a bank in a large city. The story begins with someone getting a new job and ends with a murder."};
            
            //Papers without author and tags
            Paper Paper1 = new Paper{Authors = new List<Author>{}, Title = "The goat", Date = new DateTime(2020, 10, 5), Tags = new List<Tag>{}, Document = documents[0]};
            Paper Paper2 = new Paper{Authors = new List<Author>{}, Title = "Eluminado", Date = new DateTime(2018, 8, 5), Tags = new List<Tag>{}, Document = documents[1]};
            Paper Paper3 = new Paper{Authors = new List<Author>{}, Title = "The wonders of BlueJ", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[2]};
            Paper Paper4 = new Paper{Authors = new List<Author>{}, Title = "McQueen", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[3]}; 
            Paper Paper5 = new Paper{Authors = new List<Author>{}, Title = "Harald Bluetooth", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[4]};
            Paper Paper6 = new Paper{Authors = new List<Author>{}, Title = "I don't like finibs", Date = new DateTime(1873, 7, 22), Tags = new List<Tag>{}, Document = documents[5]};
            Paper Paper7 = new Paper{Authors = new List<Author>{}, Title = "The downfall of Microsoft", Date = new DateTime(1995, 2, 5), Tags = new List<Tag>{}, Document = documents[6]};
            Paper Paper8 = new Paper{Authors = new List<Author>{}, Title = "Shitcoins are the new black", Date = new DateTime(1994, 7, 22), Tags = new List<Tag>{}, Document = documents[7]};
            Paper Paper9 = new Paper{Authors = new List<Author>{}, Title = "Algorithms - Fourth Edition", Date = new DateTime(2011, 4, 3), Tags = new List<Tag>{}, Document = documents[8]};
            Paper Paper10 = new Paper{Authors = new List<Author>{}, Title = "A Relational Model of Data for Large Shared Data Banks", Date = new DateTime(1970, 12, 24), Tags = new List<Tag>{}, Document = documents[9]};
            Paper Paper11 = new Paper{Authors = new List<Author>{}, Title = "Objects First with Java", Date = new DateTime(2003, 8, 7), Tags = new List<Tag>{}, Document = documents[10]};
            Paper Paper12 = new Paper{Authors = new List<Author>{}, Title = "Python Programming", Date = new DateTime(2009, 1, 27), Tags = new List<Tag>{}, Document = documents[11]};
            
            // Authors
            Author Sedgewick = new Author{Name = "Robert Sedgewick",Papers = new List<Paper>{Paper1, Paper2}};
            Author Dijkstra = new Author{Name = "Dijkstra", Papers = new List<Paper>{Paper3}};
            Author KameHame = new Author{Name = "Kame Hame", Papers = new List<Paper>{Paper4, Paper5, Paper9}};
            Author Dofensmirtz = new Author{Name = "Doc. Dofensmirtz", Papers = new List<Paper>{Paper6, Paper12}};
            Author Ronaldo = new Author{Name = "Ronaldo", Papers = new List<Paper>{Paper7}};
            Author Major = new Author{Name = "Major Lazor", Papers = new List<Paper>{Paper8}};
            Author Codd = new Author{Name = "Edgar F. Codd", Papers = new List<Paper>{Paper10}};
            Author Barnes = new Author{Name = "David J. Barnes", Papers = new List<Paper>{Paper11}};
            Author Kolling = new Author{Name = "Michael K??lling", Papers = new List<Paper>{Paper11}};
            Author Wayne = new Author{Name = "Kevin Wayne", Papers = new List<Paper>{Paper12}};
            
            // Tags 
            Tag Algorithm = new Tag{Name = "Algorithm", Papers = new List<Paper>{Paper2, Paper3}};
            Tag Physics = new Tag{Name = "Physics", Papers = new List<Paper>{Paper2, Paper3, Paper4}};
            Tag Maths = new Tag{Name = "Maths", Papers = new List<Paper>{Paper4}};
            Tag Drugs = new Tag{Name = "Drugs", Papers = new List<Paper>{Paper5, Paper6}};
            Tag Pathfinding = new Tag{Name = "Pathfinding", Papers = new List<Paper>{Paper8}};
            Tag Abuse = new Tag{Name = "Abuse", Papers = new List<Paper>{Paper6}};
            Tag Weapons = new Tag{Name = "Weapons", Papers = new List<Paper>{Paper6, Paper7, Paper12}};
            Tag Football = new Tag{Name = "Football", Papers = new List<Paper>{Paper7}};
            Tag Java = new Tag{Name = "Java", Papers = new List<Paper>{Paper9, Paper10, Paper11}};
            Tag Database = new Tag{Name = "Database", Papers = new List<Paper>{Paper10}};
            Tag Relational = new Tag{Name = "Relational Database", Papers = new List<Paper>{Paper10}};
            Tag CompSci = new Tag{Name = "Computer Science", Papers = new List<Paper>{Paper10}};
            Tag BlueJ = new Tag{Name = "BlueJ", Papers = new List<Paper>{Paper11}};
            Tag Programming = new Tag{Name = "Programming", Papers = new List<Paper>{Paper12}};
            Tag OOP = new Tag{Name = "Object Oriented Programming", Papers = new List<Paper>{Paper11}};
            Tag Python = new Tag{Name = "Python", Papers = new List<Paper>{Paper12}};

            // Adding tags and authors to papers
            Paper1.Authors = new List<Author>{Sedgewick};

            Paper2.Authors = new List<Author>{Sedgewick};
            Paper2.Tags = new List<Tag>{Algorithm, Physics, Pathfinding};
            
            Paper3.Authors = new List<Author>{Dijkstra};
            Paper3.Tags = new List<Tag>{Algorithm, Physics};

            Paper4.Authors = new List<Author>{KameHame, Dijkstra};
            Paper4.Tags = new List<Tag>{Physics, Maths, Drugs};

            Paper5.Authors = new List<Author>{KameHame};
            Paper5.Tags = new List<Tag>{Drugs};

            Paper6.Authors = new List<Author>{Dofensmirtz};
            Paper6.Tags = new List<Tag>{Drugs, Abuse, Weapons};

            Paper7.Authors = new List<Author>{Ronaldo};
            Paper7.Tags = new List<Tag>{Weapons, Football};

            Paper8.Authors = new List<Author>{Major};
            Paper8.Tags = new List<Tag>{Pathfinding};

            Paper9.Authors = new List<Author>{KameHame, Wayne};
            Paper9.Tags = new List<Tag>{Java};

            Paper10.Authors = new List<Author>{Codd};
            Paper10.Tags = new List<Tag>{Database, Relational, CompSci, Java};

            Paper11.Authors = new List<Author>{Barnes, Kolling};
            Paper11.Tags = new List<Tag>{Java, BlueJ, OOP};

            Paper12.Authors = new List<Author>{Wayne, Dofensmirtz};
            Paper12.Tags = new List<Tag>{Python, Programming, Weapons};

            var papers = new List<Paper>() {Paper1, Paper2, Paper3, Paper4, Paper5, Paper6, Paper7, Paper8, Paper9, Paper10, Paper11, Paper12};
            context.Papers.AddRange(papers);

            var conns = new List<Connection>();

            for(int i = 0; i < papers.Count; i++)
            {
                var p1 = papers[i];
                for(int j = i + 1; j < papers.Count; j++)
                {
                    var p2 = papers[j];
                    var connType = new List<String>();
                    foreach(Author a in p1.Authors) 
                    {
                        if(p2.Authors.Contains(a))
                        {
                            connType.Add("author");
                            break;
                        } 
                    }
                    foreach(Tag a in p1.Tags) 
                    {
                        if(p2.Tags.Contains(a)) 
                        {
                            connType.Add("tag");
                            break;
                        }    
                    }
                    if(connType.Count() > 0)
                    {
                        string type = String.Join(":", connType.ToArray());
                        if (p1 == Paper1 && p2 == Paper2 || p1 == Paper9 && p2 == Paper11) type = type + ":reference"; 
                        conns.Add(new Connection{Paper1 = p1, Paper1Id = p1.Id, Paper2 = p2, Paper2Id = p2.Id, ConnectionType = type, Description = ""});
                    }
                }
            }

            context.Connections.AddRange(conns);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedTeamsAsync(LitExploreContext context)
    {
        context.Database.Migrate();
        if(!context.Teams.Any())
        {
            User Nadja = new User{oid = "7ae4939f-b0be-42a1-a18e-c0dffd04f57d", Name = "Nadja Brix Koch"};
            User Theis = new User{oid = "0bee8cf0-a1c6-4eb5-bd63-ef02e2431526", Name = "Theis Stensgaard"};
            User Sebastian = new User{oid = "49e1ae65-96d3-4512-bb9f-9c9e9024e735", Name = "Sebastian Vestergaard Fugmann"};
            User Frederik = new User{oid = "0561a5ed-2923-44fa-9ec6-d89ea6ff1b24", Name = "Frederik Rothe"};
            User Caspar = new User{oid = "16442aef-a6d9-4c07-8b8f-6536a0af3a72", Name = "Caspar Marschall"};
            User Emil = new User{oid = "caba8b01-f802-4a62-a4db-6b34992627b7", Name = "Emil Houlborg"};
            
            Team haardSchneft = new Team
            {
                TeamLeader = Nadja, 
                Colour = 1, 
                Users = new List<User>{Nadja, Theis, Sebastian, Frederik, Caspar, Emil}, 
                TeamName = "H??rd Scneft"
            };

            Nadja.IsLeaderOf = new List<Team>{haardSchneft};
            Nadja.Teams = new List<Team>{haardSchneft};
            Theis.Teams = new List<Team>{haardSchneft};
            Sebastian.Teams = new List<Team>{haardSchneft};
            Frederik.Teams = new List<Team>{haardSchneft};
            Caspar.Teams = new List<Team>{haardSchneft};
            Emil.Teams = new List<Team>{haardSchneft};

            var theGoat = context.Papers.Where(p => p.Title == "The goat").Single();
            var downfall = context.Papers.Where(p => p.Title == "The downfall of Microsoft").Single();
            var shitcoins = context.Papers.Where(p => p.Title == "Shitcoins are the new black").Single();
            var objects = context.Papers.Where(p => p.Title == "Objects First with Java").Single();

            Connection c1 = new Connection 
            { 
                Paper1 = downfall, 
                Paper1Id = downfall.Id, 
                Paper2 = theGoat, 
                Paper2Id = theGoat.Id, 
                ConnectionType = "other", 
                Description = "I think these fit well together",
                Creator = Theis
            };
            
            Connection c2 = new Connection 
            { 
                Paper1 = objects, 
                Paper1Id = objects.Id, 
                Paper2 = shitcoins, 
                Paper2Id = shitcoins.Id, 
                ConnectionType = "other", 
                Description = "Same writing style",
                Creator = Nadja
            };

            haardSchneft.Connections = new List<Connection>() { c1, c2 };

            context.Teams.Add(haardSchneft);
            await context.SaveChangesAsync();
        }
    }
}