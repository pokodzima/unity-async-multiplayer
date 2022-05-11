# unity-async-multiplayer
Simple async multiplyer implementation

This project is an example of how game actions can be recorded and replayed for asynchronous multiplayer. The example uses a cube for which position and orientation changes over time can be recorded and serialized in JSON. This recording can then be played back. 

It is possible to release many ways to reduce the size of the record. In this example, you can adjust the frequency of the recording. It is also possible to implement decimal point reduction, as well as compression of resulting record, for example through GZip.
