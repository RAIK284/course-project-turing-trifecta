import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import GameBoard from "./GameBoard";
import Spinner from "./Spinner";
import GameSession from "../../../models/GameSession";
import { GameRound } from "../../../models/GameRound";
import { SpectrumCard } from "../../../models/SpectrumCard";

describe("GameBoard", () => {
  const directions = "Test This Component";
  const game = {} as GameSession;
  const spectrumCard = {
    id: "",
    leftName: "Risky",
    rightName: "Safe",
  } as SpectrumCard;
  const round = {
    spectrumCard: spectrumCard,
  } as GameRound;
  const spinner = <Spinner clickOption="none" />;

  test("renders without errors", async () => {
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={round}
        spinner={spinner}
      />
    );

    const gameBoard = await screen.findByLabelText("GameBoard");

    expect(gameBoard).toBeTruthy();
  });

  test("renders directions", async () => {
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={round}
        spinner={spinner}
      />
    );

    const screenDirections = await screen.findByText(directions);

    expect(screenDirections).toBeTruthy();
  });

  test("renders spectrum cards", async () => {
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={round}
        spinner={spinner}
      />
    );

    const spectrumCards = await screen.findByLabelText("RoundSpectrumCards");

    expect(spectrumCards).toBeTruthy();
  });

  test("renders children when present", async () => {
    const children = <div aria-label="Child"></div>;
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={round}
        spinner={spinner}
        children={children}
      />
    );

    const screenChildren = await screen.findByLabelText("Child");

    expect(screenChildren).toBeTruthy();
  });

  test("renders clue when present", async () => {
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={{ ...round, clue: "This Is Present" }}
        spinner={spinner}
      />
    );

    const clue = await screen.findByLabelText("ClueDisplay");

    expect(clue).toBeTruthy();
  });

  test("does not render clue when a clue is not present", async () => {
    render(
      <GameBoard
        directions={directions}
        game={game}
        round={{ ...round, clue: "" }}
        spinner={spinner}
      />
    );

    const clue = await screen.queryByLabelText("ClueDisplay");

    expect(clue).toBeFalsy();
  });
});
