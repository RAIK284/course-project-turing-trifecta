import Team from "./Team";
import User from "./User";

type GameSessionMember = {
  id: string;
  gameSessionId: string;
  userId: string;
  user: User;
  team: Team;
};

export default GameSessionMember;
