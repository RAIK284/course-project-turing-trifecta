enum TeamRole {
  NONE = 0,
  PSYCHIC = 1,
  SELECTOR = 2,
  GHOST = 3,
}

export function getRoleDirectionsIfUserIsRole(
  role: TeamRole,
  isOnOpposingTeam: boolean
): string {
  switch (role) {
    case TeamRole.GHOST:
      return isOnOpposingTeam ? "Choose left or right" : "Select your guess";
    case TeamRole.SELECTOR:
      return isOnOpposingTeam ? "Choose left or right" : "Select your guess";
    case TeamRole.PSYCHIC:
      return "Create a clue";
  }
  return "";
}

export function getRoleDirectionsIfUserNotRole(
  role: TeamRole,
  isOnOpposingTeam: boolean
): string {
  switch (role) {
    case TeamRole.GHOST:
      return isOnOpposingTeam
        ? "Awaiting opposing team ghosts..."
        : "Awaiting your team's ghosts...";
    case TeamRole.SELECTOR:
      return isOnOpposingTeam
        ? "Awaiting opposing team selector..."
        : "Awaiting your selector...";
    case TeamRole.PSYCHIC:
      return isOnOpposingTeam
        ? "Awaiting opposing team's psychic..."
        : "Awaiting your psychic...";
  }
  return "";
}

export default TeamRole;
