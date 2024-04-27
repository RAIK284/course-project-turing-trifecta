import { useState } from "react";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import { GamePageProps } from "../../pages/GamePage";
import { useStore } from "../../stores/store";
import { getRoundDirections } from "../../models/GameRound";

const GhostGuess: React.FC<GamePageProps> = ({ game, round, user }) => {
  const { gameSessionStore } = useStore();
  const { ghostGuess, callingEndpoint } = gameSessionStore;
  const [selection, setSelection] = useState<number>(-1);

  const handleSendGuess = () => {
    ghostGuess(game.id, selection);
  };

  const directions = getRoundDirections(user.id, round);

  return (
    <GameBoard
      game={game}
      round={round}
      user={user}
      spinner={
        <Spinner
          clickOption={directions.canDoAction ? "select" : "none"}
          onTargetSelect={setSelection}
          ghostGuesses={round.ghostGuesses?.map((gg) => gg.targetOffset)}
        />
      }
      directions={directions}
    >
      {directions.canDoAction && (
        <button
          disabled={selection === -1 || callingEndpoint}
          className="uppercase rounded bg-theme-blue w-fit px-5 py-1 font-semibold"
          onClick={handleSendGuess}
          type="button"
        >
          Send Selection
        </button>
      )}
    </GameBoard>
  );
};

export default GhostGuess;
