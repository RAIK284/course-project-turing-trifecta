type ClueDisplayProps = {
  clue: string;
};

const ClueDisplay: React.FC<ClueDisplayProps> = ({ clue }) => (
  <div
    aria-label="ClueDisplay"
    className="text-center rounded bg-scoreboard-dark-blue w-1/2 flex flex-col p-2"
  >
    <span className="uppercase tracking-wider font-semibold text-sm">Clue</span>{" "}
    <span>{clue}</span>
  </div>
);

export default ClueDisplay;
