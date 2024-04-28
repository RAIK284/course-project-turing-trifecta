import { useState } from "react";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import { GamePageProps } from "../../pages/GamePage";
import { useStore } from "../../stores/store";
import { getRoundDirections } from "../../models/GameRound";
import { cn } from "../../utils/utils";

const OpposingSelectorSelect: React.FC<GamePageProps> = ({
  game,
  round,
  user,
}) => {
  const { gameSessionStore } = useStore();
  const { opposingSelectorSelect, callingEndpoint } = gameSessionStore;
  const [isLeft, setLeft] = useState<boolean | undefined>(
    round.opposingGhostGuesses?.find((gg) => gg.userId === user.id)?.isLeft
  );

  const handleSendGuess = () => {
    opposingSelectorSelect(game.id, !!isLeft);
  };

  const directions = getRoundDirections(user.id, round);

  return (
    <GameBoard
      game={game}
      round={round}
      user={user}
      spinner={
        <Spinner
          clickOption={"none"}
          ghostGuesses={round.ghostGuesses?.map((gg) => gg.targetOffset)}
          selectorSelection={round.selectorSelection?.targetOffset}
          isLeftGuess={isLeft}
        />
      }
      directions={directions}
    >
      {directions.canDoAction && (
        <>
          <div className="flex gap-3">
            <button
              className={cn(
                "uppercase rounded bg-theme-blue w-32 px-5 py-1 font-semibold",
                { "border-2 border-solid border-cover-blue": isLeft === true }
              )}
              onClick={() => setLeft(true)}
              type="button"
            >
              Left
            </button>
            <button
              className={cn(
                "uppercase rounded bg-theme-blue  w-32 px-5 py-1 font-semibold",
                { "border-2 border-solid border-cover-blue": isLeft === false }
              )}
              onClick={() => setLeft(false)}
              type="button"
            >
              Right
            </button>
          </div>
          <button
            disabled={isLeft === undefined || callingEndpoint}
            onClick={handleSendGuess}
            className="uppercase rounded bg-theme-blue w-fit px-5 py-1 font-semibold"
          >
            Send Selection
          </button>
        </>
      )}
    </GameBoard>
  );
};

export default OpposingSelectorSelect;
