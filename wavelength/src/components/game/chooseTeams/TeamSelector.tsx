import { useMemo } from "react";
import GameSession from "../../../models/GameSession";
import Team from "../../../models/Team";
import { useStore } from "../../../stores/store";
import { useStoreValue } from "../../../stores/storeValue";

type TeamSelectorProps = {
  game: GameSession;
  disabled: boolean;
  team: Team;
};

const TeamSelector: React.FC<TeamSelectorProps> = ({
  game,
  disabled,
  team,
}) => {
  const { gameSessionStore, userStore } = useStore();
  const { switchTeams } = gameSessionStore;
  const [user] = useStoreValue(userStore.userStoreValue);

  const members = useMemo(
    () => game.members.filter((m) => m.team === team),
    [game, team]
  );

  const handleTeamSelect = () => {
    if (user && game) {
      switchTeams(user.id, game.id, team);
    }
  };

  return (
    <button
      disabled={disabled}
      onClick={handleTeamSelect}
      className={`TeamSelector flex flex-column bg-team-${team.toString()}-score-holder w-80 h-80`}
    >
      {members.map((member) => (
        <span key={member.id}>{member.id}</span>
      ))}
    </button>
  );
};

export default TeamSelector;
