import { GameRound } from "../../../models/GameRound";
import { getTeamName } from "../../../models/Team";

type RoundInfoDisplayProps = {
  round: GameRound;
};

const RoundInfoDisplay: React.FC<RoundInfoDisplayProps> = ({ round }) => (
  <div className="mb-5" aria-label="RoundInfoDisplay">
    <header className="text-2xl text-center tracking-wide font-bold">
      Team {getTeamName(round.teamTurn)}'s Turn
    </header>
  </div>
);

export default RoundInfoDisplay;
