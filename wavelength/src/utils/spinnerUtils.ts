import { tailWindConfig } from "./utils";

export type SpinnerOptions = {
  cover?: boolean;
  targetOffset?: number;
  ghostGuesses?: number[]; // a list of targetOffsets for each ghost guess
  opposingTeamGhostGuesses?: number[];
  selectorSelection?: number;
  opposingTeamSelectorSelection?: number;
  userMousePosition?: {
    x: number;
    y: number;
  };
};

const targetDegreeWidth = 8;

const getRadians = (degrees: number) => (Math.PI / 180) * degrees;

const clearCanvas = (ctx: CanvasRenderingContext2D, size: number) => {
  ctx.beginPath();
  ctx.clearRect(0, 0, size, size);
  ctx.closePath();
  ctx.fill();
};

const drawOuterArc = (
  ctx: CanvasRenderingContext2D,
  color: string,
  size: number
) => {
  const halfSize = size / 2;
  const radius = halfSize;
  const offsetX = halfSize;
  const offsetY = halfSize;
  ctx.beginPath();
  ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, Math.PI, true);
  ctx.fillStyle = color;
  ctx.closePath();
  ctx.fill();
};

const drawInnerArc = (
  ctx: CanvasRenderingContext2D,
  color: string,
  size: number
) => {
  const halfSize = size / 2;
  const radius = halfSize - (20 / 500) * size;
  const offsetX = halfSize;
  const offsetY = halfSize;
  ctx.beginPath();
  ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, Math.PI, true);
  ctx.fillStyle = color;
  ctx.closePath();
  ctx.fill();
};

const drawSingleTarget = (
  ctx: CanvasRenderingContext2D,
  color: string,
  offsetDegrees: number,
  degreeWidth: number,
  size: number,
  displayNumber?: string
) => {
  const halfSize = size / 2;
  const radius = halfSize - (20 / 500) * size;
  ctx.beginPath();
  ctx.moveTo(halfSize, halfSize);
  ctx.lineTo(size, size - (20 / 500) * size);
  ctx.moveTo(halfSize, halfSize);
  ctx.arc(
    halfSize,
    halfSize,
    radius,
    Math.PI + getRadians(180 - offsetDegrees - degreeWidth / 2),
    Math.PI + getRadians(180 - offsetDegrees + degreeWidth / 2)
  );
  ctx.closePath();
  ctx.fillStyle = color;
  ctx.fill();

  if (displayNumber) {
    const y =
      Math.sin(getRadians(180 - offsetDegrees)) *
      (halfSize - (1 / 6) * halfSize);
    const x =
      Math.cos(getRadians(180 - offsetDegrees)) *
      (halfSize - (1 / 6) * halfSize);
    console.log(x, y);
    ctx.save();
    // when we rotate we will be pinching the
    // top-left hand corner with our thumb and finger
    ctx.translate(halfSize - x, halfSize - y);

    // now rotate the canvas anti-clockwise by 90 degrees
    // holding onto the translate point
    ctx.rotate(getRadians(offsetDegrees + 3));

    // specify the font and colour of the text
    ctx.font = "32px spartan";
    ctx.fillStyle = "black"; // red

    // set alignment of text at writing point (left-align)
    ctx.textAlign = "center";

    // write the text
    ctx.fillText(displayNumber, 0, 0);

    // now restore the canvas flipping it back to its original orientation
    ctx.restore();
  }
};

const drawMiddleCircle = (
  ctx: CanvasRenderingContext2D,
  color: string,
  size: number
) => {
  const halfSize = size / 2;
  ctx.beginPath();
  ctx.ellipse(halfSize, halfSize, size / 10, size / 10, 0, 0, 2 * Math.PI);
  ctx.fillStyle = color;
  ctx.closePath();
  ctx.fill();
};

export const drawSpinner = (
  ctx: CanvasRenderingContext2D,
  size: number,
  options: SpinnerOptions
) => {
  const halfSize = size / 2;
  const { colors } = tailWindConfig;
  ctx.setTransform(1, 0, 0, 1, 0, 0);
  clearCanvas(ctx, size);
  drawOuterArc(ctx, colors["scoreboard-blue"], size);

  if (options.cover) {
    drawInnerArc(ctx, colors["cover-blue"], size);
  } else {
    drawInnerArc(ctx, colors["white"], size);
  }

  if (!options.cover && options.targetOffset) {
    drawSingleTarget(
      ctx,
      colors["target-4"],
      options.targetOffset,
      targetDegreeWidth,
      size,
      "4"
    );
    drawSingleTarget(
      ctx,
      colors["target-3"],
      options.targetOffset + targetDegreeWidth,
      targetDegreeWidth,
      size,
      "3"
    );
    drawSingleTarget(
      ctx,
      colors["target-2"],
      options.targetOffset + 2 * targetDegreeWidth,
      targetDegreeWidth,
      size,
      "2"
    );
    drawSingleTarget(
      ctx,
      colors["target-3"],
      options.targetOffset - targetDegreeWidth,
      targetDegreeWidth,
      size,
      "3"
    );
    drawSingleTarget(
      ctx,
      colors["target-2"],
      options.targetOffset - 2 * targetDegreeWidth,
      targetDegreeWidth,
      size,
      "2"
    );
  }

  if (options.userMousePosition) {
    const { x, y } = options.userMousePosition;

    if (x != -1 && y != -1) {
      const oppositeOverAdjacent = (y - halfSize) / (halfSize - x);
      let angle = Math.atan(oppositeOverAdjacent);
      angle = angle < 0 ? angle + Math.PI : angle;
      drawSingleTarget(ctx, "blue", (angle * 180) / Math.PI, 3, size);
    }
  }

  drawMiddleCircle(ctx, colors["center-red"], size);
};
