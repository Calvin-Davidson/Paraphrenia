# Paraphrenia

Paraphrenia is a 2 player online co-op Horror Puzzle game with asymmetrical gameplay.

# Gameplay

Paraphrenia is played online, with two players. Both players play a different role in the game. The first player plays a Mental Health Patient, called Edith Hall. The patient is restrained in a wheelchair. The Patient has the ability to look trough the ghost's eyes and can see paranormal activity, which they must communicate to the second player in order to solve puzzles. The second player plays a Nurse. The nurse can move the wheelchair around and ask the patient questions through "in-game" chat, though they can also walk around alone. The player's need to communicate with eachother to solve the various puzzles found in the Psych ward.

# Horror Story

This game takes place in 1997 America, in an old Psych Ward. There is a ghost haunting the building that only the Patient can see, because of their personal connection to the ghost. The ghost tries to drive the patient insane, and will not shy away from violence and deceit to do so. During the game, the Patient needs to warn the Nurse to hide from the ghost's attacks. The ghost also moves objects, including the Patient's wheelchair.

# Team

The team behind Paraphrenia is called Ribbit Games. This team is a team of 7:

- Sarenka Calis:
Scrum Master, Environment Artist
- Sophie de Groot:
Technical Artist
- Eva van Kessel:
Concept Artist, 2D Artist
- Joost van der Knaap:
Lead Artist, Environment Artist
- Anna van der Wijden:
Character Artist
- Ingmar van Busschbach:
Lead Designer, Product owner, Developer, Sound Engineer
- Calvin Davidson:
Lead Developer, Networking Engineer


# Geproduceerde Game Onderdelen

# Ingmar

### Compositions and SFX:

[Main Soundtrack](https://soundcloud.com/catequil/paraphrenia-soundtrack) made for the game. Initial [Piano Score](https://musescore.com/user/3171721/scores/10967167) was composed in Musescore 4, then produced in Cakewalk. This song plays during the main menu, as well as in-game during certain moments.

[High Action Soundtrack](https://musescore.com/user/3171721/scores/10967170) made for chase moments in the game. It was composed in Musescore 4, and is implemented in-game as-is.

[Sound effects](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Sound) made specifically for the game, by recording source audio in MA's radio studio, as well as sound effects sourced from the internet. All sounds are mixed and tuned by me. These include but are not limited to: Footsteps, ambient sound effects and voice lines.

### Scripts:

[High level event management](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/LevelEvents). Scripts that can activate functionality based on random timers, or volume triggers.

[High level gameplay functionality](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/GameplayScripts). Controllers that affect functionality of lights, emissive materials, and can move objects through lerping.

[Lerp controller and easing dictionary](https://github.com/Calvin-Davidson/Paraphrenia/tree/feature/readme_cleanup/Paraphrenia/Assets/Scripts/Runtime/Misc). Lerping tools that can provide emergent behavior by changing how the object moves from A to B algorithmically.

[Custom sound cue system](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/Sound). This allows an audio source to pick and play randomized sound effects, which greatly enhances immersion.

### AI Systems:

![AI State Machine](https://user-images.githubusercontent.com/53999981/234538534-a6f342ed-db93-4ed9-a66f-6a10bdfddec8.png)
![AI Targeting](https://user-images.githubusercontent.com/53999981/234538559-6af92e44-fab0-4901-99e3-e12317d044fc.png)

[AI systems](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/AI). Complete AI controller with state machine, as well as a set of scripts that drive visualization of the AI's field of view and an [editor script for field of view settings](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Editor).

```mermaid
---
title: Field of View
---
classDiagram
    FieldOfView <.. ViewCastInfo
    FieldOfView <.. EdgeInfo
    UVCalculator <.. FacingDirection
    ProceduralMesh <.. FieldOfView
    UVCalculator <.. FieldOfView
    ProceduralMesh <.. UVCalculator
    class ViewCastInfo{
        <<struct>>
        +bool hit
		    +Vector3 point
		    +float distance
		    +float angle
    }
    class EdgeInfo{
        <<struct>>
        +Vector3 pointA
		    +Vector3 pointB
    }
    class FacingDirection{
        <<enumeration>>
        Up
        Forward
        Right
    }
    class FieldOfView{
        +float viewRadius
        +float viewAngle
        +LayerMask targetMask
        +LayerMask obstacleMask

        -bool drawFieldOfView
        -bool clampUvToBounds
        -float tickDelay
        -float meshResolution
        -float uvResolution
        -float edgeDistanceThreshold
        -int edgeResolveIterations
        -MeshFilter viewMeshFilter
        -Mesh _viewMesh

        +DirectionFromAngle(float, bool) Vector3
        -Start()
        -LateUpdate()
        -FindTargetsWithDelay(float) IEnumerator
        -FindVisibleTargets()
        -DrawFieldOfView()
        -FindEdge(ViewCastInfo, ViewCastInfo) EdgeInfo
        -ViewCast(float) ViewCastInfo
    }
    class UVCalculator{
        <<static class>>
        +CalculateUVs(Vector3[], float, bool) Vector2[]
        -CalculateDirection(Vector3) FacingDirection
        -DotCalculator(Vector3, Vector3, FacingDirection, ref float, ref FacingDirection) bool
        -ScaleUV(float, float, float, bool, float, float) Vector2
        -RecalculateBounds(Vector3, ref Vector3, ref Vector3)
    }
    class ProceduralMesh {
      +Vector3[] vertices
      +Vector3[] faces
      +Vector2[] uvs
    }
```

Displaying the field of view in-game is handled through procedural mesh generation:

(Video, right click to open in new tab)
[![Post Processing Shader](https://img.youtube.com/vi/7quQo6734I8/maxresdefault.jpg)](https://youtu.be/7quQo6734I8)

### Shaders:

I made a custom [HLSL post processing shader](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Shaders/PostProcessing_Shaders) to give a strong visual distinction between the Enemy's view and a player's view. You can compare it here:

![PostProcessing](https://github.com/Calvin-Davidson/Paraphrenia/assets/34209869/30cbc739-0535-486c-b775-9d253ddc99bc)

This shader has a broad amount of settings that can be used to change the final effect:


[![Post Processing Shader](https://img.youtube.com/vi/rIlo-JIiEAM/maxresdefault.jpg)](https://youtu.be/rIlo-JIiEAM)

I was also responsible for setting up the full PBR workflow within our project, which ensures physically correct materials, increasing visual fidelity while staying realistic. The PBR shaders themselves were made partly by Sophie, partly by me.

### Level Design:

All lighting work was done by me. Most of our project has been showcased using my initial lighting pass. This pass is mostly done in post processing, with Exposure, Color Correction, Fog, Ambient Occlusion, Bloom, Lift Gamma & Gain, Shadows Midtones & Highlights, Vignette, Panini Projection, Chromatic Abberation, Lens Distortion and Film Grain. Reflections were handled purely through Specular Highlights in our PBR shaders. This created high visual fidelity, but performance suffered, with average framerate on our test devices being around 20 FPS:

(Video, right click to open in new tab)
[![Initial Lighting Pass](https://img.youtube.com/vi/XRqzWXBp-5o/maxresdefault.jpg)](https://youtu.be/XRqzWXBp-5o)

The final lighting pass was much more optimized. The first step was taking out the less important post processing effects and imitating their effects with the remaining post processing effects, with reflections being handled by accumulative Screen Space Reflections:

(Video, right click to open in new tab)
[![Final Lighting Pass](https://img.youtube.com/vi/vlJJwHPW3DQ/maxresdefault.jpg)](https://youtu.be/vlJJwHPW3DQ)

After that, light sources were optimized, by limiting the influence radius of all light sources. Indirect lighting was baked. Shadow resolutions were optimized. Reflections were optimized. All in all this brought the performance back to a very respectable 90 FPS.

### Client communication:

I was responsible for all [communication with the client](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/ClientCommunication). This was done through email.

# Calvin

### Scripts:

I was responsible for [nearly all networking systems](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/Networking), including the session hosting/joining systems, multiplayer replication, [networked events](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/Networking/NetworkEvent), animation replication, as well as object (player) location replication.

```mermaid
---
title: Networked Unity Event
---
classDiagram
    note for UnityEvent "Default unity event"
    UnityEvent <|-- NetworkEvent
    NetworkEventPermission <|-- NetworkEvent
    class NetworkEvent{
        -NetworkEventPermission _invokePermission;
        -NetworkObject _ownerObject;
        -string _eventNameID;

        _bool _isInitialized;

        +UnityEvent called;
        +UnityEvent calledClient;
        +UnityEvent calledServer;

        +NetworkEvent(NetworkEventPermission);
        +Initialize();
        +Dispose();
        +Invoke();
        -CanInvoke();
        -ReceiveMessage();
    }
    class UnityEvent {

    }

    class NetworkEventPermission{
    <<enumeration>>
    Server
    Owner
    Everyone
}
```

I was also responsible for the [player movement and camera logic](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/Player). This also includes a [camera shake system](https://github.com/Calvin-Davidson/Paraphrenia/tree/develop/Paraphrenia/Assets/Scripts/Runtime/CameraSystems):

```mermaid
---
title: Camera Shake System
---
classDiagram
    CameraData <|-- CameraShakeImpulse
    CameraShake <|-- CameraShakeImpulse
    CameraData <|-- CameraShake
    class CameraShake{
        +String beakColor
        +Awake()
        +Shake()
        +Update();
    }
    class CameraData{
        -float _positionStrength
        -float _positionStrength
        -float _positionStrength
        -float _positionStrength
        +UpdateShake();
    }
    class CameraShakeImpulse{
        +float positionStrength
        +float duration;
        +float falloffExponent;
        +Pulse()
    }
```

### Level Design:

I worked on the majority of the [level design and construction](https://github.com/Calvin-Davidson/Paraphrenia/wiki/Functional-Design#level-design). This includes placing walls, rooms, doors, furniture, etc. Once I finished with the layout, the artists did an art pass to further fill up the rooms with decor.

### Interactions

```mermaid
---
title: Interaction system
---
classDiagram
    Interactable --|> IInteractable : Inheritance
    NetworkedInteractable --|> IInteractable : Inheritance
    Interactor --> IInteractable
    class IInteractable{
        <<Interface>>
        +IsActive : bool
        
        +InteractorEnter() void;
        +InteractorExit() void;
        +DoInteract() void;
    }
    class Interactor{
        +Camera interactorCamera;
        +float interactionDistance = 10;
        +KeyCode interactionKey = KeyCode.E;

        -IInteractable _interactable;

        +UnityEvent onInteractableGain;
        +UnityEvent onInteractableLose;

        -Update();
        -CheckInteract()
        -DoInteractionHit(RaycastHit, IInteractable)
        -DoInteractionMiss()

    }
    class Interactable{
        +UnityEvent onInteract;
        +UnityEvent onInteractorEnter;
        +UnityEvent onInteractorExit;
    }
    class NetworkedInteractable{
        +UnityEvent onInteract;
        +UnityEvent onInteractorEnter;
        +UnityEvent onInteractorExit;

        -InteractorEnterServerRpc()
        -InteractorEnterClientRpc()
        -InteractorExitServerRpc()
        -InteractorExitClientRpc()
        -DoInteractServerRpc()
        -DoInteractClientRpc()
    }
    
```

### Animations:
For all characters i was responsible for creating the animation controllers and implement them accordingly. each of the animators is setup slightly different depending on how to should be controlled and how the states are handled.

**NurseAnimator:**

The nurse animator is the most complex as it has a lot of dependecies, is has to know wether or not it's holding something and wether or not it's walking. All the state managment is done in the animator itself, The NurseAnimationController is only used to set the animators parameters

<img width="720" alt="image" src="https://github.com/Calvin-Davidson/Paraphrenia/assets/53999981/4c04d843-8fd8-4056-a246-3d0a0bcc7147.png">

**WheelchairAnimator:**

The wheelchair animator is a bit unique, the wheelchair animator is just one big blend tree that blends between the different positions it has based on the clients input presented in witdth and height.

<img width="720" alt="image" src="https://github.com/Calvin-Davidson/Paraphrenia/assets/53999981/f252a5b3-a798-46d0-9b17-8bece18a0f10).png">

**EnemyAnimator:**

The EnemyAnimator is the simpelest as it simply set's the animation based on the EnemyAI state. The animation managment is still done in it's own script to make it easy to change or add new states.

<img width="720" alt="image" src="https://github.com/Calvin-Davidson/Paraphrenia/assets/53999981/afef7c52-bb5f-463d-87ee-c87f6fce8c0a.png">

There are still other animators but these three are the most interesting and unique. 
