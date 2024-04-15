import Team from "./Team";

export type OpposingSelectorSelection = {
  id: string;
  gameSessionId: string;
  gameRoundId: string;
  isLeft: boolean;
  userId: string;
  team: Team;
};
