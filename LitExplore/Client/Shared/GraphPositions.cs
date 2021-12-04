using System.Net.Http.Json;
namespace LitExplore.Client;

public static class GraphPositions {
    public static Dictionary<int, (int, int)> paperToPositions { get; } = new Dictionary<int,(int,int)>();
    public static List<(int,int)> positions = new List<(int,int)> {(12,15),(27,24),(45,23),(43,48),(55,60),(73,35),(62,15),(15,75),(38,83),(28,58),(70,73),(85,51)};
    public static PaperDTO[] papers;
    private static bool initialised = false;
    public async static void initialise(HttpClient Http) 
    {
        papers = await Http.GetFromJsonAsync<PaperDTO[]>("api/PaperManager");

        for (int i = 0; i<12; i++)
        {
            paperToPositions[papers[i].Id] = positions[i];
        }


        initialised = true;
    }
}