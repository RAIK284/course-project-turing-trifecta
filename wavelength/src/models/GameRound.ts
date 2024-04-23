import { GhostGuess } from "./GhostGuess";
import { OpposingGhostGuess } from "./OpposingGhostGuess";
import { OpposingSelectorSelection } from "./OpposingSelectorSelection";
import { RoundRole } from "./RoundRole";
import { SelectorSelection } from "./SelectorSelection";
import { SpectrumCard } from "./SpectrumCard";
import Team from "./Team";
import TeamRole from "./TeamRole";

export type GameRound = {
  id: string;
  gameSessionId: string;
  teamTurn: Team.ONE | Team.TWO;
  spectrumCardId: string;
  spectrumCard: SpectrumCard;
  clue: string;
  targetOffset: number;
  ghostGuesses?: GhostGuess[];
  roundRoles?: RoundRole[];
  selectorSelection?: SelectorSelection;
  opposingGhostGuesses?: OpposingGhostGuess[];
  opposingTeamSelection?: OpposingSelectorSelection;
};

export function getRoundRoleForUser(
  userId: string,
  round: GameRound
): RoundRole | undefined {
  const { roundRoles } = round;

  return roundRoles?.find((rr) => rr.userId === userId);
}

export function getRoundDirections(userId: string, round: GameRound) {
  const roundRole = getRoundRoleForUser(userId, round);

  if (!roundRole) return "";

  const { role, team } = roundRole;

  if (team !== round.teamTurn) {
    return "Awaiting opposing team responses...";
  }

  switch (role) {
    case TeamRole.PSYCHIC:
      const canPsychicGiveClue = !round.clue;

      return canPsychicGiveClue ? "Enter your clue:" : "Clue has been sent!";
    case TeamRole.GHOST:
      const canGhostGuess = !!round.clue;

      return canGhostGuess
        ? "Select your guess"
        : "Awaiting psychic to give their clue...";
    case TeamRole.SELECTOR:
      const numGhosts = round.roundRoles?.filter(
        (rr) => rr.team === round.teamTurn && rr.role === TeamRole.GHOST
      ).length;
      const isAwaitingPsychic = !!round.clue;

      if (isAwaitingPsychic) return "Awaiting psychic to give their clue...";

      const canSelectorSelect =
        round.ghostGuesses && round.ghostGuesses.length === numGhosts;

      return canSelectorSelect
        ? "Select your guess"
        : "Awaiting ghosts to select their guesses...";
  }

  return "";
}
