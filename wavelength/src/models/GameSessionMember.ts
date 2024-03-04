import Team from "./Team";

type GameSessionMember = {
  id: string;
  gameSessionID: string;
  userID: string;
  team: Team;
};

export default GameSessionMember;
