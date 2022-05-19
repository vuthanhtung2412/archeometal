# Technical documentation

## Interaction scripts with the database

### Database

Database describe each need for the application. It is implemented with SQLite. The following diagram describe this database :

![image](./img/db.png)

### Interact
Each database table has it script to interact with the database.


[DataBase](./docs_classes/database.md)

[MetaData](./docs_classes/metadata.md)

[ObjetArcheo](./docs_classes/objetarcheo.md)

[Tag](./docs_classes/tag.md)

### About SQL injections
This database is potentially vulnerable to SQL injections. Risk analysis shows that no part is exposed to the internet and therefore to huge amount of potential cyberattack.

With regards to this element, developers have try to avoid commun injections with doubling of apostrophes and removing semi-columns.

However, we recommand you to avoid using theses characters when requesting and writing into the database.