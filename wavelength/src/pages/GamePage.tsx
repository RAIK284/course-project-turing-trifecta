import { observer } from "mobx-react-lite";
import GameSession from "../models/GameSession";
import { useStore } from "../stores/store";
import { useStoreValue } from "../stores/storeValue";
import ChooseTeams from "../components/game/chooseTeams/ChooseTeams";
import { Navigate } from "react-router-dom";
import { WavelengthPath } from "../routing/Routes";
import { ReactNode } from "react";
import { psychicGiveClueRound } from "../api/mockData";
import PsychicGiveClue from "../components/game/PsychicGiveClue";

enum GameStatus {
  CHOOSE_TEAMS,
  PSYCHIC_GIVE_CLUE,
  GHOST_GUESS,
  SELECTOR_SELECT,
  OPPOSING_GHOST_GUESS,
  OPPOSING_SELECTOR_SELECT,
  ROUND_END,
}

const getGameStatus = (game: GameSession): GameStatus => {
  if (game.id) return GameStatus.PSYCHIC_GIVE_CLUE;
  if (game.startTime === null) return GameStatus.CHOOSE_TEAMS;

  return GameStatus.CHOOSE_TEAMS;
};

const GamePage: React.FC = observer(() => {
  const { gameSessionStore } = useStore();
  const [game] = useStoreValue(gameSessionStore.gameSessionStoreValue);

  if (!game) {
    return <Navigate to={WavelengthPath.LANDING} />;
  }

  let element: ReactNode;

  switch (getGameStatus(game)) {
    case GameStatus.CHOOSE_TEAMS:
      element = <ChooseTeams game={game} />;
      break;
    case GameStatus.PSYCHIC_GIVE_CLUE:
      element = <PsychicGiveClue game={game} round={psychicGiveClueRound} />;
      break;
  }

  return (
    <div className="GamePage flex items-center justify-center h-full">
      {element && element}
    </div>
  );
});

export default GamePage;
