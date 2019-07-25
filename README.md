* DOSBox, Scrum.org, Professional Scrum Developer Training
* Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
* Copyright (c) 2012 All Right Reserved

# Modify Project (Igustiawan)

### [BUG] MKFILE command

```
Acceptance criterias:
- MKFILE <filename> should not return a error message.
- MKFILE <filename> should create an empty file. [BUG] MKFILE command
```
  
### Record File & Directory timestamp 

```
Acceptance criterias:
- MKDIR <dir> should record the timestamp of when the directory was created.
- MKFILE <file> should record the timestamp of when the file was created.
```

### EXIT command

```
Acceptance criterias:
- EXIT exits the DosBox without any error message.
- EXIT GUGUS exits the same way.
- EXIT1 shall not exit.
```

### VER command

```
Acceptance criterias:
- VER outputs Microsoft Windows XP [Version 5.1.2600] as a fixed text.
- VER /w outputs same as VER but also outputs the name (surname and name) and email of the developers. 
  Every developer name and email is listed on a new line. 
```

### TIME command

```
Acceptance criterias:
- TIME will output the current operating system time.
- TIME 21:30:10 
  Command accepted, no output on console
- TIME gaga
  Command rejected with error message displayed on console   
```

### TYPE command

```
Acceptance criterias:
Given there is already a <file name> in the root directory
When I enter TYPE <file name>
Then the content of the file is displayed on the console

Given I am at a root directory 
When I enter TYPE <file name> of a file that does not exist
Then  Error Message "The system cannot find the file specified" is shown on the console

Given I am at a root directory with <subdirectories>
When I enter TYPE <subdirname>
Then Error Message "Access is denied" is shown on the console
```

### DEL command

```
Acceptance criterias: 
Given:	a file named file1 in current directory already exists
When:	DEL file1 command is entered
Then:	file1 is deleted

When:	DEL on <NonExistingFile> is entered
Then:	Error Message: Could Not Find C:\NonExistingFile (The absolute path is printed) is printed

Given:	A file C:\mydir\mysubdir\file2 exists	
And:  The Current directory is C:\mydir
When:	DEL mysubdir\file2 (relative path)
Then:	file2 is deleted

Given:	A file c:\mydir\mysubdir\file2 exists
When:	DEL c:\mydir\mysubdir\file2 (absolute path)
Then:	file2 is deleted
```

### COPY command

```
Acceptance criterias:
Given I am at a directory
And a <filename> with same name does not exists at <destination> (destination must be a directory)
When I enter COPY <filename> <destination>
Then the file will be copied to <destination> directory

Given I am at a directory
And a <filename> with same name already exists at <destination> (destination must be a directory)
When I enter COPY <filename> <destination> /y
Then the system will overwrite an existing destination file
```
