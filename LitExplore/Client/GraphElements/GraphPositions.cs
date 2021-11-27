namespace LitExplore.Client;

public static class GraphPositions {
    public static Dictionary<Paper, (int, int)> paperPositions = new Dictionary<Paper,(int,int)>();
    private static bool paperinitialised = false;

    public static Dictionary<Paper, (int,int)> GetPaperPositions() {
        if (!paperinitialised)
        {
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (12,15));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (24,27));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (45,23));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (43,48));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (55,60));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (73,35));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (62,15));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (15,75));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (38,83));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (28,58));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (70,73));
            paperPositions.Add(new Paper("yo", new List<string>(), "title", 2021,1,1,null), (85,51));
        }
        return paperPositions;
    }
}