# Weather Addons
A collection of addons for the Beat Saber Weather mod

Sync up your weather effects to the music. There are currently 3 ways of syncing particles to the music that effect creators can choose from. 
The addons will not currently work on Quest.

To install in Beat Saber, place the WeatherAddons.dll into your plugin folder.

# Creating Effects With Weather Addons
First you will need the Weather Unity project. This can be found [here](https://github.com/Futuremappermydud/Beat-Saber-Weather/releases).
Once you have the Weather Unity project, you can add the WeatherAddons.dll to the project.
You can now create effects as you normally would, but now using the scripts in the dll.

Addons must be added to the game object with the particle system you want the effect on.
Use [this guide](https://docs.google.com/document/d/1unpPb_R5WGtdpPGP_CpkfZzKxpipLexYjGRuPnuHC2k/edit?usp=sharing) if you don't know how to make effects.

# dB To Rate
Change how many particles your particle system will emit based on the current volume of the song. 
Choose the smallest and largest emission rate to create a gradient. Then choose the volume range to play that gradient in.

# dB To Toggle
Turn off the particle system if the song gets quiet.
Just choose the minimum required volume for the effect to play, and it will turn off and on by itself.

# Spike To Play
Turn the particle system on for one frame if the volume spikes up.
Choose the sensitivity, and the addon will take care of the rest.
It is recommended to turn off particle looping and play on awake. You should also set duration to a very low number, such as 0.05.
As decibels are exponential, and not linear, the addon will dynamically adjust to the current volume.
