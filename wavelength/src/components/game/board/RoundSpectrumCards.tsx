import { SpectrumCard } from "../../../models/SpectrumCard";
import { cn } from "../../../utils/utils";

type RoundSpectrumCardsProps = {
  spectrumCard: SpectrumCard;
};

type SingleSpectrumCardProps = {
  name: string;
  direction: "left" | "right";
};

const SingleSpectrumCard: React.FC<SingleSpectrumCardProps> = ({
  name,
  direction,
}) => {
  const beforeElement =
    "before:bg-theme-blue before:h-6 before:w-[112%] before:absolute before:bottom-[-0.1rem] before:rounded-full before:content-[' ']";
  const afterElement =
    "after:h-5 after:w-full after:absolute after:bottom-2 after:rounded-sm after:content-[' ']";

  return (
    <div
      className={cn(
        "w-1/3 h-40 rounded-lg flex items-center justify-center flex-col relative text-xl",
        beforeElement,
        afterElement,
        {
          "bg-cover-blue after:bg-cover-blue": direction === "left",
          "bg-team-1-score-holder after:bg-team-1-score-holder":
            direction === "right",
        }
      )}
      style={{ zIndex: "200" }}
    >
      {name}
      <div className="tracking-[-0.5rem]">
        {direction === "left" ? "<—————" : "————>"}
      </div>
      {/* <div
    className="absolute bg-theme-blue h-5 bottom-0 rounded"
    style={{ width: "105%", zIndex: "-20" }}
  /> */}
    </div>
  );
};

const RoundSpectrumCards: React.FC<RoundSpectrumCardsProps> = ({
  spectrumCard,
}) => (
  <div className="flex p-3 gap-10 items-center justify-center">
    <SingleSpectrumCard direction="left" name={spectrumCard.leftName} />
    <SingleSpectrumCard direction="right" name={spectrumCard.rightName} />
  </div>
);

export default RoundSpectrumCards;
