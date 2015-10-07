# ShowDownSharp
This is a simulator for the MLB ShowDown Cards baseball card game.  The game is played with player cards and dice.
The player cards list results based on the abilities of the player and dice rolls randomly determine outcomes.  The card game is
designed to be a two player game, but various rule conventions would allow one person to play two teams head to head.

My sons love the game and helped motivate me to build the software.  I found card data on line for all cards produced 
(2000 - 2005).  The project contains:
- a database of all of the cards produced with the player stats and outcomes 
- a league manager tool for creating your own teams from the card database
- the game engine based on the official rules
- a game simulator which will you let play at-bat by at-bat or silently simulate a complete game.
- statistics tracking across games played with a ui to view

This is a work in progress with more features I am considering building:
- some form of manager AI  to implement various management ideas and substitution models
- ability to play over the internet.  my sons have grown up, one in college and the other recently graduated, 
so they aren't at home any more
- a more appealiong game rendering.  the current version just moves names around the diamond as I have been putting most
of the effort into game mechanics
- performnace tuning for game loading of the selected league data

My recently graduated son is a baseball fanatic and has a Masters in Applied Stats.  He analyzed the stats of players 
from the 2014 MLB season to create card data that would rate similar to the original cards.  That data is now in the 
game database.  I recently found complete historic player statistics in an open source databse, so my next challeenge 
for him is to producce an algorithm to create card data for every year.

The applicationss are built in C#, with sqlite as the database.  Currently using zzzz SQLIte ADO library.  No other 
special libraries are needed for the current version.
