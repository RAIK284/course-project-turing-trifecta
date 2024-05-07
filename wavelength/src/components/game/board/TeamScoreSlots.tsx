function TeamScoreSlots ({score, teamOne}: {score:number; teamOne: boolean}){
    const numberOfSlots = 10; // Score needed to win
    const slots = [];
  
    // Create an array of elements to render
    for (let i = 0; i <= numberOfSlots; i++) {
      slots.push(<ScoreSlot point={numberOfSlots - i} firstTeam={teamOne} atScore={score}/>);
    }
  
    return (
      <div className="w-2/3 flex flex-col justify-center">
        {slots.map((slot, index) => (
          <div className="mb-4">
            {slot}
          </div>
        ))}
      </div>
    );
  }
  
  function ScoreSlot ({point, firstTeam, atScore}: {point: number; firstTeam: boolean; atScore: number} ) {
    if(atScore != point){
    return(
      <div className="bg-scoreboard-blue h-full rounded-full text-center">{point}</div>
    );
    }
    if(atScore && firstTeam){
      return(
        <div className="bg-team-1-score-holder h-full rounded-full text-center text-black">{point}</div>
      );
    }
    return(
      <div className="bg-team-2-score-holder h-full rounded-full text-center text-black">{point}</div>
    );
  }

  export default TeamScoreSlots;
