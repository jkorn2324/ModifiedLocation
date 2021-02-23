## Documentation

This is the documentation for my Augmented location game prototype. This repository contains all of the code for this game and this readme will go through the code to explain how it works in a nutshell.

This game was made using Unity 2019.4.13f and uses the AR Foundation package along with ARKit, the iOS native augmented reality package. For rendering, the game uses the Universal Render Pipeline.


## Table of Contents
<!--ts-->
* [Documentation](#documentation)
   * [Table of Contents](#table-of-contents)
   * [Gameplay](#gameplay)
   * [Code Structure](#code-structure)
   * [Credits](#credits)
<!--te-->

------------

### Gameplay

Originally based off of the riddler trophies and riddles treasure hunt mechanic in Batman: Arkham Asylum, this augmented reality location project was also supposed to be treasure hunt game, where the player would actually create the treasure hunt themselves via images and pass it along to their friends, whose goal was to find all of the riddles provided by the player. Currently, I was only to get the object and image scanner portion of the project working along with thee riddle solver. Right now there isn't really much, only a couple pictures with some really bad riddles for the user to solve. 

------------

### Code Structure

The code structure for this game heavily utilizes[ ScriptableObjects,](https://docs.unity3d.com/Manual/class-ScriptableObject.html " ScriptableObjects,") which basically are Unity's get out of jail free card (literally the best). They allow the developer to create a script that can be turned into various instances of itself as an asset.

A code structure that utilizes ScriptableObjects prevents dependencies that could otherwise break the prototype if they failed to exist. Furthermore, it allowed me to test various aspects of the prototype independently, as I was easily able to test one part of it at a time. I created ScriptableObjects that held variables and events, which were "fed" in to various scripts throughout the project and were utilized based on the context of the script I was working with. They could each be found in the utils folder in this repository.

------------

### Credits

Since some of the assets used in this project were taken from outside sources, this section provides credit to the makers of the tools, sounds, and other assets used in this prototype:
- [Image and Video Picker](https://assetstore.unity.com/packages/tools/integration/image-and-video-picker-28597 "Image and Video Picker") - A Unity asset that allows the users to access one's camera roll directly in-game.
