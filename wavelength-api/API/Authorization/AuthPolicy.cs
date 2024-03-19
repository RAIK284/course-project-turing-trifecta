namespace API.Authorization;

public class AuthPolicy
{
    public static readonly string IsGameSessionMember = "IsGameSessionMember";
    public static readonly string IsNotGameSessionMember = "IsNotGameSessionMember";
    public static readonly string IsGhostOnTeamTurn = "IsGhostOnTeamTurn";
    public static readonly string IsSelectorOnTeamTurn = "IsSelectorOnTeamTurn";
    public static readonly string IsPsychicOnTeamTurn = "IsPsychicOnTeamTurn";
    public static readonly string HasNoRoleOnTeamTurn = "HasNoRoleOnTeamTurn";
    public static readonly string IsGhostOnOpposingTeam = "IsGhostOnOpposingTeam";
    public static readonly string IsSelectorOnOpposingTeam = "IsSelectorOnOpposingTeam";
    public static readonly string HasNoRoleOnOpposingTeam = "HasNoRoleOnOpposingTeam";
    public static readonly string IsGameSessionOwner = "IsGameSessionOwner";
}