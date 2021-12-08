namespace LitExplore.Client.Model;

public static class CurrentUser {
    public static string? userOid { get; set; }
    public static TeamDTO[]? teams { get; set; }
    public static ConnectionDTO[]? connections { get; set; }
    public static bool initialised() => userOid != null && teams != null && connections != null;
    public static TeamDTO? selectedTeam { get; set; }
}
