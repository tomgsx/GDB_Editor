Recommended: Create two folders in the project folder, "dataSource" and "dataMods".

"dataSource" will be the default source folder for original game files. I would copy the games' assetsXXX.zip files here, extract all here to their own folders, example: \dataSource\assets001, \dataSource\assets002, \dataSource\assets003, \dataSource\assets004. In the program, use File-Settings to set the source to the subset you want to work with, i.e. \dataSource\assets004. This way the program will only look at GDB files in that specific directory. 
(technically you could leave the source directory at \dataSource, but that will slightly change the folder structure of the \dataMods output folder - each mod will save into its own \assetsXXX folder)

"dataMods" will be the default output folder for modified game files. Modified files will be saved to the output directory from Settings (default \dataMods) and will keep the source structure from the source. For example, if you have the source directory set to \dataSource\assets004 and then save a file change to \assets004\Database\systems.gdb, it will create a Database folder in dataMods and you will get \dataMods\Database\systems.gdb. In most cases, you can copy the contents of the dataMods folder directly into your game assets folder and it should use them.

Reminder: this is a prototype/rough draft, intended to help with modding, but not do everything.

The intent is to use this to create some file mods, then user will then copy those from \dataMods to their game directory. Any time you change the source directory within Settings, you will probably also want to manually delete the contents of your dataMods folder so the program will not mix up modded files from different assets sources.
