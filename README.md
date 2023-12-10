# BattleBox
CS Senior Project
By: Kyro Carolino, Austin Nguyen, Clint Penafiel

## Table of Contents
1. [Description](#description)
2. [Repository Link](#repository-link)
3. [Features Overview](#features-overview)
   1. [RTS Strategy Game](#rts-strategy-game)
   2. [Player VS Environment (PvE) Game](#player-vs-environment-pve-game)
   3. [Economy System](#economy-system)
   4. [Variety of Units](#variety-of-units)
   5. [Dynamic Unit Sprites](#dynamic-unit-sprites)
   6. [Intuitive Unit Placement](#intuitive-unit-placement)
   7. [Placement Restrictions](#placement-restrictions)
4. [Scripts](#scripts)
5. [How to Play](#how-to-play)
6. [Screenshots](#screenshots)
7. [Technologies Used](#technologies-used)


## Description
Welcome to BattleBox, an online real-time strategy (RTS) game where you can engage in thrilling battles against intelligent, autonomous agents. This game is designed for players who enjoy strategic gameplay and want to experience a Player VS Environment (PvE) scenario.

## Repository Link
[GitHub Repository](https://github.com/ClintPenafiel/BattleBox)

## Features Overview
### RTS Strategy Game
Immerse yourself in a real-time strategy environment, where quick thinking and strategic planning are crucial for success.

### Player VS Environment (PvE) Game
Battle against intelligent AI opponents, offering a challenging gaming experience.

### Economy System
Manage your in-game resources efficiently. Both the player and the enemy have their own economy systems.

### Variety of Units
Choose from different types of units to build your army and defeat your opponents.

### Dynamic Unit Sprites
Watch as unit sprites dynamically switch based on the direction they travel, adding visual flair to the game.

### Intuitive Unit Placement
Place units on the battlefield with a simple left-click. Cancel unit purchases with a right-click.

### Placement Restrictions
Player units can only be placed on the player's side, and the player must have enough gold to make a purchase.

## Scripts

Scripts in the repo are located [here](https://github.com/ClintPenafiel/BattleBox/tree/main/SeniorProject/Assets/Scripts)

**SpriteFlipper:**
Flips the sprites of all units, friendly and enemy.

**BaseController:**
Manages the gold of both the player and the enemy. Adding to the number when the gatherer brings it to their respective bases, and subtracting when a unit is bought.

**EnemyManager:**
Keeps track of the amount of enemy gatherer units and where they spawn.

**GameController:**
- Spawns the base of the player and the enemy at a specified location.
- Spawns gold node resources randomly around the player and enemy bases, making sure they are not too close to them.
- Spawns the starting gatherer unit of the player and enemy at a location.

**GathererAgent:**
Script to use the gatherer unit as an agent in Unity's Machine Learning agent framework. The gatherer unit uses this script for reinforcement learning.

**GathererAI:**
Controls the behavior of the gatherer unit used in the game, both friendly and enemy. Tells the gatherer unit to go to a gold resource node, collect gold at that node, and bring it back to base so that it can deposit the gold at base. Allowing the player and enemy to use the gold for unit purchasing.

**GathererController:**
Determines the amount of gold a gatherer unit can carry, the speed at which they accumulate gold at a node, and the speed at which they deposit gold at base.

**GoldManager:**
Manages the gold of both the player and the enemy. Initializes the amount of gold at 0 for both the player and enemy. This script also updates the amount of gold the player and enemy have. Adding to the total amount when a gatherer unit deposits gold, and subtracting a certain cost depending on the unit purchased.

**HealthSystem:**
Universal health system that handles the health of all the units in the game, as well as the bases. This script initializes the health of the base, and sets the initial health based on the unit type. This script also logs the current health and amount of damage taken, while also destroying an object when its health reaches zero.

**LongeRangeAttacker:**
Defines the behavior of the long range attacker unit. This is where the stats of the long range attacker is adjusted, including the speed of the unit, the rate of fire, damage values of its attack, the attack range of the unit, the speed at which the projectile travels at, and the detection range of the unit.

**ProjectileController:**
This script determines the speed at which the projectile moves, as well as its direction, and the amount of damage the projectile inflicts to the hit target.

**ResourceNodeController:**
Determines the behavior of the gold resource node. The node generates gold over time that the gatherer units collect. The script makes it so that gold at every node starts at zero and then accumulates over time until it reaches the maximum amount of gold a node can carry, until a gatherer unit collects it. After a gatherer collects the gold at the node, gold will start accumulating again until a gatherer collects the gold.

**UnitSpawner:**
Allows for the spawn of the different types of units, including the gatherer, melee, range, and tank. It also has the associated cost of gold that is taken away when a unit is spawned. The script also allows the player to spawn a unit at the location of the mouse, allowing for the unit to spawn anywhere on the player's side of the map, or to cancel the placement of a unit. In summary, It handles the visualization of unit previews, mouse interaction, and instantiation of units based on the player's input.

**MainMenu:**
Allows the user to quit the game and start the game.

**GameEnd:**
Displays the Game Over or Win Screen when the game ends.

## How to Play
**Access the Game:**

Visit the [BattleBox Game on GitHub Pages](https://clintpenafiel.github.io/BattleBox/).

**Game Instructions:**

- Follow on-screen instructions to understand the controls and objectives.
- Use left-click to place units and right-click to cancel purchases.

**Enjoy the Battle:**

- Engage in exciting battles against intelligent AI opponents.
- Manage your economy, deploy units strategically, and aim for victory!

# Screenshots
![image](https://github.com/ClintPenafiel/BattleBox/assets/93757667/90dcf326-ca27-494f-b21c-70be8fe67ef2)
![image](https://github.com/ClintPenafiel/BattleBox/assets/93757667/39ae5c25-b460-4fe4-8ef9-f8884c660bbc)
![image](https://github.com/ClintPenafiel/BattleBox/assets/93757667/563254a3-c541-46f7-b20e-e53c473b5389)
![image](https://github.com/ClintPenafiel/BattleBox/assets/93757667/607d2817-a2a7-47fa-8e3a-6b447283361c)

# Technologies Used
- **Unity:** The game was developed using the Unity game development platform to create an engaging and immersive gaming experience.
- **Python:** AI implementation was done using Python, adding intelligent and challenging behaviors to autonomous agents.
- **GitHub Pages:** The game is hosted on GitHub Pages, making it accessible to users without the need for additional installations. Note: Originally, Docker was considered, but GitHub Pages was used due to time constraints.

