import GameSession from "./GameSession";

type User = {
  id: string;
  userName: string;
  email: string;
  avatarId: string;
  token?: string;
  activeGameSessionId?: string;
  activeGameSession?: GameSession;
};

export default User;
