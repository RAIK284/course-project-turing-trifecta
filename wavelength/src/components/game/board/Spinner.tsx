import { useEffect, useRef, useState } from "react";
import {
  drawSpinner,
  getMousePositionDegrees,
} from "../../../utils/spinnerUtils";

type SpinnerProps = {
  targetOffset?: number;
  clickOption: "none" | "cover" | "select";
  onTargetSelect?: (targetOffset: number) => void;
  ghostGuesses?: number[];
  selectorSelection?: number;
  isLeftGuess?: boolean;
};

const size = 1000;

const Spinner: React.FC<SpinnerProps> = ({
  targetOffset,
  clickOption,
  onTargetSelect,
  ghostGuesses,
  selectorSelection,
  isLeftGuess,
}) => {
  const [covered, setCovered] = useState<boolean>(true);
  const [mousePosition, setMousePosition] = useState<{
    x: number;
    y: number;
  }>({
    x: -1,
    y: -1,
  });
  const [selectorLocked, setSelectorLocked] = useState<boolean>(false);
  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const halfSize = size / 2;

  useEffect(() => {
    setCovered(clickOption !== "cover" || !targetOffset || targetOffset === -1);
  }, [clickOption, targetOffset]);

  useEffect(() => {
    const showDefaultSelector = covered && clickOption === "select";
    setMousePosition({
      x: !showDefaultSelector ? -1 : size / 2,
      y: !showDefaultSelector ? -1 : 0,
    });
  }, [covered, clickOption]);

  useEffect(() => {
    const canvasContext = canvasRef.current?.getContext("2d");
    if (canvasContext) {
      drawSpinner(canvasContext, size, {
        cover: covered,
        targetOffset,
        userMousePosition: mousePosition,
        ghostGuesses,
        selectorSelection,
        isLeftGuess,
      });
    }
  }, [
    canvasRef,
    mousePosition,
    covered,
    isLeftGuess,
    ghostGuesses,
    targetOffset,
    selectorSelection,
  ]);

  useEffect(() => {
    const canvas = canvasRef.current;
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
  }, [selectorLocked, canvasRef, canvasRef.current, clickOption]);

  const handleCanvasClick = () => {
    if (clickOption === "select") {
      if (!selectorLocked && onTargetSelect) {
        onTargetSelect(
          getMousePositionDegrees(mousePosition.x, mousePosition.y, size)
        );
      }
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
        cursor: clickOption !== "none" ? "pointer" : undefined,
      }}
      ref={canvasRef}
      onClick={handleCanvasClick}
      aria-label="Spinner"
    ></canvas>
  );
};

export default Spinner;
