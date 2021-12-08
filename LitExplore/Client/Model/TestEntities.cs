namespace LitExplore.Client.Model;

public static class TestEntities {
    public static PaperDTO paperDTO = new PaperDTO(1,"document",new List<string>(){"author"},"title", 1,1,1, new List<string>(){"tag"});
    public static ConnectionDTO connectionDTO = new ConnectionDTO(1,"",1,1,"","",new List<int>());
    public static TeamDTO teamDTO = new TeamDTO(1,"",1 ,"1", new List<int>(), new List<int>());
}
