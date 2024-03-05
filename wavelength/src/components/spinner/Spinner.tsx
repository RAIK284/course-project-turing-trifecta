import { useRef } from "react";
import useTailwind from "../../hooks/useTailwind";

type SpinnerProps = {
  size?: number;
  targetOffset?: number;
};

const targetDegreeWidth = 8;

const Spinner: React.FC<SpinnerProps> = ({ size = 500, targetOffset = 90 }) => {
  const tailwind = useTailwind();
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const ratio = window.devicePixelRatio;

  const canvas = canvasRef.current;
  const context = canvas?.getContext("2d");
  const halfSize = size / 2;

  const getRadians = (degrees: number) => (Math.PI / 180) * degrees;

  const clearCanvas = (ctx: CanvasRenderingContext2D) => {
    ctx?.clearRect(0, 0, size, size);
  };

  const drawBackground = (ctx: CanvasRenderingContext2D) => {
    const radius = halfSize;
    const offsetX = halfSize;
    const offsetY = halfSize;
    ctx.beginPath();
    ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, Math.PI, true);
    ctx.fillStyle = tailwind.theme.colors["scoreboard-blue"];
    ctx.closePath();
    ctx.fill();
  };

  const drawWhiteBoard = (ctx: CanvasRenderingContext2D) => {
    const radius = halfSize - 20;
    const offsetX = halfSize;
    const offsetY = halfSize;
    ctx.beginPath();
    ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, Math.PI, true);
    ctx.fillStyle = tailwind.theme.colors["white"];
    ctx.closePath();
    ctx.fill();
  };

  const drawSingleTarget = (
    ctx: CanvasRenderingContext2D,
    color: string,
    offsetDegrees: number
  ) => {
    const radius = halfSize - 20;
    ctx.beginPath();
    ctx.moveTo(halfSize, halfSize);
    ctx.lineTo(size, size - 20);
    ctx.moveTo(halfSize, halfSize);
    ctx.arc(
      halfSize,
      halfSize,
      radius,
      Math.PI + getRadians(180 - offsetDegrees - targetDegreeWidth / 2),
      Math.PI + getRadians(180 - offsetDegrees + targetDegreeWidth / 2)
    );
    ctx.closePath();
    ctx.fillStyle = color;
    ctx.fill();
  };

  const drawMiddleCircle = (ctx: CanvasRenderingContext2D) => {
    ctx.beginPath();
    ctx.ellipse(halfSize, halfSize, 50, 50, 0, 0, 2 * Math.PI);
    ctx.fillStyle = tailwind.theme.colors["center-red"];
    ctx.closePath();
    ctx.fill();
  };

  if (context) {
    clearCanvas(context);
    drawBackground(context);
    drawWhiteBoard(context);
    drawSingleTarget(context, tailwind.theme.colors["target-4"], targetOffset);
    drawSingleTarget(
      context,
      tailwind.theme.colors["target-3"],
      targetOffset + targetDegreeWidth
    );
    drawSingleTarget(
      context,
      tailwind.theme.colors["target-2"],
      targetOffset + 2 * targetDegreeWidth
    );
    drawSingleTarget(
      context,
      tailwind.theme.colors["target-3"],
      targetOffset - targetDegreeWidth
    );
    drawSingleTarget(
      context,
      tailwind.theme.colors["target-2"],
      targetOffset - 2 * targetDegreeWidth
    );
    drawMiddleCircle(context);
    context.scale(ratio, ratio);
  }

  return (
    <canvas
      width={size * ratio}
      height={halfSize * ratio}
      style={{
        width: `${size}px`,
        height: `${halfSize}px`,
      }}
      ref={canvasRef}
    ></canvas>
  );
};

export default Spinner;
