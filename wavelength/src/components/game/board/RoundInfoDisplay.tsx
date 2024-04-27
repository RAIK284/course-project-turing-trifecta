import { GameRound, getRoundRoleForUser } from "../../../models/GameRound";
import User from "../../../models/User";

type RoundInfoDisplayProps = {
  round: GameRound;
  user: User;
};

const RoundInfoDisplay: React.FC<RoundInfoDisplayProps> = ({ round, user }) => (
  <div className="mb-5" aria-label="RoundInfoDisplay">
    <header className="text-2xl text-center tracking-wide font-bold">
      {round.teamTurn === getRoundRoleForUser(user.id, round)?.team
        ? "Your "
        : "Other "}{" "}
      Team's Turn
    </header>
  </div>
);

export default RoundInfoDisplay;
