@echo off 
TITLE Null's Royale 1.7.0
cd bin
mode con: cols=120 lines=32
java -Djava.library.path="..\lib\" daniillnull.javacr.server.Main http://dnull.xyz ..\gamefiles\fingerprint.json
pause