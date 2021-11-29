namespace LitExplore.Client;

public static class TestEntities {
    // Papers
    public static Paper genericPaper = new Paper("contents",new List<string>{"An author", "another author"}, "Academic paper title", 1970,1,1, new List<string>{"academic", "scientific", "knowledge", "professionalism"});
    public static Paper howToGetStronk = new Paper("file",new List<string>{"Caspar", "Nadja", "Theis"}, "How to get stronk", 2021,5,29, new List<string>{"training", "health"});
    public static Paper howToMakeFood = new Paper("file1",new List<string>{"Caspar", "Theis"}, "How to make food", 2020,4,6, new List<string>{"food", "health"});
    public static Paper hihihaha = new Paper("doc", new List<string>() { "JK ROWLING", "HAKUNA MATATA", "HJÆLP"}, "HIHIHAHA", 2012, 12, 12, null);

    // Connections
    public static Connection testconn1 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Tag}, null);
    public static Connection testconn2 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Other}, "Jeg har sat dem sammen fordi jeg godt kan lide dem.");
    public static Connection testconn3 = new Connection(hihihaha,howToMakeFood,new List<ConnectionType>{ConnectionType.Reference, ConnectionType.Other}, "Cool papers, that I like.");
    
    // Users
    public static User user1 = new User {Id = 1, Connections = new List<Connection>()};
    public static User user2 = new User {Id = 2};
    public static User user3 = new User {Id = 3};
    public static User user4 = new User {Id = 4};
    public static User user5 = new User {Id = 5};

    // Teams
    public static Team team1 = new Team(user1, "The sejerejeste team") { Id = 1, Users = new List<User>() {user2, user3, user4, user5} };
}
