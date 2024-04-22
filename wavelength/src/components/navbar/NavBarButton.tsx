import { cn } from "../../utils/utils";
import CopyTextIcon from "../../assets/icons/CopyIcon";


type NavBarProps = {
  content: string;
  onClick?: React.MouseEventHandler<HTMLButtonElement>;
  filled?: boolean;
};

/**
 * Button component to to be used in nav bars.
 */
const NavBarButton: React.FC<NavBarProps> = ({
  content,
  onClick,
  filled = true,
}) => (
  <button
    type="button"
    onClick={onClick}
    className={cn(
      "border-white border-2 px-5 py-0.4 font-bold rounded-lg h-fit hover:border-white-hover",
      {
        "text-theme-blue bg-white hover:bg-white-hover": filled,
        "text-white hover:text-white-hover": !filled,
      }
    )}
    style={{ display: 'flex', alignItems: 'center' }}
  >
    <span style={{ marginRight: '0.5rem' }}>{content}</span>
    <CopyTextIcon/>
  </button> 
);

export default NavBarButton;
