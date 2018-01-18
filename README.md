# MQTTLib

This will be a C# implementation of MQTT based on standard v3.1.1 and using .Net Framework 4.6.1.  The current state of the project is very early in development.  This project is also my first attempt to incorporate unit testing/TDD.

The MQTTLib namespace will eventually include client and server implementations that are fully compliant with MQTT standard v3.1.1.  This is the namespace that should be used most often by client code.

The MQTTLib.Protocol namespace contains the classes that implement the structure of the protocol.  All packets are/will be defined in this namespace as well as several enumerations for flag types.  It is accessible, but is not intended to be used directly.  These classes ensure proper structure of the protocol, but do not guarantee compliant behavior.  Compliant behavior will be guaranteed by the client and server classes in the MQTTLib namespace.

More updates and example code will follow as development continues.
