using System.Net.Http.Json;
namespace LitExplore.Client.Model;

public static class Graph {
    public static PaperDTO[]? allPapers { get; set; }
    public static ConnectionDTO[]? publicConnections { get; set; }
    public static Dictionary<int, (int, int)> paperToPositions { get; } = new Dictionary<int,(int,int)>();
    public static List<(int,int)> positions = new List<(int,int)> {(12,15),(27,24),(45,23),(43,48),(55,60),(73,35),(62,15),(15,75),(38,83),(28,58),(70,73),(85,51)};
    public async static void initialise(HttpClient Http) 
    {
        for (int i = 0; i<12; i++)
        { 
            paperToPositions[allPapers[i].Id] = positions[i];
        }
    }
}