import { PropsWithChildren } from "react";
import { GameRound, getRoundDirections } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";
import ClueDisplay from "./ClueDisplay";
import RoundInfoDisplay from "./RoundInfoDisplay";
import User from "../../../models/User";

type GameBoardProps = {
  game: GameSession;
  round: GameRound;
  user: User;
  spinner: React.ReactNode;
};

const GameBoard: React.FC<PropsWithChildren<GameBoardProps>> = ({
  round,
  children,
  spinner,
  user,
}) => {
  return (
    <div aria-label="GameBoard">
      <RoundInfoDisplay round={round} user={user} />
      {spinner}
      <div className="bg-scoreboard-blue flex flex-col pt-5 gap-3 justify-center items-center">
        {round.clue && <ClueDisplay clue={round.clue} />}
        <div className="text-center text-xl">
          {getRoundDirections(user.id, round)}
        </div>
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
