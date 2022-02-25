using CommandLine;

namespace test3
{
    public class Options
    {
        [Option('f', "file",
            Required = true,
            HelpText = "File (with AIS messages) to upload to the database.")]
        public string FileName { get; set; } = string.Empty;

        [Option('u', "user",
            Required = true,
            HelpText = "Connect to the database as the user 'username' instead of the default. (You must have permission to do so, of course.)")]
        public string UserName { get; set; } = string.Empty;

        [Option('h', "host",
            Default = "localhost",
            HelpText = "Specifies the host name of the machine on which the server is running. If the value begins with a slash, it is used as the directory for the Unix-domain socket.")]
        public string Host { get; set; } = string.Empty;

        [Option('d', "dbname",
            Default = "AIS",
            HelpText = "Specifies the name of the database to connect to.")]
        public string DataBase { get; set; } = string.Empty;

        [Option('p', "port",
            Default = (int) 5432,
            HelpText = "Specifies the TCP port or the local Unix-domain socket file extension on which the server is listening for connections.")]
        public int Port { get; set; }

        [Option('l', "loadfiletomemorry",
            HelpText = "Load all lines from file to memory for once. Faster, but need much memory. Be carefull.")]
        public bool LoadAllLines { get; set; } = false;

        [Option('b', "boud",
            Default = (int)115200,
            HelpText = "Boud rate for serial port.")]
        public int Boud { get; set; } = 115200;
    }
}
