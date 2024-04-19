import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import Spinner from "./Spinner";

test("renders without errors", async () => {
  render(<Spinner clickOption="none" />);

  const canvas = (await screen.findByLabelText("Spinner")) as HTMLCanvasElement;

  expect(canvas).toBeTruthy();
});
