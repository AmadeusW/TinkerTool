# Tinker Tool
Get real time insight into state of your app. Tweak the behavior of your app in real time. Language agnostic, uses SignalR.

Tinker Tool facilitates two way communication between program under development and a flexible web interface. 
The developed program sends diagnostic information, logs, current value of the variables, etc. 
The web interface displays, organizes and charts the data. 
Developed program may probes the server for up to date value for use. The web interface offers a way for developer to modify data returned to the program.


The Debug Center is built using Blazor and communication is accomplished with SignalR, which lets the client library to be cross platform and language agnostic.

## Architecture
| Component | Role |
|---|---|
| **.NET core server:** | |
| User interface | Displaying and modifying data |
| Cache | Data model |
| SignalR host | Communication with user app |
| ⚡ |   |
| **Client library:** | |
| Network adapter | SignalR communication |
| Cache | Instant access to data from app |
| *Debugged program* | |

# Requirements:
## Server:
dot net core 2.0 SDK and runtime
signalr for asp net core 2 (nuget.config is provided)
## Client:
ability to connect using sockets
