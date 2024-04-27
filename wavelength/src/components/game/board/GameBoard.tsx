import { PropsWithChildren } from "react";
import { GameRound, RoundDirection } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";
import ClueDisplay from "./ClueDisplay";
import RoundInfoDisplay from "./RoundInfoDisplay";
import User from "../../../models/User";
import IsLeftCount from "./IsLeftCount";
import { useStore } from "../../../stores/store";

type GameBoardProps = {
  game: GameSession;
  round: GameRound;
  user: User;
  spinner: React.ReactNode;
  directions: RoundDirection;
};

const GameBoard: React.FC<PropsWithChildren<GameBoardProps>> = ({
  game,
  round,
  children,
  spinner,
  user,
  directions,
}) => {
  const { gameSessionStore } = useStore();
  const { start } = gameSessionStore;
  const handleStartNextRoundClick = () => {
    start(game.id);
  };
  return (
    <div aria-label="GameBoard">
      <RoundInfoDisplay round={round} user={user} />
      {spinner}
      <div className="bg-scoreboard-blue flex flex-col pt-5 gap-2 justify-center items-center">
        {round.clue && <ClueDisplay clue={round.clue} />}
        {round.opposingGhostGuesses &&
          round.opposingGhostGuesses.length > 0 && (
            <div className="flex gap-2 w-1/2">
              <IsLeftCount round={round} isLeft={true} />
              <IsLeftCount round={round} isLeft={false} />
            </div>
          )}
        <div className="text-center text-xl">{directions.message}</div>
        {directions.canStartNextRound && game.ownerId === user.id && (
          <button
            onClick={handleStartNextRoundClick}
            className="bg-target-3 font-bold p-2 rounded"
          >
            Start Next Round
          </button>
        )}
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
