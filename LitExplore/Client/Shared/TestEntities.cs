namespace LitExplore.Client;

public static class TestEntities {
    public static Paper howToGetStronk = new Paper("file",new List<string>{"Caspar", "Nadja", "Theis"}, "How to get stronk", 2021,5,29, new List<string>{"training", "health"});
    public static Paper howToMakeFood = new Paper("file1",new List<string>{"Caspar", "Theis"}, "How to make food", 2020,4,6, new List<string>{"food", "health"});
    public static Paper hihihaha = new Paper("doc", new List<string>() { "JK ROWLING", "HAKUNA MATATA", "HJÆLP"}, "HIHIHAHA", 2012, 12, 12, null);

    public static Connection testconn1 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Tag}, null);
    public static Connection testconn2 = new Connection(howToGetStronk,howToMakeFood,new List<ConnectionType>{ConnectionType.Author, ConnectionType.Other}, "Jeg har sat dem sammen fordi jeg godt kan lide dem.");
    public static Connection testconn3 = new Connection(hihihaha,howToMakeFood,new List<ConnectionType>{ConnectionType.Reference, ConnectionType.Other}, "Cool papers, that I like.");
}
