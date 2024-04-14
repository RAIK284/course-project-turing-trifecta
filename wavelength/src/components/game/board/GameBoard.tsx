import { PropsWithChildren } from "react";
import { GameRound } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";

type GameBoardProps = {
  game: GameSession;
  round: GameRound;
  spinner: React.ReactNode;
};

const GameBoard: React.FC<PropsWithChildren<GameBoardProps>> = ({
  round,
  children,
  spinner,
}) => {
  return (
    <div>
      {spinner}
      <div className="bg-scoreboard-blue flex flex-col">
        {children && children}
        <SpectrumCardComponent spectrumCard={round.spectrumCard} />
      </div>
    </div>
  );
};

export default GameBoard;
