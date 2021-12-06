namespace LitExplore.Client;

public static class Model {
    public static UserDTO? currentUser { get; set; }
    public static IEnumerable<TeamDTO>? currentUserTeams { get; set; }
    public static IEnumerable<ConnectionDTO>? currentUserConnections { get; set; }

    public static IEnumerable<PaperDTO> allPapers { get; set; } = Enumerable.Empty<PaperDTO>();
    public static IEnumerable<ConnectionDTO> publicConnections { get; set; } = Enumerable.Empty<ConnectionDTO>();
}
