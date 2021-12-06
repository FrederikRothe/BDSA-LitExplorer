namespace LitExplore.Client;

public static class TestEntities {
    // Authors
    public static Author nadja = new Author() { Name = "Nadja" };
    public static Author caspar = new Author() { Name = "Caspar" };
    public static Author theis = new Author() { Name = "Theis" };
    public static Author emil = new Author() { Name = "Emil" };
    public static Author sebastian = new Author() { Name = "Sebastian" };
    public static Author frederik = new Author() { Name = "Frederik" };

    // Tags
    public static Tag academic = new Tag() { Name = "academic" };
    public static Tag scientific = new Tag() { Name = "scientific" };
    public static Tag knowledge = new Tag() { Name = "knowledge" };
    public static Tag professionalism = new Tag() { Name = "professionalism" };
    public static Tag training = new Tag() { Name = "training" };
    public static Tag health = new Tag() { Name = "health" };
    public static Tag food = new Tag() { Name = "food" };

    // Papers
    public static Paper genericPaper = new Paper() {
        Document = "contents",
        Authors = new List<Author>{sebastian, frederik},
        Title = "Academic paper title",
        Date = new DateTime(1970,1,1), 
        Tags = new List<Tag>{academic, scientific, knowledge, professionalism},
    };
    public static Paper howToGetStronk = new Paper() {
        Document = "file",
        Authors = new List<Author>{caspar, nadja, theis}, 
        Title = "How to get stronk", 
        Date = new DateTime(2021,5,29), 
        Tags = new List<Tag>{training, health},
    };
    public static Paper howToMakeFood = new Paper() {
        Document = "file1",
        Authors = new List<Author>{caspar, theis}, 
        Title = "How to make food", 
        Date = new DateTime(2020,4,6), 
        Tags = new List<Tag>{food, health}
    };
    public static Paper hihihaha = new Paper() {
        Document = "doc", 
        Authors = new List<Author>() {emil, theis, frederik}, 
        Title = "HIHIHAHA", 
        Date = new DateTime(2012, 12, 12), 
    };

    // Connections
    public static Connection testconn1 = new Connection() {
        Paper1 = howToGetStronk,
        Paper2 = howToMakeFood,
        ConnectionType = "author;tag;",
    };
    public static Connection testconn2 = new Connection() {
        Paper1 = howToGetStronk,
        Paper2 = howToMakeFood,
        ConnectionType = "author;other;", 
        Description = "Jeg har sat dem sammen fordi jeg godt kan lide dem.",
    };
    public static Connection testconn3 = new Connection() {
        Paper1 = hihihaha,
        Paper2 = howToMakeFood,
        ConnectionType = "reference;other;",
        Description = "Cool papers, that I like."
    };
    
    // Users
    public static User user1 = new User { Id = 1, oid = "1" };
    public static User user2 = new User { Id = 2, oid = "2" };
    public static User user3 = new User { Id = 3, oid = "3" };
    public static User user4 = new User { Id = 4, oid = "4" };
    public static User user5 = new User { Id = 5, oid = "5" };

    // Teams
    public static Team team1 = new Team() {
       TeamLeader = user1, 
       TeamName = "The sejerejeste team",
       Id = 1, 
       Users = new List<User>() {user2, user3, user4, user5}, 
    };
}
