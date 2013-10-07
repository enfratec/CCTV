#include "ServerSocket.hpp"

using namespace std;

ServerSocket::ServerSocket(int port)
{
    //init
    memset(&serverAddress, 0, sizeof(struct sockaddr));
    serverAddress.sin_family = AF_INET;
    serverAddress.sin_addr.s_addr = htonl(INADDR_ANY);
    serverAddress.sin_port = htons(port);

    //create socket
    nSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (nSocket < 0)
    {
      cout << "Can't create a socket." << endl;
      exit(1);
    }

    //set options
    int reuseAddress = 1;
    setsockopt(nSocket, SOL_SOCKET, SO_REUSEADDR, (char*)&reuseAddress, sizeof(reuseAddress));

    //bind a name to a socket
    nBind = bind(nSocket, (struct sockaddr*)&serverAddress, sizeof(struct sockaddr));
    if (nBind < 0)
    {
        cout << "Can't bind a name to a socket." << endl;
        exit(1);
    }

    //queue size
    nListen = listen(nSocket, queueSize);
    if (nListen < 0)
    {
        cout << "Can't set queue size" << endl;
    }

    listenOn = true;
}

int ServerSocket::acceptConnection()
{
    //accept connection
    return accept(nSocket, (sockaddr*)NULL, NULL);
}

void ServerSocket::listenLoop()
{
    while (listenOn)
    {
        int isNewClient = acceptConnection();

        if ( isNewClient > 0 )
        {
            emit newClientArrived(isNewClient);
        }
        else if ( isNewClient == -1 )
        {
            listenOn = false;
            break;
        }
    }
}
