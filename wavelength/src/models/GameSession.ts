import GameSessionMember from "./GameSessionMember";

type GameSession = {
  id: string;
  startTime?: Date;
  endTime?: Date;
  joinCode: string;
  ownerID: string;
  gameRound: number;
  members: GameSessionMember[];
};

export default GameSession;
