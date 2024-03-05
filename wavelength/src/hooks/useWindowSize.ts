import { useEffect, useState } from "react";

export const mobileWidth = 768;

/**
 * Adapted from https://usehooks.com/useWindowSize/
 *
 * Custom hook that provides the width of the screen.
 * @param widthDelimiters A list of widths that should determine when the windowSize state is updated for this hook to reduce rerendering.
 * @returns the width and height of the screen as well as if the screen is in a mobile or desktop view.
 */
export default function useWindowSize(
  widthDelimiters: number[] = [mobileWidth]
) {
  const [windowSize, setWindowSize] = useState<{
    width: number;
    height: number;
  }>({
    width: window.innerWidth,
    height: window.innerHeight,
  });

  const doesNumberPassDelimiter = (
    previousNumber: number,
    number: number,
    delimiters: number[]
  ) =>
    delimiters.some((delimiter) => {
      if (previousNumber > number) {
        return delimiter >= number && delimiter <= previousNumber;
      }
      return delimiter <= number && delimiter >= previousNumber;
    });

  useEffect(() => {
    // Handler to call on window resize
    const handleResize = () => {
      // Set window width/height to state
      const width = window.innerWidth;
      const height = window.innerHeight;

      if (doesNumberPassDelimiter(windowSize.width, width, widthDelimiters)) {
        setWindowSize({
          width: window.innerWidth,
          height,
        });
      }
    };
    // Add event listener
    window.addEventListener("resize", handleResize);
    // Call handler right away so state gets updated with initial window size
    handleResize();
    // Remove event listener on cleanup
    return () => window.removeEventListener("resize", handleResize);
  }, [windowSize.width, widthDelimiters]); // Only update when the width changes to redefine the handleResize method

  return {
    ...windowSize,
    isMobile: windowSize.width <= mobileWidth,
    isDesktop: windowSize.width > mobileWidth,
  };
}
