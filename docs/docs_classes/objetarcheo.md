# ObjectArcheo
Class to keep informations about a selected artifact

**attributes :**
* **me**, the related GameObject, used by Unity in RV interractions
* **id**, the unique identifier that allows retrieve database informations
* **id_excavation**, The identifier gave by archaeologists during the excavation
* **name**, the object name in the database
* **view1,view2 et view3**, booleans to describe when, during the excavation, the object have been withdrawn 
* **description**, small description for the object
* **toy_obj_radius**, radius representation, if it is a "toyobject", witch means we didn't extracted 3D model, so we use a ball
* **toy_obj_color**, color representation, if it is a "toyobject"

## getByTag
Getting caracteristics of objects with this tag

**Params :**
- tag : Tag used to search

**Returns :** Table with the caracteristics of all objects tagged with this tag.

```c#
ObjectArcheo[] getByTag(Tag tag)
```

## getByView
Getting caracteristics of objects on this view

**Params :**
- view : the view studied

**Returns :** Table with the caracteristics of all objects present in this view

```c#
ObjectArcheo[] getByView(int view)
```

## getByNom
Getting caracteristics of objects named nom

**Params :**
- nom : the name of the asked object

**Returns :** the caracteristics of asked object, under the form of an ObjectArcheo

```c#
ObjectArcheo getByNom(string nom)
```

## getAll
Getting all object in the database

**Returns :** the caracteristics of objects, under the form of ObjectArcheos

```c#
public static ObjectArcheo[] getAll()
```

## isToyObject
to know if the object is a toyObject

**Returns :** true si l'object est un toy object, false sinon

```c#
bool isToyObject()
```

## setFouilleID
to set th id_excavation

**Params :**
- id : the new ID

```c#
void setFouilleID(string id)
```

## setFouilleID
to set the name

**Params :**
- nom : the new name

```c#
void setNom(string nom)
```

## setInitialPosition
to set the initial position

**Params :**
- position : the wanted position
- rotation : the wanted rotation

```c#
void setInitialPosition(Vector3 position, Vector3 rotation)
```

## setVues
to set vues where we can see the object

**Params :**
- views[0] : if object is still in the deposit after first photogrametry
- views[1] : if object is still in the deposit after second photogrametry
- views[2] : if object is still in the deposit after third photogrametry

```c#
void setVues(int[] views)
```

## setDescription
to set description of the object

**Params :**
- text : the new description of the object

```c#
void setDescription(string text)
```