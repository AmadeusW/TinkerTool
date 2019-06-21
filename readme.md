# Debug Center
Get real time insight into state of your app. Tweak the behavior of your app in real time.

Debug Center facilitates two way communication between a flexible web interface and user's application.
User's application sends diagnostic logs, current value of the variables, etc. The web interface may chart numeric data.
User's application probes the server for up to date value for use. The web interface offers a way for user to modify this data.

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
