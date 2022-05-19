# Dropdown

At the right of the table, a scrolling tree with all the objects, sorted by tags, is managed by a handful of functions
A list has a structure like this example:
```
Level 0 tag
| Level 1 tag
| | Object
| \ Object
| Another level 1 tag
| | Level 2 tag
| | \ Object
| | Object
| Object
Another level 0 tag
| Object
\ Object

(and so on...)
```

## Structure in the scene

The content of the list is displayed inside a `ScrollRect` which must be inside a `Canvas`.
The content of the `ScrollRect` gets populated by script `SelectionSystem` using two prefabs:

### Prefab ItemListElement

This prefab represent a "line" in the menu for an object. It contains a `Toggle` to show or hide the object, a `Label` (inside the `Toggle`) to display the object's name and a `Slider` to control the object's transparency
This prefab also contains a `SlectionHelper` script that handle the prefab's internal logic which is described later

### Prefab TagDropdown

This prefab represent also a "line" in the menu, but for a tag. It is call "drop-down" since it contains a button with an arrow to show or hide its children (tags and objects in the list)
It contains a `Toggle` with its `Label` to show or hide all the children objects in the scene, and for displaying the tag name respectively. The toggle also contains an image for when only certain children are selected (not all, but at least one), which is shown or hidden by the scripts
It also contains a `Button` to show or hide the children in the list (unlike the `Toggle`, it does not hide the objects in the scene, it just tidy the list)
And finally, it contains its own script like for the `ItemListElement` prefab: `DropdownHelper`

## Scripts

### SlectionHelper

This script manages the real object that the current "line in the list" (an entry) is linked to.

#### Public variables

These public variables gets set by the `SelectionSystem` when building the menu:
`AttachedObject`: The `GameObject` that is attached to the entry.
`Parent`: The `DropdownHelper` of the parent tag, or `null` if the object is at the root of the list (doesn't have parent)

#### Public methods

`void changeVisibility(bool val)`
Change the visibility of the attached `GameObject`.
Gets triggered by clicking on the `Toggle` inside an `ItemListElement` prefab. `true` is visible, `false` is hidden.
Automatically calls the parent `DropdownHelper` `updateStatus` function (see below) after that (if has a parent).

`void changeTransparency(float val)`
Change the transparency of the attached `GameObject`.
Gets triggered by updating the `Slider` inside an `ItemListElement` prefab. 1.0f is fully opaque, 0.0f is fully transparent.
**IMPORTANT**: the `GameObject` must have a `TransparencyHelper`

### DropdownHelper

This script manages the current entry as well as its children.

#### Public variables

`AnimationDuration`: The duration of the transition between the arrow pointing down and right (when clicking on the button to show/hide the children). Defalut is 0.3f
`Checkmark`: The image inside the prefab used when all children are shown.
`Partial`: The image inside the prefab used when some but not all children are shown.

These public variables gets set by the `SelectionSystem` when building the menu:
`ToggleList`: Array of all children toggles. Used to set their status when enabling or disabling the children.
`Childs`: Array of all children entries in the list. Used to hide them when clicking on the drop-down arrow.
`OldStatuses`: Array of bool for saving all the children statuses when clicking on the drop-down arrow.
`Parent`: The `DropdownHelper` of the parent tag, or `null` if the object is at the root of the list (doesn't have parent)
`SelectionSystem`: The `SelectionSystem` of the global scene. Used to communicate with it

#### Public methods

`void onClick()`
Toggle the visibility of the children in the list.
Gets triggered by clicking on the arrow button.

`void apply(bool val)`
Change the visibility of all children objects in the scene.
Gets triggered by clicking on the checkmark toggle.

`void updateStatus()`
Checks whether all children are hidden, all children are visible, or if only some of them are visible.
Then it updates its status accordingly (no children visible = uncheck the toggle / all children visible = checks the toggle and display the `Checkmark` image / only some visible = checks to toggle and display the `Partial` image)
Gets called when a children `SlectionHelper` or `DropdownHelper` is clicked.
Automatically calls the parent `DropdownHelper` `updateStatus` function after that (if has a parent)


### SelectionSystem

The core script that must be in the scene to build the list.

#### Public variables

`ListContent`: The `Content` `GameObject` contained inside the `ScrollRect` to put the prefabs into.
`ItemPrefab`: The `ItemListElement` used when building the list.
`DropdownPrefab`: The `TagDropdown` used when building the list.
`OriginX` and `OriginY`: Coordinates of the origin when building the list
`H_padding` and `V_padding`: The horizontal space used between two levels and th vertical space between two lines

#### Public subclass

This script contains an important subclass, `TagList` which is used to represent a tag in a list.
This class contains 3 variables:
`string tag`: The name of the tag
`List<TagList> childs`: All children tags, in a `TagList` form
`List<ObjectArcheo> objects`: All children `ObjectArcheo` directly linked with this tag (does not contains `ObjectArcheo` inside children tags)

For ease of use, 2 constructors are included that also initialise the lists:
`TagList()` simply build an empty tag
`TagList(string name)`: builds a tag with the name `name`

This structure is used by an `ArcheoLoader` to give all the tree hierarchy to build.

#### Interface ArcheoLoader

This interface is used to give to the `SelectionSystem` the whole hierarchy. By using an interface, we can easily switch between loading from a database, and loading from hard-coded test objetcs for example.

This interface only contains one function: `SelectionSystem.TagList[] loadTags()` that should return an array of all root `TagList`.
When the scene load, the `SelectionSystem` will look for a component (in the same `GameObject` as itself) that implements this interface, and call this function, then build the list from the return value.
Note that only the first `ArcheoLoader` found is called.

#### Implementations of the ArcheoLoader

Currently, two implementations have been made:
The `DummyLoader` automatically creates cubes of different colors and place them somewhere in the scene. It is useful for testing the list without relying on a database.
The `DatabaseLoader` uses the data inside the database and links it to existing `GameObject`s inside the scene (by finding them by name)

On top of that, these implementations call an `ArcheoBuilder` script to add all the needed components on the `GameObject`s. This is useful because we don't have to put a bunch of scripts manually on each object.
