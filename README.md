# Distance Oriented Culling System (DOCS)
A very fast and lightweight Unity3D cull system based on distance. DOCS implements a spatial hash table to do a fast query in all subscribed objects to cull system and apply culling commands on them.  Any objects implementing ICullableObject can be culled or unculled. This technique is great for optimization of scenes have lots of objects that can be stopped doing their duties (mesh renderer, visual scripts, AI behaviors)   until close enough to the center point of area of interest (camera).
## How System Works:
The system consist of five key elements:
### 1-CullSystemManager:
As the name say this element has responsibility of  creating spatial hash table, add or remove **CullableObjectTag** to or from system and on each request generates a lists of **CullableObjectTag** in specified distance from the **CenterOfCullSystem**  and make them to perform *uncull* command while *culling* rest of the existing *Tags*.
You can have **CullSystemManager** as many as you need in your scène but each one must has unique *HashTableSystemId*.
### 2-CullableObjectTag:
Main task of this element is submitting the position of containing gameobject to corresponding **CullSystemManager** (the *CullSystemManager* having same *ID*).
**CullSytemManager** tells Tag to be culled or not and Tag will makes all of **ICullableObject** components in all children node which have same ID value to this Tag to perform their OnCull or OnUnCull method.
A gameobject can have more than one Tag with different ID.
### 3-ICullableObject
Implementation of this interface determines what will happen for this component when it got culled or unculled when receiving this type of commands from corresponding Tag which is placed higher in hierarchy. Notice if you are implementing this interface you need to consider removing this object from all Tags which this cullable object is connected to them when  cullableojcet is going to be destroyed.
There are some predefined Cullableobjects in project you may find them useful including objects to cull meshes, skinned meshes, children gameobjects and children colliders.
### 4-CenterOfCullSystem:
Feeds **CullSystemManager** with a transform’s position as center of culling system area of interest 
### 5-CullSystemDependencyManager:
Connects all elements above together in run time. **Only one DependencyManager is allowed to exist in all loaded scenes.**

## How to use: 
1.	Add **CullSystem** Prefab to your scene
This prefab includes one **DependecyManager**, one **CullSystemManager** with id of 0 and one **CenterOfCullSystem**.if you need more cull systems you may add them on this gameobject and make and add one **CenterOfCullSystem** per each added cull system.
Usually you need to attach your camera transform to **CenterOfCullSystem** to perform culling based on camera position.
2.	Add the CullableObjectTag component to all gameobjects you need to cull them based on their position in the space.
3.	Add any component implementing **ICullableObject** to Tag gameobject or any children of it. **ICullableObjects** determines which action to be taken when Tag object is culled
