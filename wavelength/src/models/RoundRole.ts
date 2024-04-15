import Team from "./Team";
import TeamRole from "./TeamRole";

export type RoundRole = {
  id: string;
  userId: string;
  gameSessionId: string;
  gameRoundId: string;
  role: TeamRole;
  team: Team;
};
