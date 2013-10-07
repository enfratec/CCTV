#ifndef SERVERQAPPLICATION_HPP_
#define SERVERQAPPLICATION_HPP_


#include <QApplication>
#include <QtGui>
#include <QThread>
#include "Client.hpp"

using namespace std;

class ServerQApplication : public QApplication
{
    Q_OBJECT
    public:
        ServerQApplication(int argc, char* argv[]);
        QMainWindow *mainWindow;
        QWidget *framesPanel;
        QScrollArea *scrollArea;
        QGridLayout *gridLayout;
        int currentRow;
        int currentColumn;

    public slots:
        void createClient(int socket);
        void updateWindow(QLabel *window, QByteArray frame);
};


#endif
