# Database
Utility static class to interact with the Database
## open_database
Open the Database
```c#
private static void open_database()
```
## close_database
Close the Database
```c#
private static void close_database()
```
## check_database
Check if Database number of tables is 5.

**Returns :** True if and only if the number of tables is 5
```c#
private static bool check_database()
```
## database_inspection
Inspect the database to check if all 5 tables are there
```c#
public static void database_inspection()
```
## DataReader
Read data from the Database

**Params :**
- command : String describing the request to the Database

**Returns :** DataTable with the content of the response
```c#
public static DataTable DataReader(string command)
```
## DataWriter
Write data from the Database

**Params :**
- command : String describing the request to the Database
```c#
public static void DataWriter(string command)
```
## DoubleApostropheAndRemoveSemiColumn
Double apostrophes and remove semi-columns in a string

**Params :**
- text : A string

**Returns :** The string with double apostrophe and without semi-column

```c#
public static string DoubleApostropheAndRemoveSemiColumn(string text)
```