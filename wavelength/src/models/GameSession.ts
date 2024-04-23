import { GameRound } from "./GameRound";
import GameSessionMember from "./GameSessionMember";

type GameSession = {
  id: string;
  startTime?: Date;
  endTime?: Date;
  joinCode: string;
  ownerId: string;
  gameRound: number;
  members: GameSessionMember[];
  rounds: GameRound[];
};

export default GameSession;

export function getCurrentGameRound(gameSession: GameSession | undefined) {
  if (
    gameSession &&
    gameSession.gameRound >= 0 &&
    gameSession.rounds.length > gameSession.gameRound
  )
    return gameSession.rounds[gameSession?.gameRound];

  return undefined;
}
