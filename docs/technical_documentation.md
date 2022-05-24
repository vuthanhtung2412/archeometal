# Technical documentation

## Interaction scripts with the database

### Data

Database describe each need for the application. It is implemented with SQLite. The following diagram describe this database :

#### Extract models
To extract the models, we used .DICOM files, witch represent differences of density along a XR scan. A software to use it can be [slicer](https://www.slicer.org/) (that is what we used).</br>
We have had to select the best density slots to see correctly the different objects, but keeping them entire. Then, we cut around the wanted object and we can re-adjust the density slots, cut another time, et caetera to have a entire object, but without any soil residue

#### Database

![image](./img/db.png)

##### Obj (describe an archeological object)
Each object is describe by fields :
- An id is given at the import into the scene
- An id is given by the experts during the excavation
- A name
- Initial position (in three dimensions) to locate the object in the scene
- Initial rotation to give orientation to the object
- View1, View2, and View3 are true (i.e. equal to 1) when the object is present in excavation stage 1 (or 2 or 3).
- A description is given to describe verbatim the object
- A toy object is a circle or a cube that represents the object when we do not have individual 3D-model related to this object. A color and a radius describe the toy object.

##### Metadata
A metadata is an image, text that is realated to the object. A name describe the metadata and the id_obj identify the metadata object. The type_name describe the type the metadata is. The URI is the path to the real metadata. Relative position describe the metadata position relatively to the center of gravity of the object.

##### Tag
A tag is a word (or word group) that describe briefly the object. A tag can have a parent that is also a tag (recursive parentality).

##### tag_mag
This register the link between a tag and an object.

### Interact

#### With the object in scene

##### To grab correctly the objects
We had some problems : 
* firstly, the meshs was too complex and that was slowing the solution. To resolve it, we just needed to simplify the 3D models and add them as mesh colliders on unity
* Secondly, the objects were always taken from the same point : when you are trying to take it from another point, it turn, and replace as usual. 

##### To select objects we wants to show

##### To show metadatas

##### To see the object different

#### With the database

Each database table has it script to interact with the database.

[DataBase](./docs_classes/database.md)

[MetaData](./docs_classes/metadata.md)

[ObjetArcheo](./docs_classes/objetarcheo.md)

[Tag](./docs_classes/tag.md)

these scripts are then used to initiate scene in ArcheoBuilder and DatabaseLoader script

### About SQL injections
This database is potentially vulnerable to SQL injections. Risk analysis shows that no part is exposed to the internet and therefore to huge amount of potential cyberattack.

With regards to this element, developers have try to avoid commun injections with doubling of apostrophes and removing semi-columns.

However, we recommand you to avoid using theses characters when requesting and writing into the database.