import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import PsychicGiveClue from "./PsychicGiveClue";
import GameSession from "../../models/GameSession";
import { psychicGiveClueRound } from "../../api/mockData";
import userEvent, { UserEvent } from "@testing-library/user-event";

describe("PsychicGiveClue", () => {
  let user: UserEvent;

  beforeEach(() => {
    user = userEvent.setup();
  });

  test("renders without errors", async () => {
    render(
      <PsychicGiveClue game={{} as GameSession} round={psychicGiveClueRound} />
    );

    const component = await screen.findByLabelText("GameBoard");

    expect(component).toBeTruthy();
  });

  test("renders input correctly", async () => {
    render(
      <PsychicGiveClue game={{} as GameSession} round={psychicGiveClueRound} />
    );

    const component = await screen.findByLabelText("GameBoard");
    const input = component.querySelector("input") as HTMLInputElement;

    expect(input).toBeTruthy();
  });

  test("disables send button by default", async () => {
    render(
      <PsychicGiveClue game={{} as GameSession} round={psychicGiveClueRound} />
    );

    const component = await screen.findByLabelText("GameBoard");
    const button = component.querySelector("button") as HTMLButtonElement;

    expect(button).toBeDisabled();
  });

  test("enables send button after entering text", async () => {
    render(
      <PsychicGiveClue game={{} as GameSession} round={psychicGiveClueRound} />
    );

    const component = await screen.findByLabelText("GameBoard");
    const input = component.querySelector("input") as HTMLInputElement;
    const button = component.querySelector("button") as HTMLButtonElement;

    expect(button).toBeDisabled();

    await user.type(input, "test");

    expect(button).toBeEnabled();
  });

  test("disables send button after entering text that is the same as the original text", async () => {
    const initialText = "test";
    render(
      <PsychicGiveClue
        game={{} as GameSession}
        round={{ ...psychicGiveClueRound, clue: initialText }}
      />
    );

    const component = await screen.findByLabelText("GameBoard");
    const input = component.querySelector("input") as HTMLInputElement;
    const button = component.querySelector("button") as HTMLButtonElement;

    expect(button).toBeDisabled();

    await user.clear(input);
    await user.type(input, initialText);

    expect(button).toBeDisabled();
  });
});
