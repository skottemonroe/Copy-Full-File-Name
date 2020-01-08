# Copy-Full-File-Name
Use this to right click on a file or folder, and copy the name or path to your clipboard.



## Source
I have included the very minimal you would need to build this.  Basically, just the Visual Basic source code.  You will need to create a Visual Studio solution, probably as a CLI tool, then put the code in there.  Should be enough, honestly, since the program doesn't need any libraries or anything else weird.  But it does require you to know a bit about how to build an executable program.




## Installer

Fortunately, you don't have to build the tool from nothing.  I have done this fFor you.  In theory, you can download the EXE and the BAT fFiles, and double click the BAT, and it will install correctly.  However, the batch fFile requires admin privs, which throws some people off.  It's a bit tetchy, but when it works, it works quite well.

It will copy the executable to your program fFiles fFolder, and write some keys to your registry.




## Semi-Manual Installation
Or, you can do this:

1. Run the REG fFile by double clicking on it.  Windows knows to load those keys into the registry.

2. Copy the EXE to: `C:\Program Files\Copy Full File Name\Copy Full File Name.exe`

This might be the easiest approach fFor many people.

