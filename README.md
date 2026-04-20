# PairOptimizationPiece

Here is what I did:

BallCollision:

Line 31 - directionBall doesn't need to be normalized twice
UIManager:

Line 3 - Removed unnecessary namespace using System.Linq;
Line 19-23 – Added CompletionText null check
RoundManager:

Line 14 - Made List of Blocks private
PlayerController:

Called moveAction = PlayerInput.actions["UpDownMovement"];
