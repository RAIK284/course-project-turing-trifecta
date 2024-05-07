import { PropsWithChildren, useEffect, useState } from "react";
import { GameRound, RoundDirection } from "../../../models/GameRound";
import GameSession from "../../../models/GameSession";
import SpectrumCardComponent from "./RoundSpectrumCards";
import ClueDisplay from "./ClueDisplay";
import RoundInfoDisplay from "./RoundInfoDisplay";
import User from "../../../models/User";
import IsLeftCount from "./IsLeftCount";
import { useStore } from "../../../stores/store";
import TeamScoreSlots from "./TeamScoreSlots";
import Team from "../../../models/Team";

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
  const [teamOneScore, setTeamOneScore] = useState(0);
  const [teamTwoScore, setTeamTwoScore] = useState(0);
  const { gameSessionStore } = useStore();
  const { start } = gameSessionStore;
  const handleStartNextRoundClick = () => {
    start(game.id);
  };

  useEffect(() => {
    if (game.scores && game.scores.winningTeam !== undefined) {
      if (game.scores.winningTeam === Team.ONE) {
        setTeamOneScore(game.scores.winningScore);
        setTeamTwoScore(game.scores.losingScore);
      } else {
        setTeamOneScore(game.scores.losingScore);
        setTeamTwoScore(game.scores.winningScore);
      }
    }
  }, [game.scores]);
  

  return (
    <div aria-label="GameBoard">
      <div className="GamePage h-screen flex items-center justify-center">
      <div className="grid grid-cols-5 bg-scoreboard-dark-blue rounded-3xl">
        <div className="col-span-1 flex flex-col items-center justify-center">
          <p className="text-xl">Team One</p>
          <TeamScoreSlots score={teamOneScore} teamOne={true}/>
        </div>
        <div className="col-span-3 flex items-center justify-center h-full">
          <div>
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
      </div>
      <div className="col-span-1 flex flex-col items-center justify-center">
        <p className="text-xl">Team Two</p>
          <TeamScoreSlots score={teamTwoScore} teamOne={false}/>
        </div>
      </div>
    </div>
  </div>
  );
};

export default GameBoard;
