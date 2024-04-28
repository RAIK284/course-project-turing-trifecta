import { FormEvent, useState } from "react";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";
import ArrowRightIcon from "../../assets/icons/ArrowRightIcon";
import { GamePageProps } from "../../pages/GamePage";
import { useStore } from "../../stores/store";
import { getRoundDirections } from "../../models/GameRound";

const maxClueLength = 75;

const PsychicGiveClue: React.FC<GamePageProps> = ({ game, round, user }) => {
  const { gameSessionStore } = useStore();
  const { giveClue, callingEndpoint } = gameSessionStore;
  const [clue, setClue] = useState<string>(round.clue);

  const handleClueSend = (e: FormEvent) => {
    e.preventDefault();
    giveClue(game.id, clue);
  };

  const directions = getRoundDirections(user.id, round);

  return (
    <GameBoard
      game={game}
      user={user}
      round={round}
      spinner={
        <Spinner
          targetOffset={round.targetOffset}
          clickOption={directions.canDoAction ? "cover" : "none"}
          ghostGuesses={round.ghostGuesses?.map((gg) => gg.targetOffset)}
          selectorSelection={round.selectorSelection?.targetOffset}
        />
      }
      directions={directions}
    >
      {directions.canDoAction && (
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
            disabled={clue.length > maxClueLength || callingEndpoint}
            className="bg-target-2 rounded-full p-1"
            type="submit"
          >
            <ArrowRightIcon className="h-6 w-6 text-white" />
          </button>
        </form>
      )}
    </GameBoard>
  );
};

export default PsychicGiveClue;
