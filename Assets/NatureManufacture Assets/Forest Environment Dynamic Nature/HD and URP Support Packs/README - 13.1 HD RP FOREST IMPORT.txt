BEFORE YOU START:
- you need Unity  2022.1 or higher 
- you need HD SRP pipeline 13.1 if you use higher etc custom shaders could not work but seems they should. 
That's why we provide 13.1 version which seems to work with much higher versions aswell. 
For all higher RP versions please use 13.1 HD RP support pack.

Be patient this tech is so fluid... we coudn't follow every beta version

Step 1
	- !!!! IMPORTANT !!!! Open "Project settings" ->"Gaphics"-> "HDRP global settings" ->  "Diffusion Profile Assets"
	and drag and drop our SSS settings diffusion profiles for foliage and water into Diffusion profile list:
		  NM_SSSSettings_Skin_Foliage
		  NM_SSSSettings_Skin_NM Foliage
		  NM_SSSSettings_Skin_NM Foliage Trees
		  NM_SSSSettings_Water_Forest
	Without this foliage, water materials will not become affected by scattering and they will look wrong.
	Open "HDRPMediumQuality" in project settings or "HDRPHighQuality" depends what unity use i your projectas default and:
	- LOD Bias to = 1 or 1.5

Step 2 Go to project settings and quality and set:
	- Set VSync to don't sync

Step 3 Find "HD RP Forest Demo Scene" and open it.

Step 4 - Choose way of movement. Movie track or free movement.
	Choose camera and turn on or off "playable director" and "animation" or leave free camera movement turned on.

Step 5 - HIT PLAY!:)

About scene construction:
		- There is post process profile: Post Process Volume. Manage post process by scene post process object.
		- There is Sky and Fog Volume object, It's are important like hell because basically it's the core of rendering and light management.
		- There are Density Volume objects which manage volumetric fog density in specific areas
		- Prefab wind manage wind speed and direction at the scene

Play with it, give us feedback and learn about hd srp power.

