namespace LitExplore.ApplicationLogic.Tests;

public class Seeder {
    public static LitExploreContext SeedInMemoryDB(LitExploreContext context)
    {
      // Entities to add to teams
        // Tag
        Tag Health = new Tag{Id = 1, Name = "Health", Papers = new List<Paper>()};

        // Papers
        Paper Fit = new Paper{Id = 1, Authors = new List<Author>(), Title = "Fit", Date = new DateTime(2000,1,1), Tags = new List<Tag>{Health}, Document = "1", AsPaper1 = new List<Connection>(), AsPaper2 = new List<Connection>()};
        Paper Obese = new Paper{Id = 2, Authors = new List<Author>(), Title = "Obese", Date = new DateTime(2000,1,1), Tags = new List<Tag>{Health}, Document = "2", AsPaper1 = new List<Connection>(), AsPaper2 = new List<Connection>()};

        // Connections
        Connection Math = new Connection{Id = 1, Creator = null,  Paper1 = Fit, Paper1Id = 1, Paper2 = Obese, Paper2Id = 2, ConnectionType = "Math", Description = "1", Teams = new List<Team>()};
        Connection Science = new Connection{Id = 2, Creator = null, Paper1 = Obese, Paper1Id = 2,  Paper2 = Fit, Paper2Id = 1, ConnectionType = "Science", Description = "2", Teams = new List<Team>()};
        Connection Physics = new Connection{Id = 3, Creator = null,  Paper1 = Fit, Paper1Id = 1, Paper2 = Obese, Paper2Id = 2, ConnectionType = "Physics", Description = "3", Teams = new List<Team>()};
        
        // Users 
        User Bob = new User{Id = 1, oid = "1", Name = "Bob", IsLeaderOf = new List<Team>(), Connections = new List<Connection>{Math, Science}, Teams = new List<Team>()};
        User Suzie = new User{Id = 2, oid = "2", Name = "Suzie", IsLeaderOf = new List<Team>(), Connections = new List<Connection>{Physics}, Teams = new List<Team>()};
        User Robert = new User{Id = 3, oid = "3", Name = "Robert", IsLeaderOf = new List<Team>(), Connections = new List<Connection>{Science}, Teams = new List<Team>()};

        // Authors
        Author ABob = new Author{Id = 1, Name = "Bob", Papers = new List<Paper>()};
        Author ASuzie = new Author{Id = 2, Name = "Suzie", Papers = new List<Paper>()};
        Author ARobert = new Author{Id = 3, Name = "Robert", Papers = new List<Paper>()};

        // Teams
        Team Potato = new Team{Id = 1, TeamLeader = Bob, Colour = 1, Users = new List<User>{Bob, Suzie}, Connections = new List<Connection>{Math, Physics}, TeamName = "Potato"};
        Team Orange = new Team{Id = 2, TeamLeader = Suzie, Colour = 2, Users = new List<User>{Robert, Suzie}, Connections = new List<Connection>{Science, Physics}, TeamName = "Orange"};
        Team Candy = new Team{Id = 3, TeamLeader = Robert, Colour = 3, Users = new List<User>{Robert}, Connections = new List<Connection>{Science}, TeamName = "Candy"};
       
        // Update dependices in entities
        Health.Papers = new List<Paper>{Fit, Obese};

        Fit.Authors = new List<Author>{ABob, ASuzie};
        Fit.AsPaper1 = new List<Connection>{Math, Physics};
        Fit.AsPaper2 = new List<Connection>{Science};

        Obese.Authors = new List<Author>{ARobert};
        Obese.AsPaper1 = new List<Connection>{Science};
        Obese.AsPaper2 = new List<Connection>{Math, Physics};

        Math.Creator = Bob;
        Math.Teams = new List<Team>{Potato};

        Science.Creator = Suzie;
        Science.Teams = new List<Team>{Orange, Candy};

        Physics.Creator = Robert;
        Physics.Teams = new List<Team>{Potato, Orange};

        Bob.IsLeaderOf = new List<Team>{Potato};
        Bob.Teams = new List<Team>{Potato};

        Suzie.IsLeaderOf = new List<Team>{Orange};
        Suzie.Teams = new List<Team>{Potato, Orange};

        Robert.IsLeaderOf = new List<Team>{Candy};
        Robert.Teams = new List<Team>{Orange, Candy};

        ABob.Papers = new List<Paper>{Fit};

        ASuzie.Papers = new List<Paper>{Fit};

        ARobert.Papers = new List<Paper>{Obese};

        // Add and save -- Apperently if you dont save after each a circular dependency will arrive
        context.Tags.AddRange(Health);
        //context.SaveChangesAsync();
        context.Authors.AddRange(ABob, ASuzie, ARobert);
        //context.SaveChangesAsync();
        context.Papers.AddRange(Fit, Obese);
        //context.SaveChangesAsync();
        context.Users.AddRange(Bob, Suzie, Robert);
        //context.SaveChangesAsync();
        context.Connections.AddRange(Math, Science, Physics);
        //context.SaveChangesAsync();
        context.Teams.AddRange(Potato, Orange, Candy);
        context.SaveChangesAsync();
        return context;
    }
}