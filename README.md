Risk Of Rain
(But Worse)

Risk Of Rain (RoR) is A 3rd person Action Roguelike that requires the player to survive an increasing number of enemies that progressively gets stronger and stronger. 

The Idea is to recreate the basic feeling that RoR tries to give the player, which is its simple yet catching gameplay. From there, the basic idea of a game can be made. 

-	Scripts
  o	PlayerController
    	Uses PlayerInput to do basic tasks through events (move, jump and sprint,) Calculates sprint speed by taking move speed and multiplying with a modifier, aswell as a accelerator to not have extreme changes in speed.

    	Updates Health through an integer given by the “Enemy” script

    	Calculates jumps and wait for contact before another jump can be issued

    	All the variables can be changed without causing damage to the other scripts.

o	Gun
    	Uses Raytracing to find what object it hits
    	Looks for a component called “Enemy” and if found, sends an damage update to said script
    	After each attack, a coroutine is started that determines the fire rate. This is done with a Boolean
    	On hit with an object, a game object will be instantiated on impact, to visualize a successful attack. This is done through a particle System.
    o	Enemy
    	Simple movement, that just rotates the enemy towards the player and moves it forwards.
    	Uses a OnTriggerEnter to see if it collides with another object. From there it runs an if-statement, that checks for the tag: Character.  It then plays its damage animation and sends their damage value to the PlayerController script and the game manager script, where after it destroys itself
    	If the Enemies each 0 hp, the object will destroy itself, and send an score update to the game manager.

o	FloatingHealthbar
    	Figures out damage taken by receiving maxHealth and health from enemy script
    	Updates with a simple health / maxHealth calculation
    	In the update method you set the sliders rotation to fit the camera transforms rotation. That way you can see the health bar from all angles.
o	ScoreManager
    	Uses TextMeshPro on a canvas in order to showcase text. 
    	Every time it receives data, either healthChange or AddScore, it runs the UI update on the canvas with an .text
o	GameManager
    	Keeps track of the stage and the current enemies that are active.
    	Runs a constant check to see when we run out of enemies to then spawn more.
    	EnemySpawner is done with a method that adds 1 to the stage and then spawns enemies based on the current stage * a multiplier.
    	Each enemy spawned then gets a random spawn within the game board.
    	To make it fair, we tell the program to find a new vector to spawn on if it is too close to our player.
    	Has a button that runs a restart function when pressed.
-	Animation
  o	Animation is done through an animation controller. Where we give it criteria’s to change animations in the scripts and then updates when there’s a change.
    	Coroutines to ensure that some animations get to finish playing before they stop. Example of this is enemy Attack animation, which stops the enemies movement, starts the animation, and destroys game object when its done.
-	Hierarchy
  o	Things that are connected to each other are made into a parent/child.
  o	An example of this, is the health bar on the enemy script, which is a child to the Enemy prefab. 
  o	Another example is the Map that is made off of different 3d objects. They are put together under one object, to save time when changing size and positions.
  o	The particle system that appears when the player attacks, is also a child to the player.
-	Objects and Materials
  o	Terrain is used to establish some depth to our level, and give it some levitation. It helps build a more interactive environment.
  o	The materials are used to add flavour to our environment and helps tell a story about the place which the player finds themselves. Most of the materials are found in the unity asset store.




Time Table
Different Part of the Project	Estimated time spent.
Player Controller	4 hours
Enemy Script	3 hours
Making Terrain	2 hours
Animations	5 hours
UI and Game manager	5 hours
Communication Between Scripts	3 hours
Fixing GitHub	3 hours
Total:	Approx. 24 hours

References:

Materials Found:
Character : https://assetstore.unity.com/packages/3d/characters/humanoids/rpg-tiny-hero-duo-pbr-polyart-225148?aid=1101lw3sv
Enemies: https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-buddy-pbr-polyart-253961?aid=1101lw3sv

YouTube

Character Controller : https://www.youtube.com/watch?v=WNV9l04s8t4&list=PLBcfp6HMOJwzDcdCzoAx3jJKm7sIcBXJZ
Healthbar:
https://www.youtube.com/watch?v=_lREXfAMUcE

Unity Tutorials
Brackeys Raycast and particles: www.youtube.com/watch?v=THnivyG0Mvo&t=
Brackeys Terrain: https://www.youtube.com/watch?v=MWQv2Bagwgk


Sending Data between scripts: https://stackoverflow.com/questions/61781488/how-to-reference-the-int-variable-to-another-script-which-is-in-another-gameobje
Finding Components in Children:
https://docs.unity3d.com/ScriptReference/Component.GetComponentInChildren.html



