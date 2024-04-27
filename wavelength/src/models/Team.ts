enum Team {
  ZERO = 0,
  ONE = 1,
  TWO = 2,
}

export function getTeamName(team: Team) {
  switch (team) {
    case Team.ZERO:
      return "None";
    case Team.ONE:
      return "One";
    case Team.TWO:
      return "Two";
  }
}

export function getOtherTeam(team: Team) {
  return team === Team.ONE ? Team.TWO : Team.ONE;
}

export default Team;
