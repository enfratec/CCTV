#include "Client.hpp"

using namespace std;

Client::Client(int sock, QLabel *window)
{
    this->window = window;
    this->socket = sock;
    moveToThread(&thread);

    QObject::connect(&thread, SIGNAL(started()), this, SLOT(receive()));

    //new thread for client
    thread.start();
}

void Client::receive()
{
    int frameSize = 0;
    int read;
    int total;
    unsigned char *frameBuffer;
    QByteArray frameBytes;
    bool videoInProgress = true;

    while ( videoInProgress ) {

        //receiveing size of frame
        read = recv(socket, (char*)&frameSize, 4, 0);
        if (frameSize != 0 && read !=-1)
        {
            total = 0;
            read = 0;
            frameBuffer = new unsigned char[frameSize*sizeof(unsigned char)];

            //receiving single frame
            while ( total < frameSize )
            {
                read = recv(socket, &frameBuffer[total], frameSize - total, 0);
                if ( read != 0 && read != -1  )
                {
                    total += read;
                }
                else
                {
                    videoInProgress = false;
                    break;
                }
            }

            //check size of received frame
            if ( total == frameSize )
            {
                frameBytes.clear();
                frameBytes.append((const char*)frameBuffer, total);
                delete(frameBuffer);

                //send frame to client's field on main window
                emit update(this->window, frameBytes);
            }
        }
    }

    //disconnected: send empty array
    frameBytes.clear();
    emit update(this->window, frameBytes);

    //quit from thread
    close(socket);
    thread.quit();
}
