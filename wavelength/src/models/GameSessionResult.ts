import Team from "./Team";

export type GameSessionResult = {
  id: string;
  gameSessionId: string;
  winningTeam: Team;
  losingTeam: Team;
  winningScore: number;
  losingScore: number;
};
