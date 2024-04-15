import Team from "./Team";

export type GhostGuess = {
  id: string;
  gameSessionId: string;
  gameRoundId: string;
  targetOffset: number;
  userId: string;
  team: Team;
};
