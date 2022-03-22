Hello and thanks for choosing to read this! 
Please read all of it!

Things you will need:
-Unity 2020.3.19f1
-Ink Editor
-Patience

Inside this repo is the .ink file that I used to test and create this system.

This system uses Ink's choice logic structure to make the game work so you need to know that before you can start editing.

Here is the youtube playlist I based the entire structure off of
https://www.youtube.com/watch?v=nWQmPCDPl04&list=PLlXuD3kyVEr5V8bK9TnEptHgoa_mYMTjb&index=1&ab_channel=DanCox


--Editing or Making a .ink File---
When editing or making a new .ink file please make sure to Export (with the same name) as a .json to the JSON Folder of the project.
MAKE SURE IT DOES NOT SAVE AS A .ink FILE THIS IS EASY TO OVERLOOK
(see the images in the Documentation subfolder of this folder for the step by step process)

Also when making a new .ink make sure that the exported .json file is hooked into the system through the inspector. DO NOT USE THE .ink FILE
(see the step 5 image in the Documentation subfolder)
(additionally, using the name name when exporting as a JSON will make sure the system updates automatically)


--Setting a background or special state--
As you can see in the .ink file there is a %test% in the main dialogue, don't worry it won't show up! For this system I use dual percentage characters to denote a state change. (you can change it to be whatever you want it to be in the Dialogue Manager script)
%test% will change the game state to "test" in the Global script and change the background
If you want the background to change at all you must include this kind of command into what will be your displayed dialogue
You must add these backgrounds and commands in the Dialogue Manager script, sorry!


--Stage Directions--
One major system of this game is called Stage Directions
Stage Directions allows you to tell the computer where to place / how to move actors in a given scene.
Right now there are only 3 categories of Stage Directions in a specific order

#actor #action/starting position #end position

Examples:
#garf #runsRightTo #left  
//garfield will run from offScreenRight to screenLeft

#tri #starts #right 
//garfield will start the scene on screenLeft


IMPORTANT RULES!!!!!

-when a character is not explicitly directed they will be off screen in the collective reset position

-each Stage Direction must have a # before it

-when adding a new category of Stage Direction you must change the public int value, numOfDirectionsPerActor, located in the Inspector of the DialogueManager script

-you must always use the same number of Stage Directions for each called actor (that also means each new category must have a default of some sort like #start)

-whaetever the order is, you must use it for all stage directions to match the order of operations in the DialogueManager script

-when adding a new category you must add it yourself to the DialogueManager script in order for it to work

-when adding a new actor, action, end position, and ect. you must add and hardcode it's function in the DialogueManager script


That's all folks!
Happy writing and email me if you got any questions!
