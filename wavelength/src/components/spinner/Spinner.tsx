import { useRef } from "react";
import useTailwind from "../../hooks/useTailwind";

type SpinnerProps = {
  size?: number;
};

const Spinner: React.FC<SpinnerProps> = ({ size = 500 }) => {
  const tailwind = useTailwind();
  const canvasRef = useRef<HTMLCanvasElement | null>(null);

  const canvas = canvasRef.current;
  const context = canvas?.getContext("2d");
  const halfSize = size / 2;

  const getRadians = (degrees: number) => {
    return (degrees * Math.PI) / 180;
  };

  const clearCanvas = (ctx: CanvasRenderingContext2D) => {
    ctx?.clearRect(0, 0, size, size);
  };

  const drawBackground = (ctx: CanvasRenderingContext2D) => {
    const radius = halfSize;
    const offsetX = halfSize;
    const offsetY = halfSize;
    ctx.beginPath();
    ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, 180);
    ctx.fillStyle = tailwind.theme.colors["scoreboard-blue"];
    ctx.closePath();
    ctx.fill();
  };

  const drawWhiteBoard = (ctx: CanvasRenderingContext2D) => {
    const radius = halfSize - 20;
    const offsetX = halfSize;
    const offsetY = halfSize;
    ctx.beginPath();
    ctx.ellipse(offsetX, offsetY, radius, radius, 0, 0, 180);
    ctx.fillStyle = tailwind.theme.colors["white"];
    ctx.closePath();
    ctx.fill();
  };

  const drawSingleTarget = (
    ctx: CanvasRenderingContext2D,
    color: string,
    offsetDegrees: number
  ) => {
    ctx.beginPath();
    ctx.moveTo(halfSize, halfSize);
    ctx.closePath();
    ctx.fillStyle = color;
    ctx.fill();
  };

  if (context) {
    clearCanvas(context);
    drawBackground(context);
    drawWhiteBoard(context);
    drawSingleTarget(context, tailwind.theme.colors["target-4"], 0);
  }

  return <canvas width={size} height={halfSize} ref={canvasRef}></canvas>;
};

export default Spinner;
