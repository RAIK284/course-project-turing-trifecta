import Team from "./Team";

export type OpposingGhostGuess = {
  id: string;
  gameSessionId: string;
  gameRoundId: string;
  isLeft: boolean;
  userId: string;
  team: Team;
};
