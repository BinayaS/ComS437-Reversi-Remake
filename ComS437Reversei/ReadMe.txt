Start on the Menu Scene located in the Scene Folder!

Note:
- The levels 3 -> 5 had big lag due to the way that the tree for min max was made.
  I couldn't figure out why the tree gets soo big when going 3+ levels deep.
  So I made the difficulty be variable on the value that each piece of the board is worth to the AI.

  !-!-! Still for a functional Game play on level 1.

- The game can just freeze for some unknown reason.
  It gets stuck in an infinite loop and I can't exactly find where this is.
  The culprit is inside the node's Simulate Method.
  It may also be due to an error with putting in a key to a hashtable when the key already exists.

- The piece you are placing can place inside an already placed piece on the board.
  For now just pick it back up and place it in a valid spot.

- Sometimes you won't be able to place your piece. 
  This is due to an internal state for the player's turn not being updated correctly due to some error.
  These erros are mainly due to an out of bounds error in an array and trying to put in a key in a hashtable when the key already exists.

- The game will end itself if it determins that the player or the AI has no valid moves.

- When a game ends the total score is calculated and the winner is determined and displayed on the board.

- AI's Move is shown by a yellow sphere above the played piece.

Controls:
---------
W A S D - move around the board & menu

Space/Enter - Choose level in menu

Left click - Pick up the one pickable piece

Left Shift - Run

Space - Jump

Exit Game - Escape
---------