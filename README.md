# Chronopath

## Description
Chronopath is a 2D endless platformer made with Unity engine. The goal of the game is to go as far as you can until nightfall. There are various obstacles blocking the way, the player has to get through them by either smashing it down or by altering time. Some obstacles appear/disappear in certain times, the player can use this to make their path forward.

## Details
### Obstacles
An obstacle can have multiple unique attributes, they are Blipping, Delayed, Existing and Dynamic. When created, the algorithm randomly adds these attributes to an obstacle. To be able to assign new behaviour without affecting the existing ones, **Decorator Pattern** was used. This way, all of the attributes can be added to an object without needing to remove another.

The obstacles have a point system and each attribute has a point. With each attribute added, this score increases based on the attributes score. The point system is used in chunks for determining how many obstacles to create.

### Chunks

A chunk is the combination of gameplay elements in a playable area. A chunk is responsible for holding obstacles and handling anything related to them. When a chunk is created by the world, it spawns obstacles using the **Factory Pattern**. The factory creates obstacles randomly and returns it to the chunk.

When an obstacle is created, it recieves a point based on its attributes. Each chunk has a point limit so that the obstacles do not make the game too hard or too easy. A chunk keeps spawning obstacles until this threshold is reached.

### Time
Chornopath has a very basic day cycle. The game starts from dawn and continues until nightfall. The player can see time progressing through the sun and the skybox.

The player move the world time forward or backward using the time-machine. The time-machine can be used to avoid nightfall or to make obstacles disappear (or both).