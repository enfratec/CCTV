#include "ServerSocket.hpp"
#include "Client.hpp"
#include "ServerQApplication.hpp"

using namespace std;

int main(int argc, char* argv[])
{
   if (argc != 2)
   {
     cout << "Usage: port_number" << endl;
     exit(1);
   }

   //main window = main thread
   ServerQApplication server(argc, argv);

   //socket
   ServerSocket serverSocket(atoi(argv[1]));
   QThread *serverSocketThread = new QThread;
   serverSocket.moveToThread(serverSocketThread);

   //binding signals and slots
   QObject::connect(serverSocketThread, SIGNAL(started()), &serverSocket, SLOT(listenLoop()));
   QObject::connect(&serverSocket, SIGNAL(newClientArrived(int)), &server, SLOT(createClient(int)));
   QObject::connect(&server, SIGNAL(aboutToQuit()), serverSocketThread, SLOT(quit()));

   //start
   serverSocketThread->start();
   server.exec();

   //exit
   close(serverSocket.nSocket);
   return 0;
}
