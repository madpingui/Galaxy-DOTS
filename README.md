# Galaxy DOTS
This is a practice project for the efficient handling of entities using Unity DOTS. 

The project works by instantiating an entity every 0.05 seconds, which is a sphere with an emissive material (simulating a star). Each sphere is instantiated with a scale of 0, and a job is responsible for scaling it to a specific size. Each entity moves toward the center (black hole) and also has lateral movement, creating an orbital motion for each entity. When the entity reaches the center, it is destroyed. 

Each star’s properties are randomized within two ranges. These properties include the scale, forward movement speed, and lateral movement speed. 

The black hole is simply a large sphere with a shader I found online (properly credited in the project). The background is just a cubic skymap of the Milky Way. 

In my tests, I managed to instantiate over 17,000 entities with a stable frame rate of 144 FPS (the refresh rate of my screen).

https://github.com/user-attachments/assets/c382e7dd-79e7-491f-95dd-a035a8b15500 
