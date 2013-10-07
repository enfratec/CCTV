#!/bin/bash

clear

qmake -o Makefile CCTV_Server.pro
make
echo -e "\n\n";

./CCTV_Server 1234

echo -e "\n\n";
make clean
