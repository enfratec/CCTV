#include "ServerQApplication.hpp"


ServerQApplication::ServerQApplication(int argc, char* argv[]):QApplication(argc, argv)
{
    //preparing main window
    gridLayout = new QGridLayout;
    gridLayout->setAlignment(Qt::AlignTop);

    framesPanel = new QWidget;
    framesPanel->setLayout(gridLayout);

    scrollArea = new QScrollArea;
    scrollArea->setWidgetResizable(true);
    scrollArea->setBackgroundRole(QPalette::Dark);
    scrollArea->setVerticalScrollBarPolicy(Qt::ScrollBarAlwaysOn);
    scrollArea->setWidget(framesPanel);

    mainWindow = new QMainWindow;
    mainWindow->setCentralWidget(scrollArea);
    mainWindow->showMaximized();

    currentRow = 0;
    currentColumn = 0;
}

void ServerQApplication::createClient(int socket)
{
    Client *newClient;
    QLabel *newLabel;
    newLabel = new QLabel;
    newLabel->setFixedSize(320, 240);
    newClient = new Client(socket, newLabel);

    QObject::connect(newClient, SIGNAL(update(QLabel*, QByteArray)),
                         this, SLOT(updateWindow(QLabel*, QByteArray)));

    //check if needs new row in scroll area
    if ( currentColumn > 3 )
    {
        currentColumn = 0;
        currentRow++;
    }
    gridLayout->addWidget( newClient->window, currentRow, currentColumn++ );
}

void ServerQApplication::updateWindow(QLabel *window, QByteArray frame)
{
    //update single field for client - if empty: disconnected
    QPixmap tmp;
    if( !frame.isEmpty() )
    {
        tmp.loadFromData(frame);
    }
    else
    {
        tmp.load("dc.png", "PNG");
    }
    window->setPixmap(tmp);
}
