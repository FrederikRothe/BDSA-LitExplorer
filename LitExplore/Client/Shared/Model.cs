namespace LitExplore.Client;

public static class Model {
    public static UserDTO? currentUser { get; set; }
    public static IEnumerable<TeamDTO>? currentUserTeams { get; set; }
    public static IEnumerable<ConnectionDTO>? currentUserConnections { get; set; }

    public static PaperDTO[]? allPapers { get; set; }
    public static ConnectionDTO[]? publicConnections { get; set; }
}
