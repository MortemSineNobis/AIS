using CommandLine;

namespace FileToAIS
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
            HelpText = "Specifies the host name of the machine on which the server is running. If the value begins with a slash, it is used as the directory for the Unix-domain socket. (Default: localhost)")]
        public string Host { get; set; } = "localhost";

        [Option('d', "dbname",
            HelpText = "Specifies the name of the database to connect to. (Default: AIS)")]
        public string DataBase { get; set; } = "AIS";

        [Option('p', "port",
            HelpText = "Specifies the TCP port or the local Unix-domain socket file extension on which the server is listening for connections. (Default: 5432)")]
        public int Port { get; set; } = 5432;
        
        [Option('a', "aisid",
            Required = true,
            HelpText = "AIS id for database")]
        public int AisId { get; set; }

        [Option('l', "loadfiletomemorry",
            HelpText = "Load all lines from file to memory for once. Faster, but need much memory. Be carefull.")]
        public bool LoadAllLines { get; set; } = false;
    }
}
