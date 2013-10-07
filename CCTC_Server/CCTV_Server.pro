CONFIG += qt

HEADERS	+= Client.hpp
HEADERS += ServerQApplication.hpp
HEADERS += ServerSocket.hpp

SOURCES += Client.cpp
SOURCES += ServerQApplication.cpp
SOURCES += ServerSocket.cpp
SOURCES += main.cpp

TARGET = CCTV_Server
