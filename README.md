# FlappyBoy
 Flappy Bird-style pixel-based game project. This will be a great learning tool for you and others to understand importing pixel art, basic gameplay loops, UI, and more. Let’s break this down into structure, features, and recommended asset specs.

# 🎮 GAME OVERVIEW
+ Genre: Side-scrolling Flappy Bird clone
+ Level: One infinite-scrolling level
+ Player Input: Spacebar to jump/flap
+ States:
  1. Idle/Hold: Player is idle, with animation swapping between two frames. Message: “Press Space to Jump”.
  2. Play: On pressing Space, the game begins. Score starts counting.
  3. Dead: On collision, freeze everything, show “You Died! Press R to Replay”.

+ Features:
  1. Player can jump and also land on ground (optional)
  2. Blocks/obstacles appear from the right
  3. High score and current score UI
  4. Pixel-perfect rendering
  5. Sounds (jump, crash, etc.)
  6. Fade transitions

# 🧱 STRUCTURE IN UNITY
## 🔧 1. Main Components
+ Object	Purpose
+ GameManager	Handles states: idle, play, dead
+ UIManager	Manages score UI, high score, messages
+ PlayerController	Handles movement, animation, jump
+ ObstacleSpawner	Spawns obstacle prefabs
+ Obstacle	The moving object the player avoids
+ FadeManager	Handles fade-in/fade-out transitions
+ AudioManager	Manages sound effects (jump, crash, etc.)

## 🎨 2. Unity Setup Tips
+ Pixel Perfect: Use the Pixel Perfect Camera (Unity package).
+ Orthographic Camera.
+ PPU (Pixels Per Unit): Usually 16 or 32 PPU for pixel art.
+ Set Filter Mode = Point (no filter) on all pixel textures.

# 🧱 RECOMMENDED ASSET SPECS

## 📏 General Game Resolution
+ Reference Resolution: 320x180 (16:9) – upscale to 640x360, 960x540, etc.
+ Easy to scale on modern screens, looks retro.

## 🧍 Player Sprite
+ Size: 16x16 or 32x32 pixels
+ Idle Animation: 2-frame animation (switch every 0.5s)
+ Flying Animation: 2-frame loop (faster switch e.g. 0.2s)
+ Flap Jump: vertical velocity impulse
+ Landing on Ground (optional): Add a collider to the floor

## 🧱 Obstacle (Pipe/Block)
+ Size: 16x64 or 32x128, varies
+ Spawn Rate: Every ~2 seconds (adjustable)
+ Speed: Moves left at a fixed speed, say 2-3 units/sec

## 🌄 Background/Ground
+ Tiled background or static image, scrolling
+ Ground as collider

# 🔊 Sounds & Music
## 🎼 Music
+ Intro music Triggered on game load
+ Game music Triggered on game play
+ Defeat music Triggered when player loses

## 📢 SFX
+ Jump: Short ‘boing’ or ‘flap’
+ Crash: 'Thud' or 'pop'
+ Flap/Jump	Small "flap" or "boing"	Triggered on Spacebar press
+ Score point	Light "ding" or "chime"	When player passes obstacle
+ Crash/Death	“Thud” or "splat"	When player hits obstacle
+ Start Game	"Whoosh"/pop in	Optional, for UI transition
+ UI Click (if needed)	Button click FX	Optional if using UI menus
+ Restart Game
+ Score Up: Ding
