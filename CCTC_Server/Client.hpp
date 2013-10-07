#ifndef CLIENT_HPP_
#define CLIENT_HPP_

#include <arpa/inet.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <QObject>
#include <QThread>
#include <QLabel>
#include <QBuffer>
#include <iostream>

class Client:public QObject
{
    Q_OBJECT

    public:
        Client();
        Client(int sock, QLabel *window);
        int socket;
        QThread thread;
        QLabel *window;

    public slots:
        void receive();

    signals:
        void update(QLabel *window, QByteArray frame);
};


#endif
