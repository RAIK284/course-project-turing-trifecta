import { GameRound } from "../../../models/GameRound";
import { getNumOpposingGhostGuessesForSide } from "../../../models/OpposingGhostGuess";

type IsLeftCount = {
  round: GameRound;
  isLeft: boolean;
};

const IsLeftCount: React.FC<IsLeftCount> = ({ round, isLeft }) => {
  return (
    <div
      aria-label="ClueDisplay"
      className="items-center justify-center rounded bg-scoreboard-dark-blue w-1/2 flex flex-col p-2"
    >
      <span className="uppercase tracking-wider font-semibold text-xs text-center">
        {isLeft ? "Left Votes" : "Right Votes"}
      </span>
      <span className="text-sm">
        {getNumOpposingGhostGuessesForSide(round, isLeft)}
      </span>
    </div>
  );
};

export default IsLeftCount;
