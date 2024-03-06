import { useEffect, useRef, useState } from "react";
import { drawSpinner } from "../../utils/spinnerUtils";

type SpinnerProps = {
  size?: number;
  targetOffset?: number;
};

const Spinner: React.FC<SpinnerProps> = ({ targetOffset = 20 }) => {
  const [mousePosition, setMousePosition] = useState<{
    x: number;
    y: number;
  }>({
    x: -1,
    y: -1,
  });
  const [selectorLocked, setSelectorLocked] = useState<boolean>(false);
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const canvas = canvasRef.current;
  const context = canvas?.getContext("2d");
  const size = 1000;
  const halfSize = size / 2;

  useEffect(() => {
    if (context) {
      drawSpinner(context, size, {
        cover: true,
        targetOffset,
        userMousePosition: {
          x: mousePosition.x,
          y: mousePosition.y,
        },
      });
    }
  }, [mousePosition, canvas]);

  useEffect(() => {
    if (!canvas) return;

    const listener = (ev: MouseEvent) => {
      if (!canvas || selectorLocked) return;

      const { x: canvasX, y: canvasY } = canvas.getBoundingClientRect();
      const [x, y] = [ev.clientX - canvasX, ev.clientY - canvasY];

      setMousePosition({ x: x * 2, y: y * 2 });
    };

    if (!selectorLocked) canvas.addEventListener("mousemove", listener);
    // else canvas.removeEventListener("mousemove", listener);

    console.log(selectorLocked);

    return () => canvas.removeEventListener("mousemove", listener);
  }, [selectorLocked]);

  return (
    <canvas
      width={size}
      height={halfSize}
      style={{
        width: `${size / 2}px`,
        maxWidth: `90vw`,
        maxHeight: `45vw`,
        height: `${halfSize / 2}px`,
      }}
      ref={canvasRef}
      onClick={() => setSelectorLocked(!selectorLocked)}
    ></canvas>
  );
};

export default Spinner;
