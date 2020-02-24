# Projects

To aquire geolocation - try use of System.Device.location;

to store and save data - try calling a .txt file in \debug\bin (location of .exe file aswell).
***
string executableLocation = Path.GetDirectoryName(
    Assembly.GetExecutingAssembly().Location);
string xslLocation = Path.Combine(executableLocation, "test.txt");
***

