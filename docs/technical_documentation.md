# Technical documentation

## Interaction scripts with the database

### Data

Database describe each need for the application. It is implemented with SQLite. The following diagram describe this database :

#### Extract models
To extract the models, we used .DICOM files, witch represent differences of density along a XR scan. A software to use it can be [slicer](https://www.slicer.org/) (that is what we used).</br>
We have had to select the best density slots to see correctly the different objects, but keeping them entire. Then, we cut around the wanted object and we can re-adjust the density slots, cut another time, et caetera to have a entire object, but without any soil residue

#### Database

![image](./img/db.png)

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