import GameSession from "../../../models/GameSession";
import Team from "../../../models/Team";
import TeamSelector from "./TeamSelector";

type ChooseTeamsProps = {
  game: GameSession;
};

const ChooseTeams: React.FC<ChooseTeamsProps> = ({ game }) => {
  return (
    <div className="Choose Teams">
      <TeamSelector disabled={false} game={game} team={Team.ONE} />
      <TeamSelector disabled={false} game={game} team={Team.TWO} />
    </div>
  );
};

export default ChooseTeams;
