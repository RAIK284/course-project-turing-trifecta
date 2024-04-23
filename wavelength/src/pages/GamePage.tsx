import { observer } from "mobx-react-lite";
import GameSession, { getCurrentGameRound } from "../models/GameSession";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import ChooseTeams from "../components/game/chooseTeams/ChooseTeams";
import { Navigate } from "react-router-dom";
import { WavelengthPath } from "../routing/Routes";
import { ReactNode } from "react";
import PsychicGiveClue from "../components/game/PsychicGiveClue";
import SelectorSelect from "../components/game/SelectorSelect";
import User from "../models/User";
import { GameRound, getRoundRoleForUser } from "../models/GameRound";
import TeamRole from "../models/TeamRole";

enum GameStatus {
  CHOOSE_TEAMS,
  PSYCHIC_GIVE_CLUE,
  GHOST_GUESS,
  SELECTOR_SELECT,
  OPPOSING_GHOST_GUESS,
  OPPOSING_SELECTOR_SELECT,
  ROUND_END,
}

export type GamePageProps = {
  game: GameSession;
  round: GameRound;
  user: User;
};

const getGameStatus = (user: User, game: GameSession): GameStatus => {
  const currentRound = getCurrentGameRound(game);

  if (currentRound) {
    const roundRole = getRoundRoleForUser(user.id, currentRound)?.role;

    switch (roundRole) {
      case TeamRole.PSYCHIC:
        return GameStatus.PSYCHIC_GIVE_CLUE;
      case TeamRole.SELECTOR:
        return GameStatus.SELECTOR_SELECT;
    }
  }

  return GameStatus.CHOOSE_TEAMS;
};

const GamePage: React.FC = observer(() => {
  const { gameSessionStore, userStore } = useStore();
  const [game] = useStoreValue(gameSessionStore.gameSessionStoreValue);
  const [user] = useStoreValue(userStore.userStoreValue);
  const currentRound = getCurrentGameRound(game);

  if (!game || !user) {
    return <Navigate to={WavelengthPath.LANDING} />;
  }

  if (game.startTime && !currentRound)
    return <Navigate to={WavelengthPath.LANDING} />;

  let element: ReactNode;

  switch (getGameStatus(user, game)) {
    case GameStatus.CHOOSE_TEAMS:
      element = <ChooseTeams game={game} />;
      break;
    case GameStatus.PSYCHIC_GIVE_CLUE:
      if (currentRound)
        element = (
          <PsychicGiveClue game={game} round={currentRound} user={user} />
        );
      break;
    case GameStatus.SELECTOR_SELECT:
      if (currentRound)
        element = (
          <SelectorSelect game={game} round={currentRound} user={user} />
        );
      break;
  }

  return (
    <div className="GamePage flex items-center justify-center h-full">
      {element && element}
    </div>
  );
});

export default GamePage;
