import { GameRound } from "./GameRound";
import Team from "./Team";

export type OpposingGhostGuess = {
  id: string;
  gameSessionId: string;
  gameRoundId: string;
  isLeft: boolean;
  userId: string;
  team: Team;
};

export function getNumOpposingGhostGuessesForSide(
  round: GameRound,
  isLeft: boolean
): number {
  return (
    round.opposingGhostGuesses?.filter((gg) => gg.isLeft === isLeft).length ?? 0
  );
}
