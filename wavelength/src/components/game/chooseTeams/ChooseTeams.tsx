import { observer } from "mobx-react-lite";
import GameSession from "../../../models/GameSession";
import Team from "../../../models/Team";
import TeamSelector from "./TeamSelector";
import { useStore } from "../../../stores/store";
import { useStoreValue } from "../../../stores/storeValue";

type ChooseTeamsProps = {
  game: GameSession;
};

const ChooseTeams: React.FC<ChooseTeamsProps> = observer(({ game }) => {
  const { userStore } = useStore();
  const [user] = useStoreValue(userStore.userStoreValue);
  const teamlessMembers = game.members.filter((gm) => gm.team === Team.ZERO);
  const teamOneMembers = game.members.filter((gm) => gm.team === Team.ONE);
  const teamTwoMembers = game.members.filter((gm) => gm.team === Team.TWO);
  const canStartGame = game.members.length >= 4;

  const gameMemberForUser = game.members.find((gm) => gm.userId === user?.id);

  // This condition should always be false
  if (!gameMemberForUser) return <></>;

  const usersTeam = gameMemberForUser.team;
  const canSelectTeamOne =
    usersTeam !== Team.ONE && teamOneMembers.length - teamTwoMembers.length < 0;
  const canSelectTeamTwo =
    usersTeam !== Team.TWO && teamTwoMembers.length - teamOneMembers.length < 0;

  return (
    <div className="ChooseTeams flex flex-col items-center gap-10">
      <h1 className="text-3xl">Select Team</h1>
      {teamlessMembers.length > 0 && (
        <div className="bg-scoreboard-blue w-52 h-32 flex flex-col gap-1 items-center">
          <header>No Team</header>
          {game.members
            .filter((gm) => gm.team === Team.ZERO)
            .map((gm) => gm.user.userName)}
        </div>
      )}
      <div className="flex gap-10">
        <TeamSelector
          disabled={!canSelectTeamOne}
          game={game}
          team={Team.ONE}
        />
        <TeamSelector
          disabled={!canSelectTeamTwo}
          game={game}
          team={Team.TWO}
        />
      </div>
      {game.ownerId === user?.id && (
        <button
          disabled={!canStartGame}
          className="bg-center-red w-52 p-3 rounded-md"
        >
          Start Game
        </button>
      )}
    </div>
  );
});

export default ChooseTeams;
