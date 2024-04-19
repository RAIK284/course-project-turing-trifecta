import { PropsWithChildren } from "react";
import { GameRound } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";
import ClueDisplay from "./ClueDisplay";
import RoundInfoDisplay from "./RoundInfoDisplay";

type GameBoardProps = {
  game: GameSession;
  round: GameRound;
  spinner: React.ReactNode;
  directions: string; // the directions for the round, like "Enter Psychic guess" or "Make selection"
};

const GameBoard: React.FC<PropsWithChildren<GameBoardProps>> = ({
  round,
  children,
  spinner,
  directions,
}) => {
  return (
    <div aria-label="GameBoard">
      <RoundInfoDisplay round={round} />
      {spinner}
      <div className="bg-scoreboard-blue flex flex-col pt-5 gap-3 justify-center items-center">
        {round.clue && <ClueDisplay clue={round.clue} />}
        <div className="text-center text-xl">{directions}</div>
        {children && (
          <div className="flex items-center justify-center flex-col gap-4">
            {children}
          </div>
        )}
        <SpectrumCardComponent spectrumCard={round.spectrumCard} />
      </div>
    </div>
  );
};

export default GameBoard;
