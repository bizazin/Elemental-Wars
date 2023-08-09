# Class Dependency Diagramming Portfolio
Hello and welcome to my class dependency diagramming portfolio! Here, I've showcased my proficiency in designing and illustrating the intricate relationships between classes and modules for a game. I've chosen four diagrams that represent specific features of the game to demonstrate the depth and breadth of my skills.

[**Click here to view the diagrams**](https://miro.com/app/board/uXjVMv5eKEQ=/?share_link_id=135373466155)

## 1. Registration module
### Description:
The registration module diagram is designed to represent the user's journey from the start of the registration process to its completion.
### Key Features:

- Utilizes the State Machine Pattern.

- Clearly depicts the various states a user goes through, such as: 

  - *Email waiting*,
  - *Email sent*,
  - *Registration code waiting* and more.
  

This approach ensures that the registration process is intuitive and seamless for the user, while also being maintainable and scalable from a development perspective.
## 2. Quests
### Description:
This diagram provides a comprehensive view of the quests feature, detailing how quests are structured, initiated, and completed.

### Key Features:

- Describes the interaction with the server.
- Illustrates how information from the server reaches the game directly.
- Ensures that quests are dynamic and engaging for the player.

## 3. Battle Scheme
### Description:
The battle scheme diagram delves deep into the intricacies of in-game battles. It meticulously outlines every stage of a battle, ensuring that both the player's experience and the server's understanding of the battle are synchronized and seamless.

### Key Features:

- **Turn Management:** Clearly indicates whose turn it is to attack, ensuring that players are always aware of the battle's flow.
- **Disconnect & Reconnect Handling:** Describes the process of loading data in the event of a disconnect, ensuring that players can seamlessly rejoin the battle without any loss of progress.
- **Server Communication:** Illustrates how information is sent to and received from the server. This ensures that the server is always updated about the current stage of the battle, down to the very second.
- **Animation Timing:** Provides insights into the duration of each animation, ensuring that the server understands the exact timing and sequence of events during a battle.

This comprehensive approach guarantees that battles are not only engaging for players but also robust and fault-tolerant from a technical perspective.
  ## 4. Skill Strategies
### Description:
For the skills feature, I've designed a diagram that emphasizes the flexibility and diversity of in-game skills.

### Key Features:

- Implements the Strategy Pattern.
- Demonstrates that any skill can be invoked using the same method by passing parameters like skill id, initiator unit, target unit, and cast duration.
- Despite the uniform method of invocation, the visual representation of each skill is distinct and unique.
- This approach ensures that adding new skills or modifying existing ones is a streamlined process, while also providing players with a rich and varied gameplay experience.

  ____
  Thank you for taking the time to explore my portfolio. I believe these diagrams not only showcase my technical expertise but also my ability to think critically about game mechanics and user experience. If you have any questions or would like to discuss any of the diagrams in more detail, please feel free to reach out.
