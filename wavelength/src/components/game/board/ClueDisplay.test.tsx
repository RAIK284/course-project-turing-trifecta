import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import ClueDisplay from "./ClueDisplay";

describe("ClueDisplay", () => {
  test("renders without errors", async () => {
    render(<ClueDisplay clue="" />);

    const clueDisplay = await screen.findByLabelText("ClueDisplay");

    expect(clueDisplay).toBeTruthy();
  });

  test("displays the clue properly with 'Spiderman'", async () => {
    const clueText = "Spiderman";
    render(<ClueDisplay clue={clueText} />);

    const clue = await screen.findByText(clueText);

    expect(clue).toBeTruthy();
  });

  test("displays the clue properly with 'Batman'", async () => {
    const clueText = "Batman";
    render(<ClueDisplay clue={clueText} />);

    const clue = await screen.findByText(clueText);

    expect(clue).toBeTruthy();
  });
});
