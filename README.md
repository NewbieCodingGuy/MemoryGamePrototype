<h1>Sequence Memory Game Prototype</h1>

A 2D memory-based Unity game where the player must observe and follow the movement path of a target character across randomly placed spots. The objective is to correctly repeat the target’s sequence of moves and score as high as possible.


<h3>🎮 Gameplay Overview</h3>

The scene contains 12 interactable circular spots placed randomly within the camera view 

A Target character generates a sequence by moving between valid spots.

The Player must click spots in the correct order to match that sequence.

Scoring is granted only for the first correct attempt per sequence index 

The game ends after 40 successful moves, followed by a final summary screen 


<h3>🧩 Key Features</h3>

Random non-overlapping spot placement

Dynamic follow sequence that updates each turn

Visual feedback for correct and incorrect selections

UI with score and step counters

Restart / summary screen on completion

<h3>🛠️ Design Patterns Used</h3>
<h4>🔹 Singleton</h4>

Used for globally accessible systems such as game management and score tracking.

Ensures there is only one instance of managers controlling flow and data.

<h4>🔹 Observer</h4>

Used for communication between game objects such as spot selection, UI updates, and state changes.

Decouples event broadcasting (spot clicked, correct sequence, score change) from event handling.

<h4>🔹 State Pattern</h4>

Controls gameplay flow transitions:

Target turn

Player turn

Wrong selection state

Game end state

Makes logic easy to maintain, expand, and debug.

<h3>📦 Tech & Requirements</h3>

Unity 2022 LTS or later 

DOTween / tweening library supported

Clean, well-structured C# code with proper documentation

<h3>▶️ How to Play</h3>

Start game – Target begins by moving and generating a follow sequence.

Click the correct spot (always the first in the sequence).

Score is added if it was your first correct attempt.

Continue following updated sequences until 40 successful moves.

<h3>📄 Project Status</h3>

Fully functional prototype

Core systems implemented using best architecture patterns

Ready to extend with additional visuals, audio, and difficulty modes


<h3>GamePlay Video</h3>


