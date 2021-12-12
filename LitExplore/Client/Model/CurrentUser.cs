namespace LitExplore.Client.Model;

public static class CurrentUser {
    public static string? userOid { get; set; }
    public static List<TeamDTO> teams { get; set; } = new List<TeamDTO>();
    public static List<ConnectionDTO> connections { get; set; } = new List<ConnectionDTO>();
    public static bool initialised() => userOid != null && teams != null && connections != null;
    public static TeamDTO? selectedTeam { get; set; }
}
