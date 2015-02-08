open System
open System.IO
open System.Threading
open Waterloo.Napoleon
open Waterloo.Bingoal
open WaterlooModule

[<EntryPoint>]
let main argv = 
    let run () =
        let napoleonFeed = NapoleonFeed.Fetch "https://e3-api.kambi.com/offering/api/v2/ngbe/event/live/open.json"
        let bingoalFeed = BingoalFeed.Fetch "https://www.bingoal.be/generated/livemenu1.js"
    
        let printFeed ( feed : Feed ) =
            Console.ForegroundColor <- ConsoleColor.Yellow
            printfn "Bookie = %A" feed.Bookie
            Console.ForegroundColor <- ConsoleColor.White 
            printfn "%67s %10s - %10s" String.Empty "1" "2"
            for e in feed.Events do
                printfn "%30s VS %30s => %10i - %10i" e.Home e.Away e.Bet.Odds1 e.Bet.Odds2 
        
        let printMatches ( matchedEvents : seq<MatchedEvent> ) =
            Console.ForegroundColor <- ConsoleColor.Yellow 
            printfn "Matched events"
            Console.ForegroundColor <- ConsoleColor.Green
            for e in matchedEvents do
            (
                printfn "%30s VS %30s" e.Home e.Away 
                printfn "%23s %10s - %10s" String.Empty "1" "2"
                for b in e.Bets do
                    printfn "%20A => %10i - %10i" b.Bookie b.Bet.Odds1 b.Bet.Odds2
            )

        let printArbitraged ( arbitrageResults : seq<ArbitrageResult> ) =
            Console.ForegroundColor <- ConsoleColor.Yellow
            printfn "Arbitraged events %A" (arbitrageResults |> Seq.length)
            Console.ForegroundColor <- ConsoleColor.Green
            if ((arbitrageResults |> Seq.length) > 0) then
                  for i in 1 .. 5 do
                    Console.Beep(1500, 1000);
            for r in arbitrageResults do
                printfn "%A" r

        let napoleonEvents = convertNapoleonFeed napoleonFeed
        let bingoalEvents = convertBingoalFeed bingoalFeed
    
        printFeed napoleonEvents
        printFeed bingoalEvents

        let matchedEvents = matchFeeds napoleonEvents bingoalEvents

        printMatches matchedEvents

        let arbitraged = arbitrage matchedEvents

        printArbitraged arbitraged

    while true do
        Console.Beep()
        run()
        Console.ForegroundColor <- ConsoleColor.White
        let wait = 1000 * 60
        printfn ""
        printfn "Retrying in %A ms..." wait
        printfn ""
        System.Threading.Thread.Sleep(wait)

    printfn "%A" argv
    0 // return an integer exit code
