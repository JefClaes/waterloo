module WaterlooModule

    open System
    
    type Bookie = | Napoleon | Bingoal
    type Bet = { Odds1 : int; Odds2 : int  }
    type Event = { Home : string; Away : string; Bet : Bet }
    type Events = seq<Event>
    type Feed = { Bookie : Bookie; Events : Events }
        
    let convertNapoleonFeed ( napoleonFeed : Waterloo.Napoleon.RootObject ) = 
        let events = 
            napoleonFeed.liveEvents        
            |> Seq.filter (fun x -> x.event.sport = "TENNIS")
            |> Seq.filter (fun x -> x.mainBetOffer <> null && x.mainBetOffer.outcomes <> null && x.mainBetOffer.outcomes |> Seq.length > 0)
            |> Seq.map (fun x ->                
                let extractOutcome ( option : Option<'a> ) =
                    match option with
                    | Option.None -> failwith "Outcome not found"
                    | Option.Some x -> x
                            
                let bet1 = x.mainBetOffer.outcomes |> Seq.tryFind (fun x -> x.label = "1") |> extractOutcome
                let bet2 = x.mainBetOffer.outcomes |> Seq.tryFind (fun x -> x.label = "2") |> extractOutcome

                { Home = x.event.homeName; Away = x.event.awayName; Bet = { Odds1 = bet1.odds; Odds2 = bet2.odds } })                                               
        { Bookie = Napoleon; Events = events }
            
    let convertBingoalFeed ( bingoalFeed : Waterloo.Bingoal.RootObject ) =
        let tennis = bingoalFeed.sports |> Seq.filter (fun x -> x.sport = "TENNIS")
        
        if (tennis |> Seq.length < 1) then
            { Bookie = Bingoal; Events = Seq.empty }
        else 
            let options = 
                (Seq.nth 0 tennis).bets               
                |> Seq.map (fun x -> x.subbets |> Seq.filter (fun y -> y.active && y.bettype = 44) |> Seq.map (fun z -> z.options))
                |> Seq.concat 
            let events = 
                options 
                |> Seq.map (fun x -> 
                    (
                        let quoteToOdd (quot : string) = 
                            Convert.ToInt32(Convert.ToDecimal(quot.Replace(".", ",")) * 1000M) 

                        let o = x |> Seq.toList
                        let home = o |> Seq.nth 0
                        let away = o |> Seq.nth 1
                    
                        {   
                            Home = home.name; Away = away.name                                 
                            Bet = { Odds1 = home.quot |> quoteToOdd; Odds2 = away.quot |> quoteToOdd }                       
                        }
                    ))
            { Bookie = Bingoal; Events = events }
        
    type MatchedBet = { Bet : Bet; Bookie : Bookie; }
    type MatchedEvent = { Home : string; Away : string; Bets : seq<MatchedBet> }     

    let matchFeeds ( napoleon : Feed ) ( bingoal : Feed ) = 
        napoleon.Events 
        |> Seq.map (fun ne -> 
            let matchingEvent = 
                bingoal.Events 
                |> Seq.tryFind (fun x -> x.Home = ne.Home && x.Away = ne.Away)
            match matchingEvent with
            | Some m -> Option.Some { 
                Home = m.Home; 
                Away = m.Away; 
                Bets = [ { Bet = ne.Bet; Bookie = napoleon.Bookie }; { Bet = m.Bet; Bookie = bingoal.Bookie } ]}                
            | None -> Option.None )
        |> Seq.filter (fun x -> x.IsSome)
        |> Seq.map (fun x -> x.Value)

    type ArbitrageResult = { Event : MatchedEvent; Success : bool; Discrepancy1 : float; Discrepancy2 : float }

    let arbitrage ( events : seq<MatchedEvent> ) =
        events 
        |> Seq.map (fun e ->
            let napoleonBet = e.Bets |> Seq.where (fun x -> x.Bookie = Bookie.Napoleon) |> Seq.nth 0
            let bingoalBet = e.Bets |> Seq.where (fun x -> x.Bookie = Bookie.Bingoal) |> Seq.nth 0
            
            let discrepancy1 = Math.Pow(float napoleonBet.Bet.Odds1 / float 1000, float -1) + Math.Pow(float bingoalBet.Bet.Odds2 / float 1000, float -1) 
            let discrepancy2 = Math.Pow(float napoleonBet.Bet.Odds2 / float 1000, float -1) + Math.Pow(float bingoalBet.Bet.Odds1 / float 1000, float -1)

            let success = discrepancy1 < float 1 || discrepancy2 < float 1
            
            { Event = e; Success = success; Discrepancy1 = discrepancy1; Discrepancy2 = discrepancy2 })
        
