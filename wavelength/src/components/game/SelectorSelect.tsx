import { GameRound } from "../../models/GameRound";
import GameSession from "../../models/GameSession";
import ClueDisplay from "./board/ClueDisplay";
import GameBoard from "./board/GameBoard";
import Spinner from "./board/Spinner";

type SelectorSelectProps = {
  game: GameSession;
  round: GameRound;
};

const SelectorSelect: React.FC<SelectorSelectProps> = ({ game, round }) => {
  return (
    <GameBoard
      game={game}
      round={round}
      directions="Select Your Guess"
      spinner={
        <Spinner clickOption="select" targetOffset={round.targetOffset} />
      }
    >
      <ClueDisplay clue={round.clue} />
    </GameBoard>
  );
};

export default SelectorSelect;
