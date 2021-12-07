namespace LitExplore.Client.Model;

public static class CurrentUser {
    public static UserDTO? user { get; set; }
    public static IEnumerable<TeamDTO>? teams { get; set; }
    public static IEnumerable<ConnectionDTO>? connections { get; set; }

    public static TeamDTO? selectedTeam { get; set; }
}
