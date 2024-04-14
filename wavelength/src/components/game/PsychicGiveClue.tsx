import { useState } from "react";
import { GameRound } from "../../models/GameRound";
import GameSession from "../../models/GameSession";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import ArrowRightIcon from "../../assets/icons/ArrowRightIcon";

type PsychicGiveClueProps = {
  game: GameSession;
  round: GameRound;
};

const maxClueLength = 75;

const PsychicGiveClue: React.FC<PsychicGiveClueProps> = ({ game, round }) => {
  const [clue, setClue] = useState<string>(round.clue);

  return (
    <GameBoard
      game={game}
      round={round}
      spinner={
        <Spinner targetOffset={round.targetOffset} clickOption="cover" />
      }
    >
      <div className="p-5 text-black w-full flex items-center justify-center gap-2 rounded">
        <input
          className="p-1 rounded"
          value={clue}
          onChange={(e) => setClue(e.target.value)}
        />
        <button
          disabled={round.clue === clue || clue.length > maxClueLength}
          className="bg-target-2 rounded-full p-1"
        >
          <ArrowRightIcon className="h-6 w-6 text-white" />
        </button>
      </div>
    </GameBoard>
  );
};

export default PsychicGiveClue;
