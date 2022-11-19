using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Net;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                //set port and ip adress to listen to
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                //start server
                server.Start();

                //create a buffer to read data
                Byte[] bytes = new Byte[256];
                String data = null;
                
                //listening loop
                while (true)
                {
                    Console.WriteLine("Waiting for connection");

                    //defines the client (the person sending us data) as whatever client was accepted by the server
                    using TcpClient client = server.AcceptTcpClient();

                    data = null;

                    //stream object
                    NetworkStream stream = client.GetStream();

                    //loop to recieve data
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Send back a response.
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("recieved!");
                        stream.Write(msg);
                        Console.WriteLine("Sent: recieved!");
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}