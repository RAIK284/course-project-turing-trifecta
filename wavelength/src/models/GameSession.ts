import { GameRound } from "./GameRound";
import GameSessionMember from "./GameSessionMember";
import { GameSessionResult } from "./GameSessionResult";

type GameSession = {
  id: string;
  startTime?: Date;
  endTime?: Date;
  joinCode: string;
  ownerId: string;
  gameRound: number;
  members: GameSessionMember[];
  rounds: GameRound[];
  scores: GameSessionResult;
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
