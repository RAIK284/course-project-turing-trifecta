import { useEffect, useRef, useState } from "react";
import { drawSpinner } from "../../../utils/spinnerUtils";

type SpinnerProps = {
  targetOffset?: number;
  clickOption: "none" | "cover" | "select";
};

const Spinner: React.FC<SpinnerProps> = ({ targetOffset, clickOption }) => {
  const [covered, setCovered] = useState<boolean>(!!targetOffset);
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
        cover: covered,
        targetOffset,
        userMousePosition: {
          x: mousePosition.x,
          y: mousePosition.y,
        },
      });
    }
  }, [mousePosition, canvas, covered]);

  useEffect(() => {
    if (!canvas || clickOption !== "select") return;

    const handlePositionUpdate = (
      clientX: number,
      clientY: number,
      isTouch?: boolean
    ) => {
      if (!canvas || (!isTouch && selectorLocked)) return;

      const {
        x: canvasX,
        y: canvasY,
        width: canvasWidth,
        height: canvasHeight,
      } = canvas.getBoundingClientRect();
      const [x, y] = [clientX - canvasX, clientY - canvasY];

      if (x < 0 || y <= 0) return;

      // Scale the user's x and y to match the canvas' size
      setMousePosition({
        x: (x * size) / canvasWidth,
        y: (y * halfSize) / canvasHeight,
      });
    };

    // Support desktop (mouse) and mobile (touch)
    const mouseListener = (e: MouseEvent) =>
      handlePositionUpdate(e.clientX, e.clientY);
    const touchListener = (e: TouchEvent) =>
      handlePositionUpdate(e.touches[0].clientX, e.touches[0].clientY, true);

    if (!selectorLocked) {
      canvas.addEventListener("mousemove", mouseListener);
    }
    // If it is touch, we don't need to worry about locking the selector
    canvas.addEventListener("touchmove", touchListener);

    return () => {
      canvas.removeEventListener("mousemove", mouseListener);
      canvas.removeEventListener("touchmove", touchListener);
    };
  }, [selectorLocked, canvas]);

  const handleCanvasClick = () => {
    if (clickOption === "select") {
      setSelectorLocked(!selectorLocked);
    } else if (clickOption === "cover") {
      setCovered(!covered);
    }
  };

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
      onClick={handleCanvasClick}
    ></canvas>
  );
};

export default Spinner;
