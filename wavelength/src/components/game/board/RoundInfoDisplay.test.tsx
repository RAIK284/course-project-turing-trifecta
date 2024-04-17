import { render, screen } from "@testing-library/react";
import RoundInfoDisplay from "./RoundInfoDisplay";
import { GameRound } from "../../../models/GameRound";
import Team, { getTeamName } from "../../../models/Team";

describe("RoundInfoDisplay", () => {
  const testRound1: GameRound = {
    id: "1",
    teamTurn: Team.ONE,
  } as GameRound;

  test("renders without errors", async () => {
    render(<RoundInfoDisplay round={{ ...testRound1, teamTurn: Team.ONE }} />);

    const roundInfoDisplay = await screen.findByLabelText("RoundInfoDisplay");

    expect(roundInfoDisplay).toBeTruthy();
  });

  test("renders correct team's turn with team one", async () => {
    const teamTurn = Team.ONE;
    const otherTeam = Team.TWO;
    render(<RoundInfoDisplay round={{ ...testRound1, teamTurn }} />);

    const teamOneElement = await screen.findByText(getTeamName(teamTurn), {
      exact: false,
    });

    const teamTwoTurn = await screen.queryByText(getTeamName(otherTeam), {
      exact: false,
    });

    expect(teamOneElement).toBeTruthy();
    expect(teamTwoTurn).toBeFalsy();
  });

  test("renders correct team's turn with team two", async () => {
    const teamTurn = Team.TWO;
    const otherTeam = Team.ONE;
    render(<RoundInfoDisplay round={{ ...testRound1, teamTurn }} />);

    const teamOneElement = await screen.findByText(getTeamName(teamTurn), {
      exact: false,
    });

    const teamTwoTurn = await screen.queryByText(getTeamName(otherTeam), {
      exact: false,
    });

    expect(teamOneElement).toBeTruthy();
    expect(teamTwoTurn).toBeFalsy();
  });
});
