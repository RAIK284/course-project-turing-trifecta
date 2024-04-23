import { FormEvent, useState } from "react";
import { GameRound } from "../../models/GameRound";
import GameSession from "../../models/GameSession";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import ArrowRightIcon from "../../assets/icons/ArrowRightIcon";
import { GamePageProps } from "../../pages/GamePage";

const maxClueLength = 75;

const PsychicGiveClue: React.FC<GamePageProps> = ({ game, round }) => {
  const [clue, setClue] = useState<string>(round.clue);

  const handleClueSend = (e: FormEvent) => {
    e.preventDefault();
    console.log(clue);
  };

  return (
    <GameBoard
      directions="Enter Your Clue:"
      game={game}
      round={round}
      spinner={
        <Spinner targetOffset={round.targetOffset} clickOption="cover" />
      }
    >
      <form
        onSubmit={handleClueSend}
        className="p-5 text-black w-full flex items-center justify-center gap-2 rounded"
      >
        <input
          className="p-1 rounded"
          value={clue}
          onChange={(e) => setClue(e.target.value)}
        />
        <button
          disabled={round.clue === clue || clue.length > maxClueLength}
          className="bg-target-2 rounded-full p-1"
          type="submit"
        >
          <ArrowRightIcon className="h-6 w-6 text-white" />
        </button>
      </form>
    </GameBoard>
  );
};

export default PsychicGiveClue;
