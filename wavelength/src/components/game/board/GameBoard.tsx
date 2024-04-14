import { PropsWithChildren } from "react";
import { GameRound } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";

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
    <div>
      {spinner}
      <div className="bg-scoreboard-blue flex flex-col pt-5">
        <div className="text-center text-xl">{directions}</div>
        {children && children}
        <SpectrumCardComponent spectrumCard={round.spectrumCard} />
      </div>
    </div>
  );
};

export default GameBoard;
