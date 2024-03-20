import { useMemo } from "react";
import GameSession from "../../../models/GameSession";
import Team from "../../../models/Team";
import { useStore } from "../../../stores/store";
import { useStoreValue } from "../../../stores/storeValue";
import { cn } from "../../../utils/utils";

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
      className={cn(
        "TeamSelector flex flex-col items-center w-80 h-96 rounded-md",
        {
          "bg-team-1-score-holder": team === Team.ONE,
          "bg-team-2-score-holder": team === Team.TWO,
        }
      )}
    >
      <span className="text-black font-bold text-3xl">
        Team {team === Team.ONE ? "One" : "Two"}
      </span>
      {members.map((member) => (
        <span key={member.id}>{member.id}</span>
      ))}
    </button>
  );
};

export default TeamSelector;
