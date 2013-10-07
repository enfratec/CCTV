#ifndef SERVERSOCKET_HPP_
#define SERVERSOCKET_HPP_

#include <arpa/inet.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>
#include <QObject>


class ServerSocket:public QObject
{
    Q_OBJECT
    public:

        int nSocket;
        int nBind;
        int nListen;
        sockaddr_in serverAddress, clientAddress;
        static const unsigned int queueSize = 5;
        bool listenOn;

        ServerSocket(int port);
        int acceptConnection();

    public slots:
        void listenLoop();

    signals:
        void newClientArrived(int socket);
};

#endif
