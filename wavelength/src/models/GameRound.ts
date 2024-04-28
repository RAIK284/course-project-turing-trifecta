import { GhostGuess } from "./GhostGuess";
import { OpposingGhostGuess } from "./OpposingGhostGuess";
import { OpposingSelectorSelection } from "./OpposingSelectorSelection";
import { RoundRole } from "./RoundRole";
import { SelectorSelection } from "./SelectorSelection";
import { SpectrumCard } from "./SpectrumCard";
import Team, { getOtherTeam } from "./Team";
import TeamRole, {
  getRoleDirectionsIfUserIsRole,
  getRoleDirectionsIfUserNotRole,
} from "./TeamRole";

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

export type RoundDirection = {
  canDoAction: boolean;
  message: string;
  canStartNextRound: boolean;
};

export function getRoundDirections(
  userId: string,
  round: GameRound
): RoundDirection {
  const roundRole = getRoundRoleForUser(userId, round);

  const direct = (
    canDoAction: boolean,
    message: string,
    canStartNextRound?: boolean
  ): RoundDirection => {
    return {
      canDoAction,
      message,
      canStartNextRound: !!canStartNextRound,
    };
  };

  if (!roundRole) return direct(false, "");

  let currentTurnRole: TeamRole | undefined;
  let teamToPlay: Team = round.teamTurn;

  const numGhostsOnTeamTurn =
    round.roundRoles
      ?.filter((rr) => rr.role === TeamRole.GHOST)
      .filter((rr) => rr.team === round.teamTurn).length ?? 0;
  const numGhostsOnOpposingTeam =
    round.roundRoles
      ?.filter((rr) => rr.role === TeamRole.GHOST)
      .filter((rr) => rr.team !== round.teamTurn).length ?? 0;

  if (!round.clue) {
    currentTurnRole = TeamRole.PSYCHIC;
  } else if (
    numGhostsOnTeamTurn > 0 &&
    (!round.ghostGuesses ||
      (round.ghostGuesses.length !== numGhostsOnTeamTurn &&
        round.ghostGuesses.every((gg) => gg.userId !== userId)))
  ) {
    currentTurnRole = TeamRole.GHOST;
  } else if (!round.selectorSelection) {
    currentTurnRole = TeamRole.SELECTOR;
  } else if (
    numGhostsOnOpposingTeam > 0 &&
    (!round.opposingGhostGuesses ||
      (round.opposingGhostGuesses.length !== numGhostsOnOpposingTeam &&
        round.opposingGhostGuesses.every((gg) => gg.userId !== userId)))
  ) {
    currentTurnRole = TeamRole.GHOST;
    teamToPlay = getOtherTeam(round.teamTurn);
  } else if (!round.opposingTeamSelection) {
    currentTurnRole = TeamRole.SELECTOR;
    teamToPlay = getOtherTeam(round.teamTurn);
  }

  if (!currentTurnRole) return direct(false, "Round Over!", true);

  if (teamToPlay !== roundRole.team || currentTurnRole !== roundRole.role) {
    return direct(
      false,
      getRoleDirectionsIfUserNotRole(
        currentTurnRole,
        teamToPlay !== roundRole.team
      )
    );
  } else {
    return direct(
      true,
      getRoleDirectionsIfUserIsRole(
        roundRole.role,
        roundRole.team !== round.teamTurn
      )
    );
  }
}
