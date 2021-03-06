namespace LitExplore.Client.Model;

public static class Graph 
{
    public static List<PaperDTO> allPapers { get; set; } = new List<PaperDTO>();
    
    public static List<ConnectionDTO> publicConnections { get; set; } = new List<ConnectionDTO>();
    
    public static Dictionary<int, (int, int)> paperToPositions { get; } = new Dictionary<int,(int,int)>();
    
    public static List<(int,int)> positions = new List<(int,int)> {(12,15),(27,24),(45,23),(43,48),(55,60),(73,35),(62,15),(15,75),(38,83),(28,58),(70,73),(85,51)};
    
    public static bool Initialised() => allPapers != null && publicConnections != null && _init;
    
    public static void Initialise() 
    {
        for (int i = 0; i<12; i++)
        { 
            paperToPositions[allPapers[i].Id] = positions[i];
        }
        _init = true;
    }
    private static bool _init = false;
}