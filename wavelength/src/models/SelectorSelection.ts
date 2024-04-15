import Team from "./Team";

export type SelectorSelection = {
  id: string;
  gameSessionId: string;
  gameRoundId: string;
  targetOffset: number;
  userId: string;
  team: Team;
};
