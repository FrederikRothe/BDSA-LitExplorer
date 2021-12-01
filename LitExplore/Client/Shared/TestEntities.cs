namespace LitExplore.Client;

public static class TestEntities {
    // Authors
    public static Author nadja = new Author("Nadja");
    public static Author caspar = new Author("Caspar");
    public static Author theis = new Author("Theis");
    public static Author emil = new Author("Emil");
    public static Author sebastian = new Author("Sebastian");
    public static Author frederik = new Author("Frederik");

    // Tags
    public static Tag academic = new Tag("academic");
    public static Tag scientific = new Tag("scientific");
    public static Tag knowledge = new Tag("knowledge");
    public static Tag professionalism = new Tag("professionalism");
    public static Tag training = new Tag("training");
    public static Tag health = new Tag("health");
    public static Tag food = new Tag("food");

    // Papers
    public static Paper genericPaper = new Paper("contents",new List<Author>{sebastian, frederik}, "Academic paper title", 1970,1,1, new List<Tag>{academic, scientific, knowledge, professionalism});
    public static Paper howToGetStronk = new Paper("file",new List<Author>{caspar, nadja, theis}, "How to get stronk", 2021,5,29, new List<Tag>{training, health});
    public static Paper howToMakeFood = new Paper("file1",new List<Author>{caspar, theis}, "How to make food", 2020,4,6, new List<Tag>{food, health});
    public static Paper hihihaha = new Paper("doc", new List<Author>() {emil, theis, frederik}, "HIHIHAHA", 2012, 12, 12, null);

    // Connections
    public static Connection testconn1 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Tag}, null);
    public static Connection testconn2 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Other}, "Jeg har sat dem sammen fordi jeg godt kan lide dem.");
    public static Connection testconn3 = new Connection(hihihaha,howToMakeFood,new List<ConnectionType>{ConnectionType.Reference, ConnectionType.Other}, "Cool papers, that I like.");
    
    // Users
    public static User user1 = new User {Id = 1};
    public static User user2 = new User {Id = 2};
    public static User user3 = new User {Id = 3};
    public static User user4 = new User {Id = 4};
    public static User user5 = new User {Id = 5};

    // Teams
    public static Team team1 = new Team(user1, "The sejerejeste team") { Id = 1, Users = new List<User>() {user2, user3, user4, user5} };
}
