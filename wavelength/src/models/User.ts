import GameSession from "./GameSession";

type User = {
  id: string;
  userName: string;
  email: string;
  avatarID: string;
  token?: string;
  activeGameSessionID?: string;
  activeGameSession?: GameSession;
};

export default User;
