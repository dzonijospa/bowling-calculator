# Bowling score calculator

A simple bowling score calculator, using the traditional scoring method.

## Project structure

The solution is organized into two projects:

- BowlingCalculator.Domain - contains classes and services that represent bowling game 
- BowlingCalculator.API    - API that clients can use to obtain score for a ten pin bowling game

## BowlingCalculator.Domain

 ### 1. `class Game`

The Game class represents a single bowling game.   
Each game has 10 frames in which player can try to hit 10 pins, with the distinction that in the last frame player can get up to 20 more pins 

#### Methods 

1. `void Roll (byte pinsDowned)` - Updates the frames based on the number of pins knocked in each roll

2. `boolean IsGameCompleted()` - Indicates wheather the game is finished or not

 ### 2. `class Frame`

The Frame class represents a single Frame.
Each frame can be in three different states: OpenFrame - default state, Strike - when user hits all 10 pins in first try, and Spare - 
when user hits all 10 pins in two tries.

#### Methods 

1. `void ApplyRoll (byte pinsDowned)` - Updates it state based on number of pins downed

2. `byte GetFrameCompletedScore()` - Returns frame score if it is concluded else returns zero

 ### 3. `class OpenFrame`

class OpenFrame represents Frame state at the start and can change to Spare or Strike based on number of pins that player knocked.
If after first try user hits all 10 pins it transitions to Strike, if he does it after two tries it goes to Spare and if some pin is still
standing after both tries - stays in same state.

 ### 4. `class Strike`
 
 represents Strike state, user has to do another two rolls so the score is calculated
 
 ### 5. `class Spare`
 
  represents Spare state, user has to do another roll so the score is calculated
 
 ## BowlingCalculator.API
 
 It contains a a single Controller with REST endpoint that supports a HTTP POST.
 Request is list of pins downed in consecutive tries.
 Response contains flag - if game is completed and list of running scores per frame.
 
 Example 2 – 6 frames completed, all throws 1
 
 Request (in json) 
 
 ```
 { 
 " pinsDowned ": [1,1,1,1,1,1,1,1,1,1,1,1] 
 } 
 ```
 Response (in json) 
 ```
 { 
  “frameProgressScores”: [”2”, ”4”, ”6”, ”8”, ”10”, ”12”], 
  "gameCompleted": false, 
 }
 ```
 

 
