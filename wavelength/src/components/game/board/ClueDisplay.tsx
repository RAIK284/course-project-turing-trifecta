type ClueDisplayProps = {
  clue: string;
};

const ClueDisplay: React.FC<ClueDisplayProps> = ({ clue }) => (
  <div className="text-center">Clue: {clue}</div>
);

export default ClueDisplay;
