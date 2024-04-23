import { useState } from "react";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import { GamePageProps } from "../../pages/GamePage";

const SelectorSelect: React.FC<GamePageProps> = ({ game, round, user }) => {
  const [selection, setSelection] = useState<number>(-1);

  const handleSendSelection = () => {
    console.log(selection);
  };

  return (
    <GameBoard
      game={game}
      round={round}
      user={user}
      spinner={
        <Spinner
          clickOption="select"
          onTargetSelect={setSelection}
          ghostGuesses={round.ghostGuesses?.map((gg) => gg.targetOffset)}
        />
      }
    >
      <button
        disabled={selection === -1}
        className="uppercase rounded bg-theme-blue w-fit px-5 py-1 font-semibold"
        onClick={handleSendSelection}
        type="button"
      >
        Send Selection
      </button>
    </GameBoard>
  );
};

export default SelectorSelect;
