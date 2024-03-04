import { ReactNode, memo } from "react";
import "./AppBackground.css";

const AppBackground: React.FC = memo(() => {
  const children: ReactNode[] = [];

  // Generate a bunch of children stars
  for (let i = 0; i < 300; i++) {
    const randomXOffset = Math.random() * 100;
    const randomYOffset = Math.random() * 100;
    const randomRadius = 2 + Math.random() * 3;
    const color = Math.ceil(Math.random() * 3); // 1, 2, or 3

    const dot = (
      <div
        key={i}
        className={`dot bg-star-${color} absolute`}
        style={{
          width: `${randomRadius}px`,
          height: `${randomRadius}px`,
          left: `${randomXOffset}vw`,
          top: `${randomYOffset}vh`,
        }}
      />
    );
    children.push(dot);
  }

  return <div className="AppBackground">{children}</div>;
});

export default AppBackground;
