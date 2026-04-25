# 3D Flappy Bird Recreation in Unity

## Project Overview
This project is for CS 134 Game Design & Programming. This game takes the core mechanics of the game flappy, and is turned into a 3D game to demonstrate the technical requirements for the class.

## Gameplay
- Press the spacebar to make the player jump
- Avoid hitting obstacles (ice)
- Pass through scoring zone/gaps to increase your score
- The game ends when the player collides with an obstacle
- Press ESC to pause the game and open the settings menu

## Technical Requirements

### Audio
- Background music plays during gameplay
- Sound effects for jumping and collisions
- Sound effect when scoring
- Option to toggle music on and off
- Option for music slider

### Visual Effects (VFX)
- Particle effects when scoring
- Player fire VFX
- Player fire trail VFX
- Player smoke trail VFX


### User Interface (UI)
- Score displayed on screen with updates
- Game over screen
- Restart button
- Settings menu with slider and toggle
- Pause system using the ESC key

### Animations
- Player animations triggered by input
- 3 different types of animations picked by random
- Uses Unity Animator with transitions and triggers
- Rock animation when playing the game
- Obstacle animation
- Starting animation

### Shaders and Materials
- Custom materials used for objects
- Transparent materials used for obstacles
- Different materials/shaders for the character and the different game objects

### Lighting
- Directional lighting is used to illuminate the scene
- Helps improve visibility and overall look of the game
- Individual lighting for the player model
