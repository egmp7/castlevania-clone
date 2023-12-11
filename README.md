**Castlevania-Clone Development Log:**

**Date: [12/9/2023]**

**Environment and Movement:**
1. Created a new project in Unity, focusing on the 2D environment.
2. Set up the initial environment using squares as placeholders.
3. Implemented a player character based on a sprite, allowing movement through user input from the Input Manager.
4. Established a camera that smoothly follows the player using Cinemachine.
5. Added animations to the player sprite, enhancing the visual appeal.
6. Implemented jumping functionality, enhancing player mobility.

**Building an Enemy:**
1. Designed and implemented an enemy character with a sprite and animations.
2. Established enemy behavior to dynamically follow the player's movements.
3. Integrated an attack animation and method for the enemy.
4. Implemented methods for the enemy to receive damage and transition to a "hurt" state.
5. Added a "die" method for the enemy, allowing for removal from the game world upon defeat.
6. Modified the state machine to ensure the enemy only follows the player under specific conditions.

**Collisions & Events:**
1. Introduced player health functionality, allowing the player to take damage.
2. Implement a player collision system to simulate injury upon contact with enemies, enhancing the gaming experience by introducing dynamic interactions between the player and adversaries.
3. Connected the player's health functionality to the onPlayerDeath event, allowing for responsive actions upon player death.
4. Implemented an Observer Pattern to manage events, specifically creating an onPlayerDeath event.

**Next Steps:** 
- Explore and implement additional player abilities, such as weapon attacks or special moves.
- Refine enemy behavior, introducing more complex patterns and interactions.
- Enhance the visual and auditory aspects of the game, focusing on aesthetics and immersion.
- Implement a scoring system or other progression elements to engage players further.
- Consider integrating a level design system to create diverse and challenging environments.

**Challenges Faced:**
1. **Implementing Enemy AI Patterns:**
   - Challenge: Design and implement more complex enemy AI patterns, such as enemies that can patrol, chase the player intelligently, or perform varied attack patterns.
   - Goal: Enhance the gameplay by introducing challenging enemy behaviors that require strategic player movements.

2. **Adding Environmental Hazards:**
   - Challenge: Integrate environmental hazards, such as spikes, moving platforms, or traps, that pose additional threats to the player.
   - Goal: Create a dynamic and engaging game environment that requires players to navigate carefully.

4. **Creating a Boss Battle:**
   - Challenge: Design and implement a challenging boss enemy with distinct attack patterns, multiple phases, and unique behaviors.
   - Goal: Add a climactic element to the game, requiring players to employ advanced strategies to succeed.
9. **Testing and Balancing:**
   - Challenge: Conduct thorough playtesting to identify and address potential bugs, improve game balance, and gather feedback on the overall player experience.
   - Goal: Ensure a polished and enjoyable gameplay experience for a diverse audience.

10. **Creating a Dynamic Camera System:**
    - Challenge: Implement a dynamic camera system that adapts to the player's movements, providing a clear view of the action and enhancing the overall game feel.
    - Goal: Improve visual clarity and responsiveness in different gameplay scenarios.

**Lessons Learned:**
- [Share insights gained from the development process, including any new techniques or skills acquired.]

**Upcoming Goals:**
- [List specific goals and milestones for the next phase of development.]

**Closing Thoughts:**
[Share any reflections on the progress made so far, express excitement for upcoming features, or note any personal achievements during the development process.]