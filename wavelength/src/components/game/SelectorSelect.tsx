import { useState } from "react";
import { GameRound } from "../../models/GameRound";
import GameSession from "../../models/GameSession";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";

type SelectorSelectProps = {
  game: GameSession;
  round: GameRound;
};

const SelectorSelect: React.FC<SelectorSelectProps> = ({ game, round }) => {
  const [selection, setSelection] = useState<number>(-1);

  const handleSendSelection = () => {
    console.log(selection);
  };

  return (
    <GameBoard
      game={game}
      round={round}
      directions="Select Your Guess!"
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
