**Initial concept**
I am thinking of doing something with procedural animations and colorful visuals.Mainly something space related or the creature from the movie annihilation.
||https://youtu.be/uBsJgceM0KI?t=45||. Creating something organic would be amazing. Also procedural animations would help me in the future since I am doing game design. I feel like it would not be too hard to make especially since we are learning procedural animations.

**Final Concept**

Through developin this project i shifted from my original idea of a procedural entity that moved through sound to a visualiser that is reminisent to a rock thrown at a lake with psychadelic colors. I named it the psychadelic ripples.

![3-water-ripples-pasieka](https://user-images.githubusercontent.com/37370470/71052400-1c53c980-2143-11ea-9b8c-3221b5bb173e.jpg)

**YOUTUBE VIDEO** = https://youtu.be/LClj-0Hve3g

**Intergration**
First thing was to creat a rock for the project. This is controlled usings spectrum data taken from audio. This data controls the local scale and the local rotation of the "rock". there is a formula set in place that i used to keep all the beats intensity similar no so that the visualizer would work with any type of music and the ripple differentiation is dependant on  the bpm of the track. The formula is 
MAXDATAVALUE*100=measure 
250-measure = measureuse which is the multiple that will be used alongside the data to visualise the data in a more evident way.


![d4iljx4-be29b443-fc77-490d-a084-11fd5e0c1253](https://user-images.githubusercontent.com/37370470/71054281-56c06500-2149-11ea-8cdf-a0d31dfbe3b1.jpg)

Next was to add the plates. This was done through instantiating the objects if the rocks scale reaches a certain threshold range and a heavy use of data logging. Such as the object being referenced in a list format, getting the transform of a object on a certain instance and losks so animations can play out and that too many plates would not be created. The way the plate works is that the plate is constantly lerping to a certain scale and the scale is updated until it reaches its highest point and it is deleted, unless if it is the last plate where it is minimised and then deleted. The way the ripple look is created is by overlapping every new plate on the plate before it.
All the colors are decided randomely using a color32 format and changes every instance the rock reaches a certain range in scale.
![image](https://user-images.githubusercontent.com/37370470/71054368-ab63e000-2149-11ea-92f3-5cdd3242f8bd.png)

The spores were created for the downtime in a song where there is very little beat intensity so no plates are spawning. they have thier own movement system that were made using trigonometry and lerping. they also move similar to the plates in they only update the leping when the rock reaches a certain range of scale. They delete themselfves if they move to far a distance and reinstantiate towards the rock. this was done using vector3.dist. 
![image](https://user-images.githubusercontent.com/37370470/71054440-e6661380-2149-11ea-9108-113d280c1457.png)

**Likes and Dislikes in the project** 
I was very proud that i was able to make this project without using any tutorial and made with my own code.
my main dissapointment was a few bugs are still in place and i could not flesh out the spores more.

**Summary**
I am extremely happy with what i have made with this project. they visualiser is very griping and it is very interesting seeing what different patterens the ripples will make for different songs. The main high was seeing a orchestra track being visualized on it making this almost magical like pattern.

![image](https://user-images.githubusercontent.com/37370470/71054497-1f9e8380-214a-11ea-81dd-43e5dbb6de81.png)
